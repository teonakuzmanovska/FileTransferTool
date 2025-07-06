using FileTransferTool.App.Helpers;

namespace FileTransferTool.App.Processes.HashingAlgorithms;

public class HashChunks
{
    /// <summary>
    /// Returns dictionary of chunk hashes and their positions.
    /// </summary>
    /// <param name="sourceFile"></param>
    /// <returns></returns>
    public static Dictionary<long, KeyValuePair<string, byte[]>> GetHashedChunks(string sourceFile)
    {
        var result = new Dictionary<long, KeyValuePair<string, byte[]>>();
        var oneMegaByte = 1024 * 1024;

        using FileStream stream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
        
        var fileLength = stream.Length;
            
        while (stream.Position < stream.Length)
        {
            // Could be less than 1Mb if it is the last chunk.
            var currentChunkPosition = stream.Position;
                
            // The expected number of bytes in the current chunk
            var numberOfBytesToRead = (int)Math.Min(oneMegaByte, fileLength - currentChunkPosition);
                
            // Physical place for storing the chunk
            var currentChunk = new byte[numberOfBytesToRead];
                
            // The actual number of bytes read from the chunk
            var _ = stream.Read(buffer:currentChunk, offset:0, count:numberOfBytesToRead);
                
            // Key value pair of the chunk position and the chunk along its hash.
            var md5Hash = currentChunk.ToMd5().ToPrintableString();
            var chunkResult = new KeyValuePair<string,byte[]>(md5Hash, currentChunk);
            result.Add(currentChunkPosition, chunkResult);
        }

        return result;
    }
}