using FileTransferTool.Models.Helpers;
using FileTransferTool.Models.Models;
using FileTransferTool.Processes.Processes.FileChunks;
using FileTransferTool.Processes.Processes.Helpers;

namespace FileTransferTool.Processes.Processes.Files;

public class FileOperations
{
    /// <summary>
    /// Returns dictionary of chunk hashes and their positions.
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="destinationFilePath"></param>
    /// <returns></returns>
    public static Dictionary<long,string> TransferFile(string sourceFilePath, string destinationFilePath)
    {
        var result = new Dictionary<long,string>();

        // Divide the file into chunks of 1 MB
        var fileChunks = FileChunkDivider.DivideFileIntoChunks(sourceFilePath);
        
        // Group chunks into two batches
        var dividingPoint = fileChunks.Count() / 2;
        var firstBatch = fileChunks.Take(dividingPoint).ToList();
        var secondBatch = fileChunks.Skip(dividingPoint).ToList();

        // Define a lock for the dictionary storing the chunk postions and hashes
        var resultLock = new object();
        var streamLock = new object();
        
        // Open a stream for transferring the blocks.
        using FileStream sharedStream = new FileStream(destinationFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        var sharedStreamProperties = new StreamProperties(sharedStream, streamLock, destinationFilePath);

        // Transfer the two chunks with two threads.
        var thread1 = new Thread(() => TransferChunksBatch(sharedStreamProperties, firstBatch, result, resultLock));
        var thread2 = new Thread(() => TransferChunksBatch(sharedStreamProperties, secondBatch, result, resultLock));

        thread1.Start();
        thread2.Start();
        
        thread1.Join();
        thread2.Join();

        // Return all chunks' checksums with their positions
        return result;
    }
    
    /// <summary>
    /// Transfers all chunks in a block 
    /// </summary>
    /// <param name="streamProperties"></param>
    /// <param name="chunkList"></param>
    /// <param name="result"></param>
    /// <param name="resultLock"></param>
    public static void TransferChunksBatch(StreamProperties streamProperties, List<FileChunkContents> chunkList, Dictionary<long,string> result, object resultLock)
    {
        chunkList.ForEach(currentChunk =>
        {
            // Transfers the chunk and returns true/false depending on whether the transfer was successful.
            var isTransferSuccessful = FileChunkOperations.TransferChunk(streamProperties, currentChunk);

            // Closes the program after several unsiccessful attempts to transfer the file.
            if (!isTransferSuccessful)
            {
                Console.WriteLine("Failed to transfer file. Closing program in 3 seconds.");
                Thread.Sleep(3000);
                Environment.Exit(-1);
            }

            lock(resultLock)
            {
                // Loads a key value pair of the chunk position and its MD5 hash.
                result.Add(currentChunk.Position, currentChunk.Md5HashValue);
            }
        });
    }

    public static string GetMd5HashFromFile(string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        var fileSha = fileStream.ToSha256().ToPrintableString();
        
        return fileSha;
    }
}