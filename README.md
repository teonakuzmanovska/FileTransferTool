## File Transfer Tool

File Transfer Tool is a program which performs in-memory file transfer from one location to another.

### Acronyms
- SHA256 - Secure Hash Algorithm 256-bit.
- MD5 - Message Digest Algorithm 5

### Context

1. The program reads the file from the source provided by the user.
2. The file is hashed using SHA256 and the checksum is kept for printing at the end.
3. The file is divided into chunks (blocks) of 1 Mb.
4. Each block is iterated and:
    1. The source block is hashed using MD5 and the checksum is kept for later comparison with the destination block's checksum.
    2. The block is sent to the destination.
    3. The block in the destination is being taken for comparison with the source block.
    4. The destination block is hashed using MD5.
    5. A validation for corruption is performed by comparing the destination block hash with the source block hash.
        1. If the blocks do not match - the destination block is overwritten (up to 3 attempts).
        2. If the blocks match - The source hash with its position is printed.
        3. Else (if maximum attempts are reached) - An error message is printed.
    6. The next block is iterated. 
5. The complete transferred file is taken from the destination.
6. The file is hashed using SHA256.
7. Both the source and destination file's checksums are printed.

### Setup and user guide
1. Clone the project to your local machine.
2. Build the project
3. Run the project
4. You will be prompted to input the full path of the file you would like to transfer, as well as the desired destination folder.
    1. Copy the absolute path of the source file.
    2. Paste the file path to the console when prompted.
    3. Copy the destination folder's path.
    4. Paste the folder path to the console when prompted.

### Prerequisites:
- Have .NET 8 and .NET IDE installed on your local machine. I recommend JetBrains' IDE Rider.

