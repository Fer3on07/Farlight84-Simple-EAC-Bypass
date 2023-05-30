using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.Win32;
using Fer3onBP;
using System.Diagnostics;

static string GenerateRandomString(int length)
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var random = new Random();
    var result = new char[length];

    for (int i = 0; i < length; i++)
    {
        result[i] = chars[random.Next(chars.Length)];
    }

    return new string(result);
}

Console.Title = "EAC Bypass For FL84 - By Fer3on";

string WelcomeMSG1 = "The esaiest way to bypass EAC on Farlight 84.";
string WelcomeMSG2 = "Comes to you by Unknowncheats!";

Console.ForegroundColor = ConsoleColor.Cyan;
Console.SetCursorPosition((Console.WindowWidth - WelcomeMSG1.Length) / 2, Console.CursorTop);
Console.WriteLine(WelcomeMSG1);
System.Threading.Thread.Sleep(2000);

Console.ForegroundColor = ConsoleColor.Green;
Console.SetCursorPosition((Console.WindowWidth - WelcomeMSG2.Length) / 2, Console.CursorTop);
Console.WriteLine(WelcomeMSG2);
System.Threading.Thread.Sleep(3000);


var key = Environment.Is64BitProcess
? "SOFTWARE\\Wow6432Node\\Valve\\Steam"
: "SOFTWARE\\Valve\\Steam";

var SettingsPath = string.Empty;

using var steamKey = Registry.LocalMachine.OpenSubKey(key);
if (steamKey is not null)
{
    var steamInstallPath = steamKey.GetValue("InstallPath") as string;
    if (Directory.Exists(steamInstallPath))
    {
        var libraryFolders = Path.Combine(steamInstallPath, "steamapps", "libraryfolders.vdf");
        if (File.Exists(libraryFolders))
        {
            var content = File.ReadAllLines(libraryFolders)
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(l => l.Trim())
                .ToList();

            var libraries = new List<string>();
            content.ForEach(l =>
            {
                l = l.Replace("\"", string.Empty);
                if (!l.StartsWith("path")) return;
                l = l.Remove(0, 4).Replace("\\\\", "\\").Trim();
                if (Directory.Exists(l)) libraries.Add(l);
            });

            libraries.ForEach(l =>
            {
                var path = Path.Combine(l,
                    "steamapps", "common", "Farlight 84",
                    "EasyAntiCheat", "settings.json"
                );

                if (File.Exists(path)) SettingsPath = l;
            });
        }
    }
}

if (!Directory.Exists(SettingsPath))
{
    Console.WriteLine("Unable to locate Farlight 84 root directory, please enter the path manually.");
    Console.Write("> ");
    var input = Console.ReadLine();
    if (!Directory.Exists(input))
    {
        Console.WriteLine("\"{0}\" is not a valid directory!", input);
        Console.WriteLine("Exit in 3 seconds...");
        Console.ReadLine();
        System.Threading.Thread.Sleep(3000);
        Environment.Exit(1);
    }
}

var settingsPath = Path.Combine(SettingsPath,
    "steamapps", "common", "Farlight 84",
    "EasyAntiCheat", "settings.json"
);

if (!File.Exists(settingsPath))
{
    Console.WriteLine("\"{0}\" is not a valid Farlight 84 installation!", SettingsPath);
    Console.WriteLine("Exit in 3 seconds...");
    Console.ReadLine();
    System.Threading.Thread.Sleep(3000);
    Environment.Exit(1);
}

var backupSettings = Path.Combine(SettingsPath,
    "steamapps", "common", "Farlight 84",
   "EasyAntiCheat", "settings2.json"
);

if (File.Exists(backupSettings)) File.Delete(backupSettings);
File.Copy(settingsPath, backupSettings);


var originalContent = File.ReadAllText(settingsPath, Encoding.UTF8);
var originalSettings = JsonSerializer.Deserialize<Settings>(originalContent);

var patchedSettings = originalSettings! with
{
    ProductId = GenerateRandomString(15),
    SandboxId = GenerateRandomString(15),
    DeploymentId = GenerateRandomString(15)
};

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
};
var patchedContent = JsonSerializer.Serialize(patchedSettings, options);
File.WriteAllText(settingsPath, patchedContent);

Console.Clear();
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("Waiting for the game:");

while (Process.GetProcessesByName("start_protected_game").Length <= 0)
{
    System.Threading.Thread.Sleep(1000);
}

Console.Clear();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Done! FL84 EAC bypassed.");

while (Process.GetProcessesByName("SolarlandClient-Win64-Shipping").Length <= 0)
{
    System.Threading.Thread.Sleep(2000);
}
if (File.Exists(settingsPath)) File.Delete(settingsPath);
File.Copy(backupSettings, settingsPath);
System.Threading.Thread.Sleep(5000);