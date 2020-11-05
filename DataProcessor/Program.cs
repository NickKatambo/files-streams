using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Parsing command line options");

            // Command line validation omitted for brevity

            var command = args[0];
            var directoryToWatch = args[0];

            if (command == "--file")
            {
                var filePath = args[1];
                WriteLine($"Single file {filePath} selected");
                ProcessSingleFile(filePath);
            }
            else if(command == "dir")
            {
                var directoryPath = args[1];
                var fileType = args[2];
                WriteLine($"Directory {directoryPath} selected for {fileType} files");
                ProcessDirectory(directoryPath, fileType);
            }
            else if(!Directory.Exists(directoryToWatch))
            {
                WriteLine($"ERROR: {directoryToWatch} does not exist");
            }
            else
            {
                WriteLine($"Watching directory {directoryToWatch} for changes");
                using (var inputFileWatcher = new FileSystemWatcher(directoryToWatch))
                {
                    inputFileWatcher.IncludeSubdirectories = false;
                    inputFileWatcher.InternalBufferSize = 32768; // 32 KB
                    inputFileWatcher.Filter = "*.*";
                    inputFileWatcher.NotifyFilter = NotifyFilters.LastWrite;

                    inputFileWatcher.Created += FileCreated;
                    inputFileWatcher.Changed += FileChanged;
                    inputFileWatcher.Deleted += FileDeleted;
                    inputFileWatcher.Renamed += FileRenamed;
                    inputFileWatcher.Error += WatcherError;

                    WriteLine("Press enter to quit.");
                    ReadLine();
                }
            }

            WriteLine("Press enter to quit");
            ReadLine();
        }

        private static void WatcherError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void FileRenamed(object sender, RenamedEventArgs e)
        {
            WriteLine($"* File renamed: {e.OldName} to {e.Name} - type: {e.ChangeType}");
        }

        private static void FileDeleted(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File deleted: {e.Name} - type: {e.ChangeType}");
        }

        private static void FileChanged(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File changed: {e.Name} - type: {e.ChangeType}");
        }

        private static void FileCreated(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File created: {e.Name} - type: {e.ChangeType}");
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {
            switch (fileType)
            {
                case "TEXT":
                    string[] textFiles = Directory.GetFiles(directoryPath, "*.txt");
                    foreach (var textFilePath in textFiles)
                    {
                        var fileProcessor = new FileProcessor(textFilePath);
                        fileProcessor.Process();
                    }
                    break;
                default:
                    WriteLine($"ERROR: {fileType} is not supported");
                    return;
            }
        }

        private static void ProcessSingleFile(string filePath)
        {
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }
    }
}
