using FileTransferTool.App.Processes.Helpers;

namespace FileTransferTool.App.Processes.FileChunks;

public class FileChunkOperations
{
    /// <summary>
    /// Transfers a chunk from source to destination.
    /// Returns true if successfully transferred, else tries 2 more times before returning false.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="position"></param>
    /// <param name="chunk"></param>
    /// <param name="trialNumber"></param>
    /// <returns></returns>
    public static bool TransferChunk(FileStream stream, object streamLock, string destination, long position, byte[] chunk, int trialNumber = 0)
    {
        var sourceChunkChecksum = chunk.ToMd5().ToPrintableString();

        lock (streamLock)
        {
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
        }

        return TransferChunk(stream, streamLock, destination, position, chunk, trialNumber + 1);
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