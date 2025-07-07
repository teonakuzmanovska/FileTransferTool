using FileTransferTool.Processes.Processes.Validations.PathValidators.Implementation;

namespace FileTransferTool.Processes.Processes.Validations;

public class InputValidationService
{
    private List<BasePathValidator> PathValidators { get; set; } = new();
    
    public InputValidationService(bool isCheckForFile)
    {
        PathValidators.Add(new NotEmptyFileValidator());
        PathValidators.Add(new CharacterPathValidator());
        PathValidators.Add(new AbsolutePathValidator());
        PathValidators.Add(new ExistingPathValidator(isCheckForFile: isCheckForFile));
        //PathValidators.Add(new FileSizeValidator(isCheckForFile: isCheckForFile));
    }
    
    public bool IsPathValid(string sourceFilePath)
    {
        return PathValidators.All(validator => validator.IsPathValid(sourceFilePath));
    }
}