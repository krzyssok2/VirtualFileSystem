using System.Text.Json;
using VirtualFileSystem.Constants;
using VirtualFileSystem.Interfaces;
using VirtualFileSystem.Models;

namespace VirtualFileSystem.Services;

public class StorageService : IStorageService
{
    public FolderModel GetSystemData()
    {
        var jsonFileExists = File.Exists(ApplicationConstants.StorageJson);

        if (jsonFileExists)
        {
            var json = File.ReadAllText(ApplicationConstants.StorageJson);
            return JsonSerializer.Deserialize<FolderModel>(json);
        }

        return new FolderModel
        {
            Name = ApplicationConstants.RootName
        };
    }

    public void SaveFolderData(FolderModel folderModel)
    {
        string strJson = JsonSerializer.Serialize(folderModel);

        var jsonFileExists = File.Exists(ApplicationConstants.StorageJson);

        if (jsonFileExists)
        {
            File.Delete(ApplicationConstants.StorageJson);
        }

        File.WriteAllText(ApplicationConstants.StorageJson, strJson);
    }
}