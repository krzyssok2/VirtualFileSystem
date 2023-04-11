using VirtualFileSystem.Models;

namespace VirtualFileSystem.Interfaces;

public interface IStorageService
{
    FolderModel GetSystemData();

    void SaveFolderData(FolderModel folderModel);
}