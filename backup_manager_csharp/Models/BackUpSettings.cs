namespace backup_manager_csharp.Models;

public class BackUpSettings(Boolean enabled, int retentionDays, IncrementalBackupSettings incrementalBackupSettings)
{
    public Boolean Enabled { get; set; } = enabled;
    public int RetentionDays { get; set; } = retentionDays;
    public IncrementalBackupSettings incrementalBackupSettings { get; set; } = incrementalBackupSettings;
}