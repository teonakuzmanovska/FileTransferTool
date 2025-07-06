using System.IO;
using System.Security.Cryptography;

namespace FileTransferTool.App.Helpers;

public static class ByteArrayExtensions
{
    /// <summary>
    /// Hashes stream to MD5 checksum
    /// </summary>
    /// <param name="byteArray"></param>
    /// <returns>byte[]</returns>
    public static byte[] ToMd5(this byte[] byteArray)
    {
        return MD5.Create().ComputeHash(byteArray);
    }
    
    /// <summary>
    /// Hashes stream to SHA256 checksum
    /// </summary>
    /// <param name="stream"></param>
    /// <returns>byte[]</returns>
    public static byte[] ToSha256(this Stream stream)
    {
        return SHA256.Create().ComputeHash(stream);
    }
    
    /// <summary>
    /// Joins byte array elements to printable string
    /// </summary>
    /// <param name="byteArray"></param>
    /// <returns>string</returns>
    public static string ToPrintableString(this byte[] byteArray)
    {
        var stringResult = byteArray.ToList().Select(x =>{ return x.ToString("X2"); });
    
        return string.Join("", stringResult);
    }
}