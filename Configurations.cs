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


        public string LogFile { get; set; } = "cronicology.log";


        public string LogPath { get; set; } = "/var/log";


        public string Log
        {
            get
            {
                return LogPath + "/" + LogFile;
            }
        }


        public Configurations()
        {
            Backups = new List<Backup>();
        }
    }
}
