using System.Net;
using System.Text.Json;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;

public class AppSettingsLoader
{
    public static AppSettings Load()
    {
        string json = File.ReadAllText("appsettings.json");
        AppSettings appSettings = JsonSerializer.Deserialize<AppSettings>(json);
        return appSettings;
    }
}