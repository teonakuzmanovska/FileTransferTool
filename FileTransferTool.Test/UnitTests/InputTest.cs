using FileTransferTool.Processes.Processes.Validations;

namespace FileTransferTool.Test.UnitTests;

public class InputTest
{
    [Test]
    public void NotValidFileSizeTest()
    {
        var inputValidationService = new InputValidationService(isCheckForFile: true);
        
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string testFilePath = Path.Combine(desktopPath, "TestInputFile.txt");

        try
        {
            File.WriteAllText(testFilePath, string.Empty);
            Assert.IsFalse(inputValidationService.IsPathValid(testFilePath));
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
    
    [Test]
    public void NotValidFilePathTest()
    {
        var inputValidationService = new InputValidationService(isCheckForFile: true);
        
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string testFilePath = Path.Combine(desktopPath, "TestInputFile$*^<");

        Assert.IsFalse(inputValidationService.IsPathValid(testFilePath));
        
    }
    
    [Test]
    public void NotValidDirectoryPathTest()
    {
        var inputValidationService = new InputValidationService(isCheckForFile: false);

        var testDirectoryPath = "C:/*Test/File<>";
        
        Assert.IsFalse(inputValidationService.IsPathValid(testDirectoryPath));
    }
}