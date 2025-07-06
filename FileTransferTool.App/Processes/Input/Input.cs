using FileTransferTool.App.Processes.Validations;

namespace FileTransferTool.App.Processes.Input;

public class Input
{
    /// <summary>
    /// Validates path provided by the user. Prints error message if invalid. Returns path if valid.
    /// Expects flag stating whether the check is for file or folder.
    /// </summary>
    /// <returns></returns>
    public static string ReadFilePath(bool isCheckForFile)
    {
        var sourceFilePath = Console.ReadLine();
        
        // Null or empty path check. If validation fails, call function recursively.
        var inputValidationService = new InputValidationService(isCheckForFile: isCheckForFile);
        var isPathValid = inputValidationService.IsPathValid(sourceFilePath);

        if (isPathValid) ReadFilePath(isCheckForFile);
        
        return sourceFilePath;
    }
}