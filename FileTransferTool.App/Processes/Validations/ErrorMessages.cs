namespace FileTransferTool.App.Processes.Validations;

public static class ErrorMessages
{
    public const string Default = "Processing error. Please try again.";
    
    public static readonly Dictionary<ErrorKey,string> ErrorMessageDict = new()
    {
        {ErrorKey.EmptyPath, "The path is empty."},
        {ErrorKey.InvalidCharacters, "The path contains an invalid character."},
        {ErrorKey.RelativePath, "Please provide the full path to the file."},
        {ErrorKey.NonExistingFile, "The file does not exist."},
        {ErrorKey.NonExistingDirectory, "The destination path does not exist."},
    };
    
    public enum ErrorKey
    {
        EmptyPath,
        InvalidCharacters,
        RelativePath,
        NonExistingFile,
        NonExistingDirectory
    }
}