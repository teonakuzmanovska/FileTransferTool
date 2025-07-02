using static FileTransferTool.App.Processes.Validations.Input;

namespace FileTransferTool.App.Processes;

public class Input
{
    /// <summary>
    /// Validates path provided by the user. Prints error message if invalid. Returns path if valid.
    /// </summary>
    /// <returns></returns>
    public static string ReadFilePath()
    {
        var sourceFilePath = Console.ReadLine();
        
        // Null or empty path check. If validation fails, call function recursively.
        var isFileEmpty = IsPathNullOrEmpty(sourceFilePath);
        if(isFileEmpty) ReadFilePath();
        
        // Valid path check. If validation fails, call function recursively
        var isPathValid = IsPathValid(sourceFilePath);
        if (!isPathValid) ReadFilePath();
        
        return sourceFilePath;
    }
}