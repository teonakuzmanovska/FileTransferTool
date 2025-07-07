using FileTransferTool.Models.Helpers;
using FileTransferTool.Models.Models;

namespace FileTransferTool.Processes.Processes.FileChunks;

public class FileChunkOperations
{
    /// <summary>
    /// Transfers a chunk from source to destination.
    /// Returns true if successfully transferred, else tries 2 more times before returning false.
    /// </summary>
    /// <param name="streamProperties"></param>
    /// <param name="fileChunkContents"></param>
    /// <param name="trialNumber"></param>
    /// <returns></returns>
    public static bool TransferChunk(StreamProperties streamProperties, FileChunkContents fileChunkContents, int trialNumber = 0)
    {
        var sourceChunkChecksum = fileChunkContents.Md5HashValue;

        lock (streamProperties.StreamLock)
        {
            streamProperties.Stream.Position = fileChunkContents.Position;
            streamProperties.Stream.Write(fileChunkContents.Contents, 0, fileChunkContents.Contents.Length);
            streamProperties.Stream.Flush();
            
            // Reset stream position to read the written file
            streamProperties.Stream.Position = fileChunkContents.Position;
            var chunkFromDestination = GetChunkFromDestination(streamProperties.Stream, fileChunkContents);
        
            var destinationChunkChecksum = chunkFromDestination.ToMd5().ToPrintableString();
            
            // validate hashes
            if (sourceChunkChecksum == destinationChunkChecksum)
            {
                return true;
            }
            
            if (trialNumber == 3)
            {
                streamProperties.Stream.Position = 0;
                streamProperties.Stream.Flush();
                File.Delete(streamProperties.StreamPath);
                return false;
            }
        }

        return TransferChunk(streamProperties, fileChunkContents, trialNumber + 1);
    }

    /// <summary>
    /// Retrieves a chunk from a destination.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileChunkContents"></param>
    /// <returns></returns>
    public static byte[] GetChunkFromDestination(Stream stream, FileChunkContents fileChunkContents)
    {
        var chunkFromDestination = new byte[fileChunkContents.Contents.Length];
        
        stream.Position = fileChunkContents.Position;
        var _ = stream.Read(buffer:chunkFromDestination, offset:0, count:fileChunkContents.Contents.Length);
        
        return chunkFromDestination;
    }
}