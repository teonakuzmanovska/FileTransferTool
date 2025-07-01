## File Transfer Tool

File Transfer Tool is a program which performs in-memory file transfer from one location to another.

### Context

1. The program reads the file from the source provided by the user.
    2. The file is hashed using Secure Hash Algorithm (SHA1/SHA256).
    3. The file is being divided into chunks (blocks) of 1 Mb.
        1. Each block is iterated and hashed using MD5 (Message Digest Algorithm 5). The block is transferred and its checksum is compared with the source block's hash.
            1. The source block is hashed using MD5 and the checksum is kept for later comparison with the destination block's checksum.
            2. The block is sent to the destination.
            3. The block in the destination is being taken for comparison with the source block.
            4. The destination block is hashed using MD5.
            5. A validation for corruption is performed by comparing the dest. hash with the src. hash.
            6. If the block does not match - the block is overwritten (up to 3 attempts).
            7. If the block matches - The source hash with its position is printed and the next block is iterated.
            8. Else (if maximum attempts are reached) - An error message is printed.
2. The whole file from the destination is taken.
    1. The file is hashed Secure Hash Algorithm (SHA1/SHA256)
3. Both the source and destination file's checksums are printed.

### Setup and user guide
1. Clone the project to your local machine.
2. Build the project
3. Run the project
4. You will be prompted to input the full path of the file you would like to transfer, as well as the desired destination folder.
   1. Copy the file's absolute path.
   2. Paste the file path to the console.
   3. Copy the destination folder's path.
   4. Paste the folder path to the console.

### Prerequisites:
- Have .NET 8 and .NET IDE installed on your local machine. I recommend JetBrain's IDE Rider.

