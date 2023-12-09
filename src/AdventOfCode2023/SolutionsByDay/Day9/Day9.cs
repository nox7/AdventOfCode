using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day9
{
    internal class Day9 : DaySolution
    {
        public long GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day9.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            List<NumbersRow> topLevelNumberRows = [];

            // Iterate over each map line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] numbersAsStrings = line.Split(" ").Where(part => part.Trim() != "").ToArray();
                List<long> numbers = [];
                NumbersRow numberRow = new();

                foreach(string numberAsString in numbersAsStrings)
                {
                    numbers.Add(Int64.Parse(numberAsString.Trim()));
                }

                numberRow.Numbers = numbers;
                numberRow.ExtrapolateNumbersIntoRows();
                topLevelNumberRows.Add(numberRow);
            }

            long sumOfPredictedNextTopLevelNumbers = 0;
            foreach(var topLevelNumberRow in topLevelNumberRows)
            {
                long nextNumberInSeries = topLevelNumberRow.GetNextExpectedNumberInRow();
                sumOfPredictedNextTopLevelNumbers += nextNumberInSeries;
            }

            return sumOfPredictedNextTopLevelNumbers;
        }

        public long GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day9.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            List<NumbersRow> topLevelNumberRows = [];

            // Iterate over each map line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] numbersAsStrings = line.Split(" ").Where(part => part.Trim() != "").ToArray();
                List<long> numbers = [];
                NumbersRow numberRow = new();

                foreach (string numberAsString in numbersAsStrings)
                {
                    numbers.Add(Int64.Parse(numberAsString.Trim()));
                }

                numberRow.Numbers = numbers;
                numberRow.ExtrapolateNumbersIntoRows();
                topLevelNumberRows.Add(numberRow);
            }

            long sumOfPredictedPreviousTopLevelNumbers = 0;
            foreach (var topLevelNumberRow in topLevelNumberRows)
            {
                long previousNumberInSeries = topLevelNumberRow.GetPreviousExpectedNumberInRow();
                sumOfPredictedPreviousTopLevelNumbers += previousNumberInSeries;
            }

            return sumOfPredictedPreviousTopLevelNumbers;
        }
    }
}
