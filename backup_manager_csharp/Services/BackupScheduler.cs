using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Backups;
using backup_manager_csharp.Models.Settings;
using backup_manager_csharp.Records;

namespace backup_manager_csharp.Services;

public class BackupScheduler
{
    private readonly AppSettings _appSettings = AppSettingsLoader.Load();
    private readonly List<BackupConfig> _backupConfigs = new ConfigLoader().BackupConfigs;
    private readonly DateTime _dateTime = DateTime.Now;
    
    public bool IsFullBackupDue(BackUpSettings backupSettings, FullBackUp fullBackUp, int days)
    {
        if (!backupSettings.Enabled) return false;
        DateTime currentTime = DateTime.Now;
        return currentTime >= fullBackUp.BackupCreationDate.AddDays(days);
    }
    public bool IsIncrementalBackupDue(IncrementalBackupSettings backupSettings, IncrementalBackUp incrementalBackUp,
        int days)
    {
        if (!backupSettings.Enabled) return false;
        DateTime currentTime = DateTime.Now;
        return currentTime >= incrementalBackUp.BackupCreationDate.AddDays(days);
    }

    public void ScheduleFullBackups()
    {
        foreach(BackupConfig backupConfig in _backupConfigs)
        {
            BackupManifest backupManifest =
                BackupManifestLoader.LoadByApplication(backupConfig.Application, _appSettings);
        }
    }
    
    
}

