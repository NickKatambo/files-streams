using static System.Console;
using System.IO;
using System;

namespace DataProcessor
{
    internal class FileProcessor
    {
        private static readonly string BackupDirectoryName = "backup";
        private static readonly string InProgressDirectoryName = "processing";
        private static readonly string CompletedDirectoryName = "complete";

        public string InputFilePath { get; set; }

        public FileProcessor(string filePath)
        {
            InputFilePath = filePath;
        }

        public void Process()
        {
            WriteLine($"Begin process of {InputFilePath}");

            // Check if file exists
            if (!File.Exists(InputFilePath))
            {
                WriteLine($"ERROR: file {InputFilePath} does not exist.");
                return;
            }

            string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
            WriteLine($"Root data path is {rootDirectoryPath}");

            // Check if backup dir exists
            string inputFileDirectoryPath = Path.GetDirectoryName(InputFilePath);
            string backUpDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);

            Directory.CreateDirectory(backUpDirectoryPath);

            /*
            *if (!Directory.Exists(backUpDirectoryPath))
            {
                WriteLine($"Creating {backUpDirectoryPath}");
            }
            */

            // Copy file to backup dir
            string inputFileName = Path.GetFileName(InputFilePath);
            string backupFilePath = Path.Combine(backUpDirectoryPath, inputFileName);
            WriteLine($"Copying {InputFilePath} to {backUpDirectoryPath}");
            File.Copy(InputFilePath, backupFilePath, true);

            // Move to In progress dir
            Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
            string inProgressFilePath = Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);

            if (File.Exists(inProgressFilePath))
            {
                WriteLine($"ERROR: a file with the name {inProgressFilePath} is already being processed.");
                return;
            }

            WriteLine($"Moving {inputFileName} to {inProgressFilePath}");
            File.Move(InputFilePath, inProgressFilePath);

            // Determine type of file 
            string extesioin = Path.GetExtension(InputFilePath);

            string completedDirectoryPath = Path.Combine(rootDirectoryPath, CompletedDirectoryName);
            Directory.CreateDirectory(completedDirectoryPath);

            //WriteLine($"Moving {inProgressFilePath} to {completedDirectoryPath}");
            //File.Move(inProgressFilePath, Path.Combine(completedDirectoryPath, inputFileName));

            var completedFileName = $"{Path.GetFileNameWithoutExtension(InputFilePath)}-{Guid.NewGuid()}{extesioin}";
            var completedFilePath = Path.Combine(completedDirectoryPath, completedFileName);

            switch (extesioin)
            {
                case ".txt":
                    //ProcessTextFile(inProgressFilePath);
                    var textProcessor = new TextFileProcessor(inProgressFilePath, completedFilePath);
                    textProcessor.Process(2);
                    break;
                case ".data":
                    var binaryProcessor = new BinaryFileProcessor(inProgressFilePath, completedFilePath);
                    binaryProcessor.Process();
                    break;
                default:
                    WriteLine($"{extesioin} is an unsupported file type");
                    break;
            }

   
            WriteLine($"Completed processing of {inProgressFilePath}");

            WriteLine($"Deleting processing of {inProgressFilePath}");
            File.Delete(inProgressFilePath);

            //File.Move(inProgressFilePath, completedFilePath);

            // ** Prevent Inprogress directory from being deleted. **

            //string inProgressDirectoryPath = Path.GetDirectoryName(inProgressFilePath);
            //Directory.Delete(inProgressDirectoryPath, true);
        }

        /*private void ProcessTextFile(string inProgressFilePath)
        {
            WriteLine($"Processing text file {inProgressFilePath}");
            // Read in and process
        }
        */
    }
} 