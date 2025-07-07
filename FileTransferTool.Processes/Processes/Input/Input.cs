using FileTransferTool.Processes.Processes.Validations;

namespace FileTransferTool.Processes.Processes.Input;

public class Input
{
    /// <summary>
    /// Reads and validates a user-given path until a correct one is provided.
    /// </summary>
    /// <param name="isCheckForFile"></param>
    /// <returns></returns>
    public static string ReadPathWhileNotValid(bool isCheckForFile = false)
    {
        var path = string.Empty;
        var isPathValid = false;
        
        while (!isPathValid)
        {
            path = Console.ReadLine()?.Trim();
            isPathValid = ValidatePath.IsPathValid(path, isCheckForFile);
        }

        return path;
    }
}