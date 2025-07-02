using FileTransferTool.App.Processes.Validations;

namespace FileTransferTool.Test;

public class InputTest
{
    [Test]
    public void ValidFilePathTest()
    {
        var inputValidationService = new InputValidationService(isCheckForFile: true);
        
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string testFilePath = Path.Combine(desktopPath, "TestInputFile.txt");

        try
        {
            File.WriteAllText(testFilePath, string.Empty);
            Assert.IsTrue(inputValidationService.IsPathValid(testFilePath));
        }

        finally
        {
            if (File.Exists(testFilePath)) File.Delete(testFilePath);
        }
        
    }
    
    [Test]
    public void ValidDirectoryPathTest()
    {
        var inputValidationService = new InputValidationService(isCheckForFile: false);

        var testDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Assert.IsTrue(inputValidationService.IsPathValid(testDirectoryPath));
    }
}