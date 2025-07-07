using FileTransferTool.Processes.Processes.Validations.PathValidators.Interface;

namespace FileTransferTool.Processes.Processes.Validations.PathValidators.Implementation;

public abstract class BasePathValidator : IBasePathValidator
{
    public abstract bool IsPathValid(string path);
}