// See https://aka.ms/new-console-template for more information

using Cronicology;
using Newtonsoft.Json;
using Tomlyn;


Syslog.Write("Starting up the cronicology application.");

// Load configuration files.

var config = new Configurations();
var cron = new Cron();

Console.WriteLine("Backups: " + cron.Config.Backups.Count + "\n" + "Path 1: " + cron.Config.Backups.First().Path);

var files = Directory.GetFiles(cron.Config.Backups.First().Path);
foreach (var file in files)
{
    Console.WriteLine(cron.Config.Backups.First().Pattern.IsMatch(file) + " :: " + cron.Config.Backups.First().Pattern.Match(file).Groups[1].Value);
}

Console.WriteLine(cron.Config.Backups.First().Pattern.ToString());

Console.WriteLine("Hello, World!");
