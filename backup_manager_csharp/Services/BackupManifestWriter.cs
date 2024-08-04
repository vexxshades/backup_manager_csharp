using backup_manager_csharp.Models.Backups;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;

public class BackupManifestWriter
{
    private BackupManifest _backupManifest;
    private AppSettings _appSettings = AppSettingsLoader.Load();
    private string _application;
    private string _manifestDirectory;
    private string _manifestPath;
    

    public BackupManifestWriter(BackupManifest backupManifest, string application)
    {
        this._backupManifest = backupManifest;
        this._application = application;
        this._manifestDirectory = $"{_appSettings.BackupAppData}/{_application}";
        this._manifestPath = $"{_manifestDirectory}/manifest.json";
    }

    public void WriteConfig()
    {
        Directory.CreateDirectory(_manifestDirectory);
        // File.WriteAllText(_manifestDirectory, _backupManifest.BackUpConfigToJsonString());
    }
}