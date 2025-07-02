namespace FileTransferTool.App.Processes.Validations.PathValidators.Interface;

public interface IBasePathValidator
{
    bool IsPathValid(string path);
}