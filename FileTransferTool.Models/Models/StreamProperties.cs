namespace FileTransferTool.Models.Models;

public class StreamProperties
{
    public FileStream Stream { get; set; }
    
    public object StreamLock { get; set; }
    
    public string StreamPath { get; set; }
    
    
    public StreamProperties(FileStream stream, object streamLock, string streamPath)
    {
        Stream = stream;
        StreamLock = streamLock;
        StreamPath = streamPath;
    }
}