using VirtualFileSystem.Constants;
using VirtualFileSystem.Services;

StorageService storageService = new();
VirtualFileService virtualFileService = new(storageService);

Console.WriteLine(ApplicationConstants.HelpString);

string result;
string[] folders;
while (true)
{
    Console.Write(">>");
    var input = Console.ReadLine();

    var arguments = input.Split(' ');

    if (arguments.Length == 0)
    {
        continue;
    }

    switch (arguments[0].ToUpper())
    {
        case ApplicationConstants.AddFolderCommand:
            if (arguments.Length != 2)
            {
                Console.WriteLine($"Invalid amount of arguments for:{arguments[0]}");
                continue;
            }

            folders = arguments[1].Split('\\', '/');

            result = virtualFileService.AddFolders(folders);
            Console.WriteLine(result);
            break;

        case ApplicationConstants.RemoveFolder:
            if (arguments.Length != 2)
            {
                Console.WriteLine($"Invalid amount of arguments for:{arguments[0]}");
                continue;
            }

            folders = arguments[1].Split('\\', '/');

            result = virtualFileService.DeleteFolder(folders);
            Console.WriteLine(result);
            break;

        case ApplicationConstants.ListFolderCommand:
            virtualFileService.ListFolders();
            break;

        case ApplicationConstants.AddFileCommand:
            if (arguments.Length != 3)
            {
                Console.WriteLine($"Invalid amount of arguments for:{arguments[0]}");
                continue;
            }

            folders = arguments[1].Split('\\', '/');

            result = virtualFileService.AddFile(folders, arguments[2]);
            Console.WriteLine(result);
            break;

        case ApplicationConstants.RemoveFileCommand:
            if (arguments.Length != 3)
            {
                Console.WriteLine($"Invalid amount of arguments for:{arguments[0]}");
                continue;
            }

            folders = arguments[1].Split('\\', '/');

            result = virtualFileService.RemoveFile(folders, arguments[2]);
            Console.WriteLine(result);
            break;

        case ApplicationConstants.ListFilesCommand:
            if (arguments.Length != 2)
            {
                Console.WriteLine($"Invalid amount of arguments for:{arguments[0]}");
                continue;
            }

            var folders5 = arguments[1].Split('\\', '/');

            result = virtualFileService.ListFiles(folders5);
            Console.Write(result);
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