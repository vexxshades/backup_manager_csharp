using System.Runtime.InteropServices.JavaScript;
using System.Security.AccessControl;
using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Backups;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;

public class BackupScheduler
{
    private readonly AppSettings _appSettings = AppSettingsLoader.Load();
    private readonly List<BackupConfig> _backupConfigs = new ConfigLoader().BackupConfigs;
    private readonly DateTime _dateTime = DateTime.Now;
    private readonly Dictionary<BackupConfig, BackupManifest> _backupState = new Dictionary<BackupConfig,
        BackupManifest>();
    public enum BackupFrequency
    {
        HOURLY,
        DAILY,
        WEEKLY,
        MONTHLY
    }

    public BackupScheduler()
    {
        foreach (BackupConfig backupConfig in _backupConfigs)
        {
            BackupManifest backupManifest = BackupManifestLoader.LoadByApplication(backupConfig.Application
                , _appSettings);
            _backupState.Add(backupConfig, backupManifest);
        }
    }

    public bool IsFirstBackUp(List<FullBackUp> fullBackUpList, BackUpSettings backUpSettings)
    {
        return backUpSettings.Enabled && fullBackUpList.Count == 0;
    }

    public bool IsFullBackupDue(BackUpSettings backupSettings, FullBackUp fullBackUp, int days)
    {
        DateTime currentDate = DateTime.Now;
        if (currentDate >= fullBackUp.BackupCreationDate.AddDays(days))
        {
            return true;
        }
        else return false;
    }
    
    public bool IsIncrementalBackupDue(IncrementalBackupSettings backupSettings, IncrementalBackUp incrementalBackUp,
        int days)
    {
        DateTime currentDate = DateTime.Now;
        if (currentDate >= incrementalBackUp.BackupCreationDate.AddDays(days))
        {
            return true;
        }
        else return false;
    }

    public void CopyFullBackUp(BackupConfig backupConfig, BackUpSettings backupSettings, BackupFrequency backupFrequency)
    {
        string destinationDirectory = $"{_appSettings.BackupBaseDirectory}/{backupConfig.Application}";
        string destinationFolder = $"{destinationDirectory}/{Path.GetFileName(backupConfig.SourceDirectory)}";
        string destinationTarFile = $"{destinationFolder}.tar.gz";

        switch (backupFrequency)
        {
            case BackupFrequency.HOURLY:
                _backupState[backupConfig].HourlyFullBackups.Add(
                    new FullBackUp(
                        sourceDirectory: backupConfig.SourceDirectory,
                        destinationDirectory: destinationDirectory,
                        destinationTarFile: destinationTarFile
                    ));
                break;
            case BackupFrequency.DAILY:
                _backupState[backupConfig].DailyFullBackups.Add(
                    new FullBackUp(
                        sourceDirectory: backupConfig.SourceDirectory,
                        destinationDirectory: destinationDirectory,
                        destinationTarFile: destinationTarFile
                    ));
                break;
            case BackupFrequency.WEEKLY:
                _backupState[backupConfig].HourlyFullBackups.Add(
                    new FullBackUp(
                        sourceDirectory: backupConfig.SourceDirectory,
                        destinationDirectory: destinationDirectory,
                        destinationTarFile: destinationTarFile
                    ));
                break;
            case BackupFrequency.MONTHLY:
                _backupState[backupConfig].HourlyFullBackups.Add(
                    new FullBackUp(
                        sourceDirectory: backupConfig.SourceDirectory,
                        destinationDirectory: destinationDirectory,
                        destinationTarFile: destinationTarFile
                    ));
                break;
        }
        
        if (!Directory.Exists(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }

        foreach (string file in Directory.GetFiles(backupConfig.SourceDirectory))
        {
            File.Copy(file, Path.Combine(destinationDirectory, Path.GetFileName(file)));
        }
        
    }

    public void ApplyFullBackups()
    {
        foreach (KeyValuePair<BackupConfig, BackupManifest> backupState in _backupState)
        {
            
        }
    }
    
    
}

