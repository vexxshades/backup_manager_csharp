using System.Text.Json;

namespace backup_manager_csharp.Models.Settings;

public class AppSettings
{
    public string BackupConfigBaseDirectory { get; set; }
    public string BackupBaseDirectory { get; set; }
    public string BackupAppData { get; set; }
}