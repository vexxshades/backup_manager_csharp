using System.Text.Json;
namespace backup_manager_csharp.Models;

public class BackupConfig(string sourceDirectory, string application, BackUpSettings hourlyBackupSettings,
    BackUpSettings dailyBackupSettings, BackUpSettings weeklyBackupSettings, 
    BackUpSettings monthlyBackupSettings)
{
    public string SourceDirectory { get; set; } = sourceDirectory;
    public string Application { get; set; } = application;
    public BackUpSettings HourlyBackupSettings { get; set; } = hourlyBackupSettings;
    public BackUpSettings DailyBackupSettings { get; set; } = dailyBackupSettings;
    public BackUpSettings WeeklyBackupSettings { get; set; } = weeklyBackupSettings;
    public BackUpSettings MonthlyBackupSettings { get; set; } = monthlyBackupSettings;

    public static void WriteDefaultConfig()
    {
        string application = "minecraft";
        string sourceDirectory = "/opt/docker/minecraft/minecraft_data";
            
        BackUpSettings hourlyBackupSettings = new BackUpSettings(
            enabled: true,
            retentionDays: 7,
            incrementalBackupSettings: new IncrementalBackupSettings(
                enabled: false,
                retentionDays: 0));
        BackUpSettings dailyBackupSettings = new BackUpSettings(
            enabled: true,
            retentionDays: 7,
            incrementalBackupSettings: new IncrementalBackupSettings(
                enabled: false,
                retentionDays: 0));
        BackUpSettings weeklyBackupSettings = new BackUpSettings(
            enabled: true,
            retentionDays: 7,
            incrementalBackupSettings: new IncrementalBackupSettings(
                enabled: false,
                retentionDays: 0));
        BackUpSettings monthlyBackupSettings = new BackUpSettings(
            enabled: true,
            retentionDays: 7,
            incrementalBackupSettings: new IncrementalBackupSettings(
                enabled: false,
                retentionDays: 0));

        BackupConfig backupConfig = new BackupConfig(
            sourceDirectory: sourceDirectory,
            application: application,
            dailyBackupSettings: dailyBackupSettings,
            hourlyBackupSettings: hourlyBackupSettings,
            weeklyBackupSettings: weeklyBackupSettings,
            monthlyBackupSettings: monthlyBackupSettings
        );
        Console.WriteLine(JsonSerializer.Serialize(backupConfig).ToString());
    }
}

