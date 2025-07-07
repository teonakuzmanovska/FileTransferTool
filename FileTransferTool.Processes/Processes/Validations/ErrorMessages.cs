namespace FileTransferTool.Processes.Processes.Validations;

public static class ErrorMessages
{
    public const string Default = "Processing error. Please try again.";
    
    public static readonly Dictionary<ErrorKey,string> ErrorMessageDict = new()
    {
        {ErrorKey.EmptyPath, "The path is empty. Please try again."},
        {ErrorKey.InvalidCharacters, "The path contains an invalid character. Please try again."},
        {ErrorKey.RelativePath, "Please provide the full path to the file. Please try again."},
        {ErrorKey.NonExistingFile, "The file does not exist. Please try again."},
        {ErrorKey.NonExistingDirectory, "The destination path does not exist. Please try again."},
        {ErrorKey.MinimumSizeNotSatisfied, "The file must be of minimum size 1 GB. Please provide another file."}
    };
    
    public enum ErrorKey
    {
        EmptyPath,
        InvalidCharacters,
        RelativePath,
        NonExistingFile,
        NonExistingDirectory,
        MinimumSizeNotSatisfied
    }
}