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
                default:
                    break;
            }
        }
    }
}
