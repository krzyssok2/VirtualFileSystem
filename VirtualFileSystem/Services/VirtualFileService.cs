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

    public void AddFolders(string[] folders)
    {
        HandleFolderCreation(RootFolder, folders);
    }

    public void DeleteFolder(string[] folders)
    {
        HandleFolderDeletion(RootFolder, folders);
    }

    public void ListFolders()
    {
        ListVirtualFolderStructure(RootFolder, "");
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

    public void ListFiles(string[] folders)
    {
        var lastFolder = GetFoldersWithoutCreation(RootFolder, folders);

        if (lastFolder == null)
        {
            return;
        }

        foreach (var item in lastFolder.Files)
        {
            Console.WriteLine(item.Name);
        }
    }

    private FolderModel GetFolderWithCreation(FolderModel parentFolder, string[] folders)
    {
        if (folders.Length == 0)
        {
            return parentFolder;
        }

        var folder = folders.First();

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder));

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

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder));

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

    private void HandleFolderDeletion(FolderModel parentFolder, string[] newFolders)
    {
        if (newFolders.Length == 0)
        {
            return;
        }

        var folder = newFolders.First();

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder));

        if (neededFolder == null)
        {
            return;
        }

        if (newFolders.Length == 1)
        {
            parentFolder.SubFolder.Remove(neededFolder);
        }

        newFolders = RemoveFirstEntryInArray(newFolders);
        HandleFolderDeletion(neededFolder, newFolders);
    }

    private void HandleFolderCreation(FolderModel parentFolder, string[] newFolders)
    {
        if (newFolders.Length == 0)
        {
            return;
        }

        var folder = newFolders.First();

        var neededFolder = parentFolder.SubFolder.FirstOrDefault(i => i.Name.Equals(folder));

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
        HandleFolderCreation(newlyCreatedFolder, newFolders);
    }

    private static string[] RemoveFirstEntryInArray(string[] array)
    {
        var newArray = new string[array.Length - 1];
        Array.Copy(array, 1, newArray, 0, newArray.Length);
        return newArray;
    }
}