using static FileTransferTool.App.Helpers.ByteArrayExtensions;
using static FileTransferTool.App.Processes.Input;

// Initial basic implementation of md5 - testing
var filePath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\TestInputFile.txt";
using var input = new FileStream(filePath, FileMode.Open);
var fileName = Path.GetFileName(filePath);

Console.WriteLine("Please enter a complete path of the file you would like to transfer:");
Console.WriteLine("e.g. C:\\Users\\YourUserName\\SourceFileFolder\\FileName.txt");

var sourceFile = ReadFilePath();

Console.WriteLine("Please enter the destination to which you would like to transfer:");
Console.WriteLine("e.g. C:\\Users\\YourUserName\\DestinationFolder\\");

var destinationFile = ReadFilePath();







var hashedSourceFile = input.ToSha256().ToPrintableString();

Console.WriteLine($"{fileName} -> {input.Length} - {hashedSourceFile}");