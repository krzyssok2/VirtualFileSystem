namespace VirtualFileSystem.Constants;

public static class ApplicationConstants
{
    public const string StorageJson = "dataStorage.json";

    public const string RootName = "ROOT";

    public const string AddFolderCommand = "ADDFOLDER";
    public const string RemoveFolder = "REMOVEFOLDER";
    public const string ListFolderCommand = "LISTFOLDERS";

    public const string AddFileCommand = "ADDFILE";
    public const string RemoveFileCommand = "REMOVEFILE";
    public const string ListFilesCommand = "LISTFILES";
    public const string WipeDataCommand = "WIPEDATA";
    public const string ExitCommand = "EXIT";

    public const string FolderNotFoundForSpecifiedFile = "Specified folder didn't have file";
    public const string FileNotFound = "File not found";
    public const string SuccessFileDeleted = "Successfully deleted file";

    public const string FileAlreadyExists = "File with same name already exists";
    public const string SuccessFileAdded = "File added";

    public const string SuccessFoldersCreated = "Folders were created";

    public const string FolderNotFound = "Folder for deletion not found";
    public const string SuccessFolderDeleted = "Folder deleted successfully";

    public const string Prefix = "  ";

    public const string HelpString = "Application commands and examples:\n" +
        $"{ListFolderCommand}\n" +
        $"{WipeDataCommand}\n" +
        $"{ExitCommand}\n" +
        $"{AddFolderCommand} Example: {AddFolderCommand} Folder1\\Folder2\\Folder3\n" +
        $"{RemoveFolder} Example: {RemoveFolder} Folder1\\Folder2\\Folder3\n" +
        $"{AddFileCommand} Example: {AddFileCommand} Folder1\\Folder2\\Folder3 file.text\n" +
        $"{RemoveFileCommand} Example: {RemoveFileCommand} Folder1\\Folder2\\Folder3 file.text\n";
}