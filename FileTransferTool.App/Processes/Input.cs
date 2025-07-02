using FileTransferTool.App.Processes.Validations;
using static FileTransferTool.App.Processes.Validations.InputValidationService;

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
        var inputValidationService = new InputValidationService(isCheckForFile: true);
        var isPathValid = inputValidationService.IsPathValid(sourceFilePath);

        if (isPathValid) ReadFilePath();
        
        return sourceFilePath;
    }
}