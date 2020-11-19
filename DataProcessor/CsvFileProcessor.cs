using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataProcessor
{
    internal class CsvFileProcessor
    {
        private string InputFilePath;
        private string OutputFilePath;

        public CsvFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        public void Process()
        {
            using (StreamReader input = File.OpenText(InputFilePath))
            using(CsvReader csvReader = new CsvReader((IParser)input))
            {
                IEnumerable<dynamic> records = csvReader.GetRecord<dynamic>();

                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@'; // Default is #
                csvReader.Configuration.AllowComments = true;
                //csvReader.Configuration.IgnoreBlankLines = true;

                foreach (var record in records)
                {
                    Console.WriteLine(record.OrderNumber);
                    Console.WriteLine(record.CustomerNumber);
                    Console.WriteLine(record.Description);
                    Console.WriteLine(record.Quantity);

                }
            }
        }
    }
}