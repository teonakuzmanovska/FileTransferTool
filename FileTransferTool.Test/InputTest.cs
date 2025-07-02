using Input = FileTransferTool.App.Processes.Validations.Input;

namespace FileTransferTool.Test;

public class InputTest
{
    [Test]
    public void ValidFilePathTest()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string testFilePath = Path.Combine(desktopPath, "TestInputFile.txt");

        try
        {
            File.WriteAllText(testFilePath, string.Empty);
            Assert.IsTrue(Input.IsPathValid(testFilePath, isCheckForFile: true));
        }

        finally
        {
            if (File.Exists(testFilePath)) File.Delete(testFilePath);
        }
        
    }
    
    [Test]
    public void ValidDirectoryPathTest()
    {
        var testDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Assert.IsTrue(Input.IsPathValid(testDirectoryPath, isCheckForFile: false));
    }
}