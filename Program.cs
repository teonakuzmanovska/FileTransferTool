using System;
using System.IO;
using static FileTransferTool.Helpers.ByteArrayExtensions;

// Initial basic implementation of md5 - testing
var filePath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\TestInputFile.txt";
using var input = new FileStream(filePath, FileMode.Open);
var fileName = Path.GetFileName(filePath);

var md5Hash = input.ToMd5Hash().ToPrintableString();

Console.WriteLine($"{fileName} -> {input.Length} - {md5Hash}");