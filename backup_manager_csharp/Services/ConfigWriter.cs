using System.Net;
using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;

public class ConfigWriter
{
    private BackupConfig _backupConfig;
    private AppSettings _appSettings = AppSettingsLoader.Load();
    private string _application;
    private string _configDirectory;
    private string _configurationPath;
    

    public ConfigWriter(BackupConfig backupConfig)
    {
        this._backupConfig = backupConfig;
        this._application = backupConfig.Application;
        this._configDirectory = $"{_appSettings.BackupConfigBaseDirectory}/{_application}";
        this._configurationPath = $"{_configDirectory}/config.json";
    }

    public void WriteConfig()
    {
        Directory.CreateDirectory(_configDirectory);
        File.WriteAllText(_configurationPath, _backupConfig.BackUpConfigToJsonString());
    }
}