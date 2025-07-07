using FileTransferTool.Processes.Processes.Files;
using FileTransferTool.Processes.Processes.Helpers;

namespace FileTransferTool.Test.UnitTests;

public class FileHashTest
{
    [Test]
    public void AreSrcAndDstHashesEqual()
    {
        // TODO: customize path. Create file and destination folder on desktop first.
        var sourceFilePath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\test100MB.txt";
        var destinationPath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\destination";
        
        var fullDestinationPath = FilePathGenerator.GenerateDestinationFilePath(sourceFilePath, destinationPath);

        try
        {
            var _ = FileOperations.TransferFile(sourceFilePath, fullDestinationPath);

            var sourceFileChecksum = FileOperations.GetMd5HashFromFile(sourceFilePath);
            var destinationFileChecksum = FileOperations.GetMd5HashFromFile(fullDestinationPath);
            
            Assert.That(sourceFileChecksum, Is.Not.Empty);
            Assert.That(destinationFileChecksum, Is.Not.Empty);
            Assert.That(destinationFileChecksum, Is.EqualTo(sourceFileChecksum));
        }
        finally
        {
            if (!Path.Exists(fullDestinationPath)) Assert.Fail();
            
            File.Delete(fullDestinationPath);
        }
        
    }
}