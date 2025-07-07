namespace FileTransferTool.Processes.Processes.Helpers;

public class FilePathGenerator
{
    /// <summary>
    /// Generates a unique destination path name from the source file name.
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="destinationPath"></param>
    /// <returns></returns>
    public static string GenerateDestinationFilePath(string sourceFilePath, string destinationPath)
    {
        var sourceFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
        var sourceFileExtension = Path.GetExtension(sourceFilePath);
        
        var fullDestinationPath = Path.Combine(destinationPath, Path.GetFileName(sourceFilePath));

        // Return the path to the new file if no file with the same name exists.
        if (!Path.Exists(fullDestinationPath)) return fullDestinationPath;

        var index = 1;
        
        // Generate new file names until no file with such name exists.
        while (Path.Exists(fullDestinationPath))
        {
            fullDestinationPath = Path.Combine(destinationPath, $"{sourceFileName}({index}){sourceFileExtension}");
            index++;
        }
        
        return fullDestinationPath;
    }
}