namespace FileTransferTool.App.Processes.Helpers;

public class FileChunkDivider
{
    /// <summary>
    /// Returns dictionary of chunk positions with their contents.
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <returns></returns>
    public static Dictionary<long,byte[]> DivideFileIntoChunks(string sourceFilePath)
    {
        var result = new Dictionary<long,byte[]>();
        
        // Chunk size
        var oneMegaByte = 1024 * 1024;

        // Open stream for source file
        using FileStream stream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
        
        // Total size of the file
        var fileLength = stream.Length;

        while (stream.Position < stream.Length)
        {
            // Starting position of the current chunk. Could be less than 1Mb if it is the last chunk.
            var currentChunkPosition = stream.Position;

            // The expected number of bytes in the current chunk
            var numberOfBytesToRead = (int)Math.Min(oneMegaByte, fileLength - currentChunkPosition);

            // Physical place (buffer) for storing the chunk
            var currentChunk = new byte[numberOfBytesToRead];

            // Loading the read chunk into a buffer
            var _ = stream.Read(buffer: currentChunk, offset: 0, count: numberOfBytesToRead);
            
            result.Add(currentChunkPosition, currentChunk);
        }
        
        return result;
    }
}