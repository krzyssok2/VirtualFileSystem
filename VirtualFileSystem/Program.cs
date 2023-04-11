using VirtualFileSystem.Constants;
using VirtualFileSystem.Services;

StorageService storageService = new();
VirtualFileService virtualFileService = new(storageService);

Console.WriteLine(ApplicationConstants.HelpString);

while (true)
{
    Console.Write(">");
    var command = Console.ReadLine();

    var lines = command.Split(' ');

    if (lines.Length == 0)
    {
        continue;
    }

    switch (lines[0].ToUpper())
    {
        case ApplicationConstants.AddFolderCommand:
            if (lines.Length != 2)
            {
                Console.WriteLine($"Invalid amount of arguments for:{lines[0]}");
                continue;
            }

            var folders = lines[1].Split('\\', '/');

            virtualFileService.AddFolders(folders);

            break;

        case ApplicationConstants.RemoveFolder:
            if (lines.Length != 2)
            {
                Console.WriteLine($"Invalid amount of arguments for:{lines[0]}");
                continue;
            }

            var folders2 = lines[1].Split('\\', '/');

            virtualFileService.DeleteFolder(folders2);

            break;

        case ApplicationConstants.ListFolderCommand:
            virtualFileService.ListFolders();
            break;

        case ApplicationConstants.AddFileCommand:
            if (lines.Length != 3)
            {
                Console.WriteLine($"Invalid amount of arguments for:{lines[0]}");
                continue;
            }

            var folders3 = lines[1].Split('\\', '/');

            virtualFileService.AddFile(folders3, lines[2]);

            break;

        case ApplicationConstants.RemoveFileCommand:
            if (lines.Length != 3)
            {
                Console.WriteLine($"Invalid amount of arguments for:{lines[0]}");
                continue;
            }

            var folders4 = lines[1].Split('\\', '/');

            virtualFileService.RemoveFile(folders4, lines[2]);
            break;

        case ApplicationConstants.ListFilesCommand:
            if (lines.Length != 2)
            {
                Console.WriteLine($"Invalid amount of arguments for:{lines[0]}");
                continue;
            }

            var folders5 = lines[1].Split('\\', '/');

            virtualFileService.ListFiles(folders5);
            break;

        case ApplicationConstants.WipeDataCommand:
            virtualFileService.HardWipe();
            return;

        case ApplicationConstants.ExitCommand:
            virtualFileService.SaveAndExit();
            return;

        default:
            Console.WriteLine("Command doesn't exist");
            break;
    }
}