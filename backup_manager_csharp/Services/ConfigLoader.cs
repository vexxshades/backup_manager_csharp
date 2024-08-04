using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;
public class ConfigLoader
{
    private readonly AppSettings _appSettings = AppSettingsLoader.Load();
    public readonly List<BackupConfig> BackupConfigs = new List<BackupConfig>();

    public ConfigLoader()
    {
        string[] applications = Directory.GetDirectories(_appSettings.BackupConfigBaseDirectory);
        foreach (string application in applications)
        {
            string json = File.ReadAllText($"{application}/config.json");
            BackupConfig config = JsonSerializer.Deserialize<BackupConfig>(json);
            BackupConfigs.Add(config);

        }
    }
}