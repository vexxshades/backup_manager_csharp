using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace backup_manager_csharp.Models.Backups;

public class BackupManifest
{
    public List<FullBackUp> HourlyFullBackups { get; set; }
    public List<FullBackUp> DailyFullBackups { get; set; }
    public List<FullBackUp> WeeklyFullBackups { get; set; }
    public List<FullBackUp> MonthlyFullBackups { get; set; }

    public static BackupManifest GetDefaultBackupManifest()
    {
        return new BackupManifest()
        {
            HourlyFullBackups = new List<FullBackUp>(),
            DailyFullBackups = new List<FullBackUp>(),
            WeeklyFullBackups = new List<FullBackUp>(),
            MonthlyFullBackups = new List<FullBackUp>()
        };
    }

    public string BackupManifestToJsonString()
    {
        return JsonSerializer.Serialize(this);
    }
}