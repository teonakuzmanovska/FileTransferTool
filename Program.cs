using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

// Initial basic implementation of md5 - testing
var filePath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\TestInputFile.txt";
using var input = new FileStream(filePath, FileMode.Open);
var fileName = Path.GetFileName(filePath);

var md5Hash = ByteArrayToString(MD5.Create().ComputeHash(input));

Console.WriteLine($"{fileName} -> {input.Length} - {md5Hash}");

byte[] GetMd5Hash(Stream stream)
{
    return MD5.Create().ComputeHash(stream);
}

string ByteArrayToString(byte[] byteArray)
{
    var stringResult = byteArray.ToList().Select(x =>
    {
        return x.ToString("X2");
    });
    
    return string.Join("", stringResult);
}

/* IMPLEMENTATION PLAN
 * Create a File Transfer Tool. Steps:
 * 
 * 1. Import a file from a source
 *      a. Hash file using Secure Hash Algorithm (SHA1/SHA256)
 *      b. Divides the file into chunks (blocks) of 1 Mb.
 *      c. Iterates each block, hashes it using MD5 (Message Digest Algorithm 5), transfers the block, compares src and dest hash.
 *          c.1. Hash the source block and keep it for later comparison
 *          c.2. Send the block to the destination
 *          c.3. Take the block written on the destination
 *          c.4. Hash the block taken from the destination
 *          c.5. Validate transfer by comparing the dest hash with the src hash (check for corruption)
 *              c.5.1. If the block does not match - override block (allow only a few attempts)
 *              c.5.2. If the block matches - Print source hash with its position and iterate next block
 *              c.5.3. Else (maximum attempts) - Print error message
 * 2. Take the whole file from the destination
 *      a. Hash file using Secure Hash Algorithm (SHA1/SHA256)
 *      b. Display both src file and dest file hashes (checksums)
 * 
 * *** use threading
 *      
*/