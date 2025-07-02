using static FileTransferTool.Processes.Validations.ErrorMessages;

namespace FileTransferTool.Processes.Validations;

public static class Input
{
    /// <summary>
    /// Validates the path
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="isCheckForFile"></param>
    /// <returns></returns>
    public static bool IsPathValid(string sourceFilePath, bool isCheckForFile = false)
    {
        // Invalid characters check
        var doesFileContainInvalidCharacters = DoesPathContainInvalidCharacters(sourceFilePath);
        if(doesFileContainInvalidCharacters) return false;
        
        // Absolute path check
        var isAbsolutePath = IsAbsolutePath(sourceFilePath);
        if(!isAbsolutePath) return false;
        
        // Existing file check
        var doesFileExist = DoesPathExist(sourceFilePath, isCheckForFile);
        if(!doesFileExist) return false;

        return true;
    }

    /// <summary>
    /// Checks whether the path provided by the user is null or empty
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <returns></returns>
    public static bool IsPathNullOrEmpty(string? sourceFilePath)
    {
        var isFileEmpty = string.IsNullOrEmpty(sourceFilePath) || string.IsNullOrWhiteSpace(sourceFilePath);
        
        if (isFileEmpty)
        {
            ErrorMessageDict.TryGetValue(ErrorMessages.ErrorKey.EmptyPath, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks whether the path provided by the user contains invalid characters
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <returns></returns>
    private static bool DoesPathContainInvalidCharacters(string sourceFilePath)
    {
        var invalidChars = Path.GetInvalidPathChars();
        var doesPathContainInvalidChars = sourceFilePath.Any(x => invalidChars.Contains(x));

        if (doesPathContainInvalidChars)
        {
            ErrorMessageDict.TryGetValue(ErrorMessages.ErrorKey.InvalidCharacters, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Checks whether the file or directory provided by the user exists
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="isCheckForFile"></param>
    /// <returns></returns>
    private static bool DoesPathExist(string sourceFilePath, bool isCheckForFile)
    {
        var doesPathExist = isCheckForFile ? File.Exists(sourceFilePath) : Directory.Exists(sourceFilePath);

        if (!doesPathExist)
        {
            var errorMessageKey = isCheckForFile ? ErrorMessages.ErrorKey.NonExistingFile : ErrorMessages.ErrorKey.NonExistingDirectory;
            
            ErrorMessageDict.TryGetValue(errorMessageKey, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// Checks whether the user has provided absolute or rooted path
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <returns></returns>
    private static bool IsAbsolutePath(string sourceFilePath)
    {
        var isPathRooted = Path.IsPathRooted(sourceFilePath);
        if (!isPathRooted)
        {
            ErrorMessageDict.TryGetValue(ErrorMessages.ErrorKey.RelativePath, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return false;
        }

        return true;
    }
}