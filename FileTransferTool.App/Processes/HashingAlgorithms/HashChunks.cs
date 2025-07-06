using FileTransferTool.App.Processes.Helpers;

namespace FileTransferTool.App.Processes.HashingAlgorithms;

public class HashChunks
{
    /// <summary>
    /// Returns dictionary of chunk hashes and their positions.
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="destinationPath"></param>
    /// <returns></returns>
    public static Dictionary<long,string> TransferFile(string sourceFilePath, string destinationPath)
    {
        var result = new Dictionary<long,string>();
        
        // Chunk size
        var oneMegaByte = 1024 * 1024;

        // Open stream for source file
        using FileStream stream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
        
        // Total size of the file
        var fileLength = stream.Length;
            
        // Full path and name of the file to be stored in the destination
        var fullDestinationPath = FilePathGenerator.GenerateDestinationFilePath(sourceFilePath, destinationPath);
        
        while (stream.Position < stream.Length)
        {
            // Starting position of the current chunk. Could be less than 1Mb if it is the last chunk.
            var currentChunkPosition = stream.Position;
                
            // The expected number of bytes in the current chunk
            var numberOfBytesToRead = (int)Math.Min(oneMegaByte, fileLength - currentChunkPosition);
                
            // Physical place (buffer) for storing the chunk
            var currentChunk = new byte[numberOfBytesToRead];
                
            // Loading the read chunk into a buffer
            var _ = stream.Read(buffer:currentChunk, offset:0, count:numberOfBytesToRead);
                
            // MD5 hash of the chunk
            var md5Hash = currentChunk.ToMd5().ToPrintableString();
            
            // Transfers the chunk and returns true/false depending on whether the transfer was successful.
            var isTransferSuccessful = TransferChunk(destination:fullDestinationPath, position:currentChunkPosition, chunk: currentChunk);

            // Closes the program after several unsiccessful attempts to transfer the file.
            if (!isTransferSuccessful)
            {
                Console.WriteLine("Failed to transfer file. Closing program in 3 seconds.");
                Thread.Sleep(3000);
                Environment.Exit(-1);
            }
            
            // Loads a key value pair of the chunk position and its MD5 hash.
            result.Add(currentChunkPosition, md5Hash);
        }

        return result;
    }

    /// <summary>
    /// Transfers a chunk from source to destination.
    /// Returns true if successfully transferred, else tries 2 more times before returning false.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="position"></param>
    /// <param name="chunk"></param>
    /// <param name="trialNumber"></param>
    /// <returns></returns>
    public static bool TransferChunk(string destination, long position, byte[] chunk, int trialNumber = 0)
    {
        var sourceChunkChecksum = chunk.ToMd5().ToPrintableString();
        
        using FileStream stream = new FileStream(destination, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        stream.Position = position;
        stream.Write(chunk, 0, chunk.Length);
        stream.Flush();
        
        // Reset stream position to read the written file
        stream.Position = position;
        var chunkFromDestination = GetChunkFromDestination(stream, position, numberOfBytesToRead: chunk.Length);
        
        var destinationChunkChecksum = chunkFromDestination.ToMd5().ToPrintableString();
        
        // validate hashes
        if (sourceChunkChecksum == destinationChunkChecksum)
        {
            return true;
        }
        
        if (trialNumber == 3)
        {
            stream.Position = 0;
            stream.Flush();
            File.Delete(destination);
            return false;
        }

        return TransferChunk(destination, position, chunk, trialNumber + 1);
    }

    /// <summary>
    /// Retrieves a chunk from a destination.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="position"></param>
    /// <param name="numberOfBytesToRead"></param>
    /// <returns></returns>
    public static byte[] GetChunkFromDestination(Stream stream, long position, int numberOfBytesToRead)
    {
        var chunkFromDestination = new byte[numberOfBytesToRead];
        
        stream.Position = position;
        var _ = stream.Read(buffer:chunkFromDestination, offset:0, count:numberOfBytesToRead);
        
        return chunkFromDestination;
    }
}