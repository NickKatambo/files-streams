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
                //IEnumerable<dynamic> records = csvReader.GetRecord<dynamic>();
                IEnumerable<ProcessedOrder> records = (IEnumerable<ProcessedOrder>)csvReader.GetRecord<ProcessedOrder>();

                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@'; // Default is #
                csvReader.Configuration.AllowComments = true;
                //csvReader.Configuration.IgnoreBlankLines = true;
                //csvReader.Configuration.Delimiter = ";";
                //csvReader.Configuration.HasHeaderRecord = false;
                //csvReader.Configuration.HeaderValidated = null;
                //csvReader.Configuration.MissingFieldFound = null;

                foreach (ProcessedOrder record in records)
                {
                    Console.WriteLine(record.OrderNumber);
                    Console.WriteLine(record.Customer);
                    Console.WriteLine(record.Amount);

                }
            }
        }
    }
}