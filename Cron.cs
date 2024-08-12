using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Cronicology.Syslog;

namespace Cronicology
{
    internal class Cron
    {
        public static string PSFName { get; set; } = "cronicology.psf";
        public static string Path { get; set; } = "/opt/psf";
        public static string PSF
        {
            get
            {
                return Path + "/" + PSFName;
            }
        }
        public static DateTime StartTime { get; set; }
        public static DateTime StopTime { get; set; }
        public static Configurations Config { get; set; }

        Cron()
        {
            Config = JsonConvert.DeserializeObject<Configurations>("config.json") ?? new Configurations();
        }

        public static void Start(string message = "Starting Cronicology.")
        {
            Log(message);

            // Lets see if the process is running.
            if (File.Exists(PSF))
            {
                // The file exists, it may still be running.
                var status = File.ReadAllText(PSF);
                if (status != "complete")
                {
                    // The status of the file seems to imply it is running. Exiting.
                    Log("Cronicology already running, exiting.", Level.Warning);
                    Environment.Exit(0);
                }
            }

            // Updating the process status file.
            Log("Writing the process status file at '" + PSF + "'.", system: false);
            File.AppendAllText(PSF, "started");

            // Starting the log.
            if (File.Exists(Config.Log))
            {
                // Log file exists. Checking size.
                var info = new FileInfo(Config.Log);
                if (info.Length > 10*1024*1024)
                {
                    // Log file is greater than 10 MiB so it needs rotated.
                    Log("Log file greater than 10 MiB, rotating it.");
                    Log(DateTime.Now.ToString("s") + ": Rotating log file", system: false);
                    File.Move(Config.Log, Config.LogPath + "/" + Config.LogFile + ".old." + DateTime.Now.ToString("s"));
                    File.Create(Config.Log);
                    Log(DateTime.Now.ToString("s") + ": Log file created.", system: false);
                }
            }
            else
            {
                // The log file doesn't exists. Creating it.
                File.Create(Config.Log);
                File.AppendAllText(Config.Log, DateTime.Now.ToString("s") + ": Log file created.");
            }
        }

        public static void Fail(string message = "Ending Cronicology in a failed state.")
        {
            Syslog.Write(message, Syslog.Level.Err);
            Syslog.Write("Writing failed to PSF file.");
            File.WriteAllText(PSF, "failed");
        }

        public static void Complete()
        {

        }

        public static void Log(string message, Syslog.Level level = Level.Info, string identity = "cronicology", bool system = true)
        {
            if (system) Syslog.Write(message, level, identity);

            File.AppendAllText(Config.Log, DateTime.Now.ToString("s") + "[" + level + "]: " + message );
        }
    }
}
