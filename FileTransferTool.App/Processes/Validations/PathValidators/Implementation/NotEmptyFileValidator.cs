namespace FileTransferTool.App.Processes.Validations.PathValidators.Implementation;

public class NotEmptyFileValidator : BasePathValidator
{
    public override bool IsPathValid(string path)
    {
        var isFileEmpty = string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path);
        
        if (isFileEmpty)
        {
            ErrorMessages.ErrorMessageDict.TryGetValue(ErrorMessages.ErrorKey.EmptyPath, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return false;
        }
        
        return true;
    }
}