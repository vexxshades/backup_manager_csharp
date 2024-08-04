using System.Text.Json;
using backup_manager_csharp.Models.Backups;
using backup_manager_csharp.Models.Settings;
namespace backup_manager_csharp.Models;

public class BackupConfig
{

    public string SourceDirectory { get; set; }
    public string Application { get; set; } 
    public BackUpSettings HourlyBackupSettings { get; set; } 
    public BackUpSettings DailyBackupSettings { get; set; } 
    public BackUpSettings WeeklyBackupSettings { get; set; } 
    public BackUpSettings MonthlyBackupSettings { get; set; }

    public BackupConfig()
    {
        
    }

    public BackupConfig(string sourceDirectory, string application, BackUpSettings hourlyBackupSettings,
        BackUpSettings dailyBackupSettings, BackUpSettings weeklyBackupSettings, BackUpSettings monthlyBackupSettings)
    {
        this.SourceDirectory = sourceDirectory;
        this.Application = application;
        this.HourlyBackupSettings = hourlyBackupSettings;
        this.DailyBackupSettings = dailyBackupSettings;
        this.WeeklyBackupSettings = weeklyBackupSettings;
        this.MonthlyBackupSettings = monthlyBackupSettings;
    }
    public static string GenerateDefaultConfign()
    {
        BackupConfig backupConfig = new BackupConfig()
        {
            HourlyBackupSettings = new BackUpSettings()
            {
                Enabled = false,
                RetentionDays = 0,
                IncrementalBackupSettings = new IncrementalBackupSettings()
                {
                    Enabled = false,
                    RetentionDays = 0
                }
            },
            DailyBackupSettings = new BackUpSettings()
            {
                Enabled = true,
                RetentionDays = 7,
                IncrementalBackupSettings = new IncrementalBackupSettings()
                {
                    Enabled = true,
                    RetentionDays = 1
                }
            },
            WeeklyBackupSettings = new BackUpSettings()
            {
                Enabled = true,
                RetentionDays = 30,
                IncrementalBackupSettings = new IncrementalBackupSettings()
                {
                    Enabled = true,
                    RetentionDays = 7
                }
            },
            MonthlyBackupSettings = new BackUpSettings()
            {
                Enabled = false,
                RetentionDays = 365,
                IncrementalBackupSettings = new IncrementalBackupSettings()
                {
                    Enabled = true,
                    RetentionDays = 7
                }
            },
            Application = "minecraft",
            SourceDirectory = "/opt/docker/minecraft/minecraft_data"
        };

        return JsonSerializer.Serialize(backupConfig).ToString();
    }
}

