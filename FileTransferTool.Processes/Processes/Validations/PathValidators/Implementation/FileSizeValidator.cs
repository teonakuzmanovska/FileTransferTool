namespace FileTransferTool.Processes.Processes.Validations.PathValidators.Implementation;

public class FileSizeValidator : BasePathValidator
{
    private bool IsCheckForFile { get; set; }

    public FileSizeValidator(bool isCheckForFile)
    {
        IsCheckForFile = isCheckForFile;
    }
    
    public override bool IsPathValid(string path)
    {
        if(!IsCheckForFile) return true;
        
        var oneGigaByte = 1024L * 1024L * 1024L;

        var doesSizeSatisfyMinimum = new FileInfo(path).Length >= oneGigaByte;

        if (!doesSizeSatisfyMinimum)
        {
            ErrorMessages.ErrorMessageDict.TryGetValue(ErrorMessages.ErrorKey.MinimumSizeNotSatisfied, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return false;
        }

        return true;
    }
}