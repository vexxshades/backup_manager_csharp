using System.Net.Mime;
using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Backups;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;

public class BackupScheduler
{
    private readonly AppSettings _appSettings;
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

    public BackupScheduler(AppSettings appSettings)
    {
        this._appSettings = appSettings;
        foreach (BackupConfig backupConfig in _backupConfigs)
        {
            BackupManifest backupManifest = BackupManifestLoader.LoadByApplication(backupConfig.Application
                , _appSettings);
            _backupState.Add(backupConfig, backupManifest);
        }
    }

    public bool IsFirstBackUp(List<FullBackUp> fullBackUpList, BackUpSettings backUpSettings, string application)
    {
        Console.WriteLine($"No backup found for {application}.");
        return backUpSettings.Enabled && fullBackUpList.Count == 0;
        
    }

    public bool IsFullBackupDue(BackUpSettings backupSettings, FullBackUp fullBackUp, BackupFrequency backupFrequency)
    {
        DateTime currentDate = DateTime.Now;
        switch (backupFrequency)
        {
            case BackupFrequency.HOURLY:
                if (currentDate >= fullBackUp.BackupCreationDate.AddHours(1))
                {
                    return true;
                }

                break;
            case BackupFrequency.DAILY:
                if (currentDate >= fullBackUp.BackupCreationDate.AddDays(1))
                {
                    return true;
                }

                break;
            case BackupFrequency.WEEKLY:
                if (currentDate >= fullBackUp.BackupCreationDate.AddDays(7))
                {
                    return true;
                }

                break;
            case BackupFrequency.MONTHLY:
                if (currentDate >= fullBackUp.BackupCreationDate.AddMonths(1))
                {
                    return true;
                }
                break;
            
        }
        
        return false;

    }
    
    public bool IsIncrementalBackupDue(IncrementalBackupSettings backupSettings, IncrementalBackUp incrementalBackUp,
        int days)
    {
        if (_dateTime >= incrementalBackUp.BackupCreationDate.AddDays(days))
        {
            return true;
        }
        else return false;
    }

    public void CopyFullBackUp(BackupConfig backupConfig, BackupFrequency backupFrequency)
    {
        string destinationDirectory = $"{_appSettings.BackupBaseDirectory}/{backupConfig.Application}";
        string destinationFolder = $"{destinationDirectory}/{Path.GetFileName(backupConfig.SourceDirectory)}";
        string destinationTarFile = $"{destinationFolder}.tar.gz";

        FullBackUp fullBackup = new FullBackUp(
            sourceDirectory: backupConfig.SourceDirectory,
            destinationDirectory: destinationDirectory,
            destinationTarFile: destinationTarFile
        );


        if (!Directory.Exists(destinationDirectory))
        {
            Directory.CreateDirectory(destinationDirectory);
        }
        
        foreach (string file in Directory.GetFiles(backupConfig.SourceDirectory))
        {
            File.Copy(file, Path.Combine(destinationDirectory, Path.GetFileName(file)));
        }

        foreach (string directory in Directory.GetDirectories(backupConfig.SourceDirectory))
        {
            File.Copy(directory, Path.Combine(destinationDirectory,
                Path.GetDirectoryName(directory)));
        }

        switch (backupFrequency)
        {
            case BackupFrequency.HOURLY:
                _backupState[backupConfig].HourlyFullBackups.Add(fullBackup);
                break;
            case BackupFrequency.DAILY:
                _backupState[backupConfig].DailyFullBackups.Add(fullBackup);
                break;
            case BackupFrequency.WEEKLY:
                _backupState[backupConfig].WeeklyFullBackups.Add(fullBackup);
                break;
            case BackupFrequency.MONTHLY:
                _backupState[backupConfig].MonthlyFullBackups.Add(fullBackup);
                break;
        }
        
        
    }

    public void ProcessBackups(string application, BackUpSettings backUpSettings, List<FullBackUp> fullBackUps,
        BackupConfig backupConfig, BackupFrequency frequency, BackupManifest backupManifest)
    {
        bool isFirstBackup = IsFirstBackUp(fullBackUps, backUpSettings, application);
        if (isFirstBackup)
        {
            CopyFullBackUp(backupConfig, frequency);
            BackupManifestWriter backupManifestWriter = new BackupManifestWriter(backupManifest, application);
            backupManifestWriter.WriteConfig();
        }

        if (fullBackUps.Count == 0)
        {
            return;
        }
        
        FullBackUp lastFullBackup = fullBackUps[fullBackUps.Count - 1];
        bool isFullBackupDue = IsFullBackupDue(backUpSettings, lastFullBackup, frequency);
        if (isFullBackupDue)
        {
            CopyFullBackUp(backupConfig, frequency);
            BackupManifestWriter backupManifestWriter = new BackupManifestWriter(backupManifest, application);
            backupManifestWriter.WriteConfig();
        }

    }

    public void ApplyFullBackups()
    {
        // foreach (KeyValuePair<BackupConfig, BackupManifest> backupState in _backupState)
        foreach(BackupConfig backupConfig in _backupConfigs)
        {
            string application = backupConfig.Application;
            BackupManifest backupManifest = BackupManifestLoader.LoadByApplication(application, _appSettings);
            BackUpSettings hourlyBackupSettings = backupConfig.HourlyBackupSettings;
            List<FullBackUp> hourlyFullBackups = backupManifest.HourlyFullBackups;
            ProcessBackups(application, hourlyBackupSettings, hourlyFullBackups, backupConfig, BackupFrequency.HOURLY,
                backupManifest);
            
            BackUpSettings dailyBackupSettings = backupConfig.DailyBackupSettings;
            List<FullBackUp> dailyFullBackups = backupManifest.DailyFullBackups;
            ProcessBackups(application, dailyBackupSettings, dailyFullBackups, backupConfig, BackupFrequency.DAILY,
                backupManifest);
            
            BackUpSettings weeklyBackupSettings = backupConfig.WeeklyBackupSettings;
            List<FullBackUp> weeklyFullBackups = backupManifest.WeeklyFullBackups;
            ProcessBackups(application, weeklyBackupSettings, weeklyFullBackups, backupConfig, BackupFrequency.HOURLY,
                backupManifest);
            
            BackUpSettings monthlyBackupSettings = backupConfig.MonthlyBackupSettings;
            List<FullBackUp> monthlyFullBackups = backupManifest.MonthlyFullBackups;
            ProcessBackups(application, monthlyBackupSettings, monthlyFullBackups, backupConfig, BackupFrequency.DAILY,
                backupManifest);

            var backupManifestWriter = new BackupManifestWriter(backupManifest, application);
            backupManifestWriter.WriteConfig();

        }
    }
    
    
}

