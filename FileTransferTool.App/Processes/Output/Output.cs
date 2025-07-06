namespace FileTransferTool.App.Processes.Output;

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
        var printableCheckSums = chunkPositionsAndHashSums.Select(x =>
        {
            return $"position = {x.Key}, hash = {x.Value}";
        });

        Console.Write(string.Join(Environment.NewLine, printableCheckSums));
    }
}