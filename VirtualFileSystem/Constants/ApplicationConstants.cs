namespace VirtualFileSystem.Constants;

public static class ApplicationConstants
{
    public const string StorageJson = "dataStorage.json";

    public const string RootName = "Root";

    public const string AddFolderCommand = "AddFolder";
    public const string RemoveFolder = "RemoveFolder";
    public const string ListFolderCommand = "ListFolders";

    public const string AddFileCommand = "AddFile";
    public const string RemoveFileCommand = "RemoveFile";
    public const string ListFilesCommand = "ListFiles";
    public const string WipeDataCommand = "WipeData";
    public const string ExitCommand = "Exit";

    public const string FolderNotFoundForSpecifiedFile = "Specified folder didn't have file";
    public const string FileNotFound = "File not found";
    public const string SuccessFileDeleted = "Successfully deleted file";

    public const string FileAlreadyExists = "File with same name already exists";
    public const string SuccessFileAdded = "File added";

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