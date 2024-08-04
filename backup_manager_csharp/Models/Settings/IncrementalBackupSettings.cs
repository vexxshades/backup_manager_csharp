namespace backup_manager_csharp.Models.Settings;

public class IncrementalBackupSettings
{
    public bool Enabled { get; set; }
    public int RetentionDays { get; set;}

    public IncrementalBackupSettings()
    {
        
    }

    public IncrementalBackupSettings(bool enabled, int retentionDays)
    {
        this.Enabled = enabled;
        this.RetentionDays = retentionDays;
    }
}