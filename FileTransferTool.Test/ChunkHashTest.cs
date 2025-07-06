using FileTransferTool.App.Helpers;
using static FileTransferTool.App.Processes.HashingAlgorithms.HashChunks;

namespace FileTransferTool.Test;

public class ChunkHashTest
{
    [Test,Timeout(30000)]
    public void GetHashedChunksTest()
    {
        var sourceFile = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\test100MB.txt";

        using var input = new FileStream(sourceFile, FileMode.Open);
        var chunkSize = 1024 * 1024;
        var expectedNumberOfChunks = (input.Length + chunkSize - 1) / chunkSize;
        
        input.Close();
        
        try
        {
            var hashedChunks = GetHashedChunks(sourceFile);

            hashedChunks.ToList().ForEach(x => { Console.WriteLine($"position: {x.Key}; hash:{x.Value.Key}"); });
            
            Assert.That(hashedChunks.Count, Is.EqualTo(expectedNumberOfChunks));
            
            var expectedHashOfFirstBlock = hashedChunks[0].Value.ToMd5().ToPrintableString();
            var actualHashOfSecondBlock = hashedChunks[0].Key;
            
            Assert.That(expectedHashOfFirstBlock, Is.EqualTo(actualHashOfSecondBlock));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Assert.Fail(e.Message);
        }
    }
}