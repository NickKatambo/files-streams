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
                    // ** Simplified version **
                    //using (var inputFileStream = new FileStream(InputFilePath, FileMode.Open))
                    //using (var outputFileStream = new FileStream(OutputFilePath, FileMode.Create))
                    //using (var outPutStreamWriter = new StreamWriter(outputFileStream))
                    
                    using (var inputStreamReader = new StreamReader(InputFilePath))
                    using (var outPutStreamWriter = new StreamWriter(OutputFilePath))
                    {
                        while (!inputStreamReader.EndOfStream)
                        {
                            /*string line = inputStreamReader.ReadLine();
                            string processedLine = line.ToUpperInvariant();
                            outPutStreamWriter.WriteLine(processedLine);
                            */
                            string line = inputStreamReader.ReadLine();
                            string processedLine = line.ToUpperInvariant();
                            bool isLastLine = inputStreamReader.EndOfStream;

                            if (isLastLine)
                            {
                                outPutStreamWriter.Write(processedLine);
                            }
                            else
                            {
                                outPutStreamWriter.WriteLine(processedLine);
                            }
                        }
                    }
                    break;
                case 4:
                    using (var inputStreamReader = new StreamReader(InputFilePath))
                    using (var outPutStreamWriter = new StreamWriter(OutputFilePath))
                    {
                        var currentLineNumber = 1;
                        while (!inputStreamReader.EndOfStream)
                        {
                            string line = inputStreamReader.ReadLine();
                            if (currentLineNumber == 2)
                            {
                                Write(line.ToUpperInvariant());
                            }
                            else
                            {
                                Write(line);
                            }

                            currentLineNumber++;

                            void Write(string content)
                            {
                                bool isLastLine = inputStreamReader.EndOfStream;
                                if (isLastLine)
                                {
                                    outPutStreamWriter.Write(content);
                                }
                                else
                                {
                                    outPutStreamWriter.WriteLine(content);
                                }
                            }
                        }
                    }
                    break;
                case 5:
                    using (FileStream input = File.Open(InputFilePath,FileMode.Open,FileAccess.Read))
                    using (FileStream output = File.Create(OutputFilePath))
                    {
                        const int endOfStream = -1;
                        int largestByte = 0;
                        int currentByte = input.ReadByte();
                        while (currentByte != endOfStream)
                        {
                            output.WriteByte((byte)currentByte);

                            if (currentByte > largestByte)
                            {
                                largestByte = currentByte;
                            }

                            currentByte = input.ReadByte();
                        }

                        output.WriteByte((byte)largestByte);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
