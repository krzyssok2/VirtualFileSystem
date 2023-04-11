namespace VirtualFileSystem.Models;

public class FolderModel
{
    public string Name { get; set; } = string.Empty;
    public List<FolderModel> SubFolder { get; set; } = new();
    public List<FileModel> Files { get; set; } = new();
}