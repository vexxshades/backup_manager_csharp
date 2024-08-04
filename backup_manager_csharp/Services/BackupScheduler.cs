using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Backups;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;

public class BackupScheduler
{
    private readonly AppSettings _appSettings = AppSettingsLoader.Load();
    private readonly List<BackupConfig> _backupConfigs = new ConfigLoader().BackupConfigs;
    private readonly DateTime _dateTime = DateTime.Now;
    
    public bool RequireDailyFullBackup(BackupConfig backupConfig)
    {
        
        return true;
    }
}

