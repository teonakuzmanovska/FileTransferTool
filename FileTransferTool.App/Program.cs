using FileTransferTool.Processes.Processes.Files;
using FileTransferTool.Processes.Processes.Input;
using FileTransferTool.Processes.Processes.Output;

Output.PrintSourceFilePathPrompt();
// Read user input until a valid source file is provided.
var sourceFilePath = Input.ReadPathWhileNotValid(isCheckForFile: true);

Output.PrintDestinationPathPrompt();
// Read user input until a valid destination is provided.
var destinationPath = Input.ReadPathWhileNotValid(isCheckForFile: false);

// Print MD5 checksums after the whole file is successfully transferred chunk by chunk.
var chunkPositionsAndHashSums = FileOperations.TransferFile(sourceFilePath, destinationPath);
Output.PrintChunksChecksums(chunkPositionsAndHashSums);

// Print SHA256 checksums of the source and destination files.
// TODO: call SHA256 algorithm

Console.WriteLine("\nPress Enter to exit...");
Console.ReadLine();