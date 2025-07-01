using System.IO;
using System.Security.Cryptography;

namespace FileTransferTool.Helpers;

public static class ByteArrayExtensions
{
    /// <summary>
    /// Hashes stream to md5 checksum
    /// </summary>
    /// <param name="stream"></param>
    /// <returns>byte[]</returns>
    public static byte[] ToMd5Hash(this Stream stream)
    {
        return MD5.Create().ComputeHash(stream);
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