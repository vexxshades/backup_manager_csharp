using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace backup_manager_csharp.Models.Backups;

public class BackupManifest
{
    public List<FullBackUp> HourlyFullBackups { get; set; }
    public List<FullBackUp> DailyFullBackups { get; set; }
    public List<FullBackUp> WeeklyFullBackups { get; set; }
    public List<FullBackUp> MonthlyFullBackups { get; set; }
}