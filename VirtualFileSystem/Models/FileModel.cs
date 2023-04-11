namespace VirtualFileSystem.Models;

public class FileModel
{
    public string Name { get; set; }

    public FileModel(string name)
    {
        Name = name;
    }
}