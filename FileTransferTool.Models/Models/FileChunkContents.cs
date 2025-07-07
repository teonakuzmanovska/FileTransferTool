using FileTransferTool.Models.Helpers;

namespace FileTransferTool.Models.Models;

public class FileChunkContents 
{
    public long Position { get; set; }
    
    public byte[] Contents { get; set; }
    
    public string Md5HashValue { get; set; }
    
    public FileChunkContents(long position, byte[] contents)
    {
        Position = position;
        Contents = contents;
        Md5HashValue = contents.ToMd5().ToPrintableString();
    }
}