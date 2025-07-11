﻿using FileTransferTool.Models.Helpers;
using FileTransferTool.Models.Models;
using FileTransferTool.Processes.Processes.Files;
using FileTransferTool.Processes.Processes.Helpers;
using FileTransferTool.Processes.Processes.Output;
using static FileTransferTool.Processes.Processes.FileChunks.FileChunkOperations;

namespace FileTransferTool.Test;

public class ChunkHashTest
{
    [Test,Timeout(30000)]
    public void CorrectChunkHashTest()
    {
        // TODO: customize path. Create file and destination folder on desktop first.
        var sourceFilePath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\test100MB.txt";
        var destinationPath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\destination";
        
        var fullDestinationPath = FilePathGenerator.GenerateDestinationFilePath(sourceFilePath, destinationPath);
        
        using var stream = new FileStream(sourceFilePath, FileMode.Open);
        var chunkSize = 1024 * 1024;
        var expectedNumberOfChunks = (stream.Length + chunkSize - 1) / chunkSize;

        var firstChunk = GetChunkFromDestination(stream, new FileChunkContents(0, new byte[chunkSize]));
        var expectedHashOfFirstBlock = firstChunk.ToMd5().ToPrintableString();
        
        stream.Close();

        try
        {
            var hashedChunks = FileOperations.TransferFile(sourceFilePath, fullDestinationPath);
            Output.PrintChunksChecksums(hashedChunks);

            var actualNumberOfChunks = hashedChunks.Count;
            Assert.That(expectedNumberOfChunks, Is.EqualTo(actualNumberOfChunks));

            var actualHashOfFirstBlock = hashedChunks.FirstOrDefault(x => x.Key == 0).Value;
            Assert.That(expectedHashOfFirstBlock, Is.EqualTo(actualHashOfFirstBlock));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (!Path.Exists(fullDestinationPath)) Assert.Fail();
            
            File.Delete(fullDestinationPath);
        }
    }
}