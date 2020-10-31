using static System.Console;

namespace DataProcessor
{
    internal class FileProcessor
    {
        public string InputFilePath { get; set; }

        public FileProcessor(string filePath)
        {
            InputFilePath = filePath;
        }

        public void Process()
        {
            WriteLine($"Begin process of {InputFilePath}");
        }
    }
}