using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Settings;
using backup_manager_csharp.Services;

namespace backup_manager_csharp
{
    class Program
    {
        public static void Main(String[] args)
        {
            var configWriter = new ConfigWriter
                (BackupConfig.GetDefaultBackupConfig("minecraft",
                    "/opt/docker/minecraft/minecraft_data"));
            configWriter.WriteConfig();

            var configLoader = new ConfigLoader();
            Console.WriteLine(configLoader.BackupConfigs.ToString());
        }

    }
}