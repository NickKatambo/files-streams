using static System.Console;
using System.IO;

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

        }
    }
} 