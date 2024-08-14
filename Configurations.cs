using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cronicology
{
    internal class Configurations
    {

        public List<Backup> Backups { get; set; }

        public string LogPath { get; set; } = "/var/log/cronicology";

        public float LogMaxSize { get; set; } = 10;

        public int LogMaxOld { get; set; } = 5;

        public string PSF { get; set; }

        public string Log
        {
            get
            {
                if (!LogPath.EndsWith("/"))
                    LogPath += "/";
                return LogPath + "/cronicology.log";
            }
        }

        public Configurations()
        {
            Backups = new List<Backup>();
        }
    }
}
