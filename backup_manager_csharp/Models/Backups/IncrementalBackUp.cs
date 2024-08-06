namespace backup_manager_csharp.Models.Backups;

public class IncrementalBackUp : Backup
{
    public int id { get; set; }

    public IncrementalBackUp(string sourceDirectory, string destinationDirectory, string destinationTarFile, int id)
        : base(sourceDirectory, destinationDirectory, destinationTarFile)
    {
        this.id = id;
    }
}