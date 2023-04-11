using VirtualFileSystem.Models;

namespace VirtualFileSystem.Tests.Arrange;

public static class ArrangeVirtualFileServiceUnitTests
{
    public static FolderModel RootFolderWithParent1FolderAndTestExeFile = new FolderModel
    {
        Name = "Root",
        SubFolder = new List<FolderModel>
        {
            new FolderModel
            {
                Name = "Parents1",
                Files = new List<FileModel>
                {
                    new FileModel("test.exe")
                }
            }
        }
    };

    public static FolderModel RootFolderWithParent1AndNoFiles = new FolderModel
    {
        Name = "Root",
        SubFolder = new List<FolderModel>
        {
            new FolderModel
            {
                Name = "Parents1"
            }
        }
    };

    public static FolderModel RootFolderWithNoFolders = new FolderModel
    {
        Name = "Root"
    };
}