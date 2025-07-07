using FileTransferTool.Processes.Processes.Files;

namespace FileTransferTool.Processes.Processes.Output;

public class Output
{
    /// <summary>
    /// Prints simple instructions for user input
    /// </summary>
    public static void PrintSourceFilePathPrompt()
    {
        Console.WriteLine("Please enter a complete path of the file you would like to transfer:");
        Console.WriteLine("e.g. C:\\Users\\YourUserName\\SourceFileFolder\\FileName.txt");
    }
    
    /// <summary>
    /// Prints simple instructions for user input
    /// </summary>
    public static void PrintDestinationPathPrompt()
    {
        Console.WriteLine("Please enter the destination to which you would like to transfer:");
        Console.WriteLine("e.g. C:\\Users\\YourUserName\\DestinationFolder\\");
    }

    /// <summary>
    /// Joins file chunk checksums into printable format
    /// </summary>
    /// <param name="chunkPositionsAndHashSums"></param>
    public static void PrintChunksChecksums(Dictionary<long, string> chunkPositionsAndHashSums)
    {
        var printableCheckSums = chunkPositionsAndHashSums
            .OrderBy(x => x.Key)
            .Select(x =>
        {
            return $"position = {x.Key}, hash = {x.Value}";
        });

        Console.Write(string.Join(Environment.NewLine, printableCheckSums));
    }
    
    /// <summary>
    /// Compares and prints SHA256 checksums of source and destination file. 
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="destinationFilePath"></param>
    public static void CompareAndPrintSourceAndDestinationFileChecksums(string sourceFilePath, string destinationFilePath)
    {
        var sourceFileChecksum = FileOperations.GetMd5HashFromFile(sourceFilePath);
        var destinationFileChecksum = FileOperations.GetMd5HashFromFile(destinationFilePath);
        
        var areChecksumsEqual = sourceFileChecksum == destinationFileChecksum;
        
        Console.WriteLine($"Source file and destination file path checksums are{(areChecksumsEqual ? " " : " not ")}equal." + Environment.NewLine);
        Console.WriteLine($"Source file checksum: {sourceFileChecksum}" + Environment.NewLine);
        Console.WriteLine($"Destination file checksum: {destinationFileChecksum}" + Environment.NewLine);
    }
}