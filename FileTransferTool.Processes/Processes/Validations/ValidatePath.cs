namespace FileTransferTool.Processes.Processes.Validations;

public class ValidatePath
{
    public static bool IsPathValid(string path, bool isCheckForFile)
    {
        // Null or empty path check. If validation fails, call function recursively.
        var inputValidationService = new InputValidationService(isCheckForFile: isCheckForFile);
        return inputValidationService.IsPathValid(path);
    }
}