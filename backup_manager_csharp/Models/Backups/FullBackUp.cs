namespace backup_manager_csharp.Models.Backups;

public class FullBackUp : Backup
{
    public readonly List<IncrementalBackUp> IncrementalBackups = new List<IncrementalBackUp>();
    
    public FullBackUp(string sourceDirectory, string destinationDirectory, string destinationTarFile)
        : base(sourceDirectory, destinationDirectory, destinationTarFile){}
}