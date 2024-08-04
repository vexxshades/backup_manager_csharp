using System.Text.Json;

namespace backup_manager_csharp.Models.Settings;

public class AppSettings
{
    public string BackupConfigBaseDirectory { get; set; }
    public string BackupBaseDirectory { get; set; }

    public AppSettings()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        string jsonString = File.ReadAllText(@"/opt/backup_manager/appsettings.json");
        AppSettings appSettings = JsonSerializer.Deserialize<AppSettings>(jsonString);
        this.BackupBaseDirectory = appSettings.BackupBaseDirectory;
        this.BackupConfigBaseDirectory = appSettings.BackupBaseDirectory;
    }
}