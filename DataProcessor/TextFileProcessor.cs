using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    public class TextFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public TextFileProcessor(string inputFilePath, string outPutFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outPutFilePath;
        }

        public void Process(int action)
        {
            switch (action)
            {
                case 1:
                    // Using read all text
                    string originalText = File.ReadAllText(InputFilePath);
                    string ProcessedText = originalText.ToUpperInvariant();
                    File.WriteAllText(OutputFilePath, ProcessedText);
                    break;
                case 2:
                    // Using read all lines
                    string[] lines = File.ReadAllLines(InputFilePath);
                    if (lines.Length > 1)
                    {
                        lines[1] = lines[1].ToUpperInvariant(); // Assumes there is a line 2 in the file
                        File.WriteAllLines(OutputFilePath, lines);
                    }
                    break;
                case 3:
                    // Coming soon
                    using (var inputFileStream = new FileStream(InputFilePath, FileMode.Open))
                    using (var inputStreamReader = new StreamReader(inputFileStream))
                    using (var outputFileStream = new FileStream(OutputFilePath, FileMode.Create))
                    using (var outPutStreamWriter = new StreamWriter(outputFileStream))
                    {
                        while (!inputStreamReader.EndOfStream)
                        {
                            string line = inputStreamReader.ReadLine();
                            string processedLine = line.ToUpperInvariant();
                            outPutStreamWriter.WriteLine(processedLine);
                        }
                    }
                        break;
                default:
                    break;
            }
        }
    }
}
