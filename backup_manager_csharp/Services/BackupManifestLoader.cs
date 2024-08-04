using System.Net;
using System.Text.Json;
using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Backups;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp.Services;

public class BackupManifestLoader
{
   public static BackupManifest LoadByApplication(string application, AppSettings _appSettings)
   {
      string manifestPath = $"{_appSettings.BackupAppData}/{application}/manifest.json";
      string json = File.ReadAllText(manifestPath);
      return JsonSerializer.Deserialize<BackupManifest>(json);
   }
}