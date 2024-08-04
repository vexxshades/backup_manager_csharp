namespace backup_manager_csharp.Models.Backups;

public abstract class Backup
{
    public DateTime BackupCreationDate;
    public string SourceDirectory { get; set; }
    public string DestinationDirectory { get; set; }
    public string DestinationTarFile { get; set; }

    public Backup(string sourceDirectory, string destinationDirectory, string destinationTarFile)
    {
        this.SourceDirectory = sourceDirectory;
        this.DestinationDirectory = destinationDirectory;
        this.DestinationTarFile = destinationTarFile;
        this.BackupCreationDate = new DateTime();
    }
}