// See https://aka.ms/new-console-template for more information

using Cronicology;
using Newtonsoft.Json;


Syslog.Write("Starting up the cronicology application.");

// Load configuration files.




Console.WriteLine("Backups: " + configs.Backups.Count + "\n" + "Path 1: " + configs.Backups.First().Path);

var files = Directory.GetFiles(configs.Backups.First().Path);
foreach (var file in files)
{
    Console.WriteLine(configs.Backups.First().Pattern.IsMatch(file) + " :: " + configs.Backups.First().Pattern.Match(file).Groups[1].Value);
}

Console.WriteLine(configs.Backups.First().Pattern.ToString());

Console.WriteLine("Hello, World!");
