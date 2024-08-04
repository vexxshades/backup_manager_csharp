using backup_manager_csharp.Models;
using backup_manager_csharp.Models.Settings;

namespace backup_manager_csharp
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine(BackupConfig.WriteDefaultConfigToJson());

        }

    }
}