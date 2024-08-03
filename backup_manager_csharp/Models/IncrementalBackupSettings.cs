namespace backup_manager_csharp.Models;

public class IncrementalBackupSettings(Boolean enabled, int retentionDays)
{
    public Boolean Enabled { get; set; } = enabled;
    public int RetentionDays { get; set; } = retentionDays;
}