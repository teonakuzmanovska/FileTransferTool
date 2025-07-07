namespace FileTransferTool.Processes.Processes.Validations.PathValidators.Implementation;

public class ExistingPathValidator : BasePathValidator
{
    private bool IsCheckForFile { get; set; }

    public ExistingPathValidator(bool isCheckForFile)
    {
        IsCheckForFile = isCheckForFile;
    }
    
    public override bool IsPathValid(string path)
    {
        var doesPathExist = IsCheckForFile ? File.Exists(path) : Directory.Exists(path);

        if (!doesPathExist)
        {
            var errorMessageKey = IsCheckForFile ? ErrorMessages.ErrorKey.NonExistingFile : ErrorMessages.ErrorKey.NonExistingDirectory;
            
            ErrorMessages.ErrorMessageDict.TryGetValue(errorMessageKey, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return false;
        }
        
        return true;
    }
}