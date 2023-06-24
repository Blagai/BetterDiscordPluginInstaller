using System;
using System.IO;

namespace PluginInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            string executableFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string pluginFolderPath = GetPluginFolderPath();
            string[] sourceFilePath = Directory.GetFiles(executableFolderPath, "*.plugin.js");


            if (pluginFolderPath == null)
            {
                Console.WriteLine("Failed to locate the BetterDiscord plugins folder.");
                Console.WriteLine("Make sure BetterDiscord is installed and try again.");
                return;
            }

            foreach (string filePath in sourceFilePath)
            {
                string pluginFileName = Path.GetFileName(filePath);
                string pluginFilePath = Path.Combine(pluginFolderPath, Path.GetFileName(pluginFileName));
                try
                {
                    File.Copy(filePath, pluginFilePath);
                    Console.WriteLine($"Plugin '{pluginFileName}' successfully installed in the BetterDiscord plugins folder.");
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to install the plugin: {ex.Message}");
                    Thread.Sleep(2000);
                }
            }
        }


        static string GetPluginFolderPath()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string betterDiscordFolder = Path.Combine(appDataFolder, "BetterDiscord");
            string pluginsFolder = Path.Combine(betterDiscordFolder, "plugins");

            if (Directory.Exists(pluginsFolder))
                return pluginsFolder;

            return null;
        }
    }
}