using static FileTransferTool.App.Processes.Validations.ErrorMessages;

namespace FileTransferTool.App.Processes.Validations.PathValidators.Implementation;

public class CharacterPathValidator : BasePathValidator
{
    public override bool IsPathValid(string path)
    {
        var invalidChars = Path.GetInvalidPathChars();
        var doesPathContainInvalidChars = path.Any(x => invalidChars.Contains(x));

        if (doesPathContainInvalidChars)
        {
            ErrorMessageDict.TryGetValue(ErrorMessages.ErrorKey.InvalidCharacters, out var errorMessage);
            Console.WriteLine(errorMessage ?? ErrorMessages.Default);
            
            return false;
        }
        
        return true;
    }
}