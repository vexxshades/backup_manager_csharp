namespace backup_manager_csharp.Models.Settings;

public class BackUpSettings
{
    public bool Enabled { get; set; } 
    public int RetentionDays { get; set; } 
    public IncrementalBackupSettings IncrementalBackupSettings { get; set; }

    public BackUpSettings()
    {
        
    }

    public BackUpSettings(bool enabled, int retentionDays, IncrementalBackupSettings incrementalBackupSettings)
    {
        this.Enabled = enabled;
        this.RetentionDays = retentionDays;
        this.IncrementalBackupSettings = incrementalBackupSettings;
    }
}