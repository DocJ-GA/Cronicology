using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cronicology
{
    internal class Backup
    {
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Daily { get; set; } = 7;
        public int Weekly { get; set; } = 8;
        public int Monthly { get; set; } = 12;
        public Regex Pattern { get; set; }

        public Backup()
        {
            Pattern = new Regex(@".+(\d\d\d\d-\d\d-\d\d)\.tar\.gz/");
        }
    }
}
