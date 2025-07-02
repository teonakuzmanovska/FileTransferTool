using FileTransferTool.App.Processes.Validations.PathValidators.Interface;

namespace FileTransferTool.App.Processes.Validations.PathValidators.Implementation;

public abstract class BasePathValidator : IBasePathValidator
{
    public abstract bool IsPathValid(string path);
}