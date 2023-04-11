using VirtualFileSystem.Constants;
using VirtualFileSystem.Interfaces;
using VirtualFileSystem.Models;

namespace VirtualFileSystem.Services;

public class VirtualFileService
{
    private readonly IStorageService _storageService;
    private FolderModel RootFolder;

    public VirtualFileService(IStorageService storageService)
    {
        _storageService = storageService;
        RootFolder = _storageService.GetSystemData();
    }

    public void SaveAndExit()
    {
        _storageService.SaveFolderData(RootFolder);
    }

    public void HardWipe()
    {
        RootFolder = new FolderModel()
        {
            Name = ApplicationConstants.RootName
        };
    }

    public string AddFolders(string[] folders)
    {
        return HandleFolderCreation(RootFolder, folders);
    }

    public string DeleteFolder(string[] folders)
    {
        return HandleFolderDeletion(RootFolder, folders);
    }

    public void ListFolders()
    {
        ListVirtualFolderStructure(RootFolder, string.Empty);
    }

    public string AddFile(string[] folders, string file)
    {
        var lastFolder = GetFolderWithCreation(RootFolder, folders);

        var doesFileExist = lastFolder.Files.Any(i => i.Name == file);

        if (doesFileExist)
        {
            return ApplicationConstants.FileAlreadyExists;
        }

        lastFolder.Files.Add(new FileModel(file));

        return ApplicationConstants.SuccessFileAdded;
    }

    public string RemoveFile(string[] folders, string file)
    {
        var lastFolder = GetFoldersWithoutCreation(RootFolder, folders);

        if (lastFolder == null)
        {
            return ApplicationConstants.FolderNotFoundForSpecifiedFile;
        }

        var fileForDeletion = lastFolder.Files.FirstOrDefault(i => i.Name == file);

        if (fileForDeletion == null)
        {
            return ApplicationConstants.FileNotFound;
        }

        lastFolder.Files.Remove(fileForDeletion);
        return ApplicationConstants.SuccessFileDeleted;
    }

    public string ListFiles(string[] folders)
    {
        var lastFolder = GetFoldersWithoutCreation(RootFolder, folders);

        if (lastFolder == null)
        {
            return string.Empty;
        }

        var fileList = string.Empty;
        foreach (var item in lastFolder.Files)
        {
            fileList += item.Name + '\n';
        }

        return fileList;
    }

    private FolderModel GetFolderWithCreation(FolderModel parentFolder, string[] folders)
    {
        if (folders.Length == 0)
        {
            return parentFolder;
        }

        var folder = folders.First();

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder, StringComparison.OrdinalIgnoreCase));

        folders = RemoveFirstEntryInArray(folders);
        if (neededFolder == null)
        {
            var newlyCreatedFolder = new FolderModel
            {
                Name = folder
            };

            parentFolder.SubFolder.Add(newlyCreatedFolder);

            return GetFolderWithCreation(newlyCreatedFolder, folders);
        }

        return GetFolderWithCreation(neededFolder, folders);
    }

    private FolderModel GetFoldersWithoutCreation(FolderModel parentFolder, string[] folders)
    {
        if (folders.Length == 0)
        {
            return parentFolder;
        }

        var folder = folders.First();

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder, StringComparison.OrdinalIgnoreCase));

        folders = RemoveFirstEntryInArray(folders);

        if (neededFolder == null)
        {
            return null;
        }

        return GetFoldersWithoutCreation(neededFolder, folders);
    }

    public static void ListVirtualFolderStructure(FolderModel folder, string prefix)
    {
        Console.WriteLine($"{prefix}{folder.Name}");

        prefix += ApplicationConstants.Prefix;

        foreach (FolderModel subFolder in folder.SubFolder)
        {
            ListVirtualFolderStructure(subFolder, prefix);
        }
    }

    private string HandleFolderDeletion(FolderModel parentFolder, string[] newFolders)
    {
        if (newFolders.Length == 0)
        {
            return ApplicationConstants.FolderNotFound;
        }

        var folder = newFolders.First();

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder, StringComparison.OrdinalIgnoreCase));

        if (neededFolder == null)
        {
            return ApplicationConstants.FolderNotFound;
        }

        if (newFolders.Length == 1)
        {
            parentFolder.SubFolder.Remove(neededFolder);
            return ApplicationConstants.SuccessFolderDeleted;
        }

        newFolders = RemoveFirstEntryInArray(newFolders);
        return HandleFolderDeletion(neededFolder, newFolders);
    }

    private string HandleFolderCreation(FolderModel parentFolder, string[] newFolders)
    {
        if (newFolders.Length == 0)
        {
            return ApplicationConstants.SuccessFoldersCreated;
        }

        var folder = newFolders.First();

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder, StringComparison.OrdinalIgnoreCase));

        var newlyCreatedFolder = new FolderModel();
        if (neededFolder == null)
        {
            newlyCreatedFolder = new FolderModel
            {
                Name = folder
            };

            parentFolder.SubFolder.Add(newlyCreatedFolder);
        }
        else
        {
            newlyCreatedFolder = neededFolder;
        }

        newFolders = RemoveFirstEntryInArray(newFolders);
        return HandleFolderCreation(newlyCreatedFolder, newFolders);
    }

    private static string[] RemoveFirstEntryInArray(string[] array)
    {
        var newArray = new string[array.Length - 1];
        Array.Copy(array, 1, newArray, 0, newArray.Length);
        return newArray;
    }
}