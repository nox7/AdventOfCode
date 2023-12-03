using AdventOfCode2023.SolutionsByDay.Day2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day3
{
    internal class Day3 : DaySolution
    {
        public int GetPart1Solution()
        {
            // 2D [row][column] map of every character in the input text
            List<List<char>> inputMap = [];

            EngineSchematic schematic = new();

            // Store the location of symbols as a Vector2 (Row, Column)
            List<Vector2> locationOfSymbols = [];

            // Store the found numbers, their row found, and column spans
            List<List<EngineNumber>> engineNumbers = [];

            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day3.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Store the current line number index as the row coordinate - 0 based
            int currentLineNumber = 0;

            // The current engine number Id to assign to an EngineNumber instance
            int engineIdAutoIncrement = 1;

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                // Create a new list for this line number
                inputMap.Add([]);
                engineNumbers.Add([]);

                EngineNumber? currentEngineNumber = null;

                for (int column = 0; column < line.Length; column++)
                {
                    char c = line[column];

                    // Add the current character to our input map
                    inputMap[currentLineNumber].Add(c);

                    if (Char.IsDigit(c))
                    {
                        // Is there a current engine number being parsed?
                        if (currentEngineNumber == null)
                        {
                            currentEngineNumber = new EngineNumber()
                            {
                                Id = engineIdAutoIncrement++,
                                Row = currentLineNumber,
                                Number = Int32.Parse(c.ToString())
                            };
                            currentEngineNumber.ColumnSpan.Add(column);
                        }
                        else
                        {
                            // Add the column span to the current engine, as well as the append the current digit to the number
                            currentEngineNumber.ColumnSpan.Add(column);
                            currentEngineNumber.Number = Int32.Parse(currentEngineNumber.Number.ToString() + c.ToString());
                        }
                    }
                    else
                    {
                        // Not a digit, was there a current engine number being parsed?
                        if (currentEngineNumber != null)
                        {
                            engineNumbers[currentLineNumber].Add(currentEngineNumber);
                            currentEngineNumber = null;
                        }

                        if (c != '.')
                        {
                            locationOfSymbols.Add(new Vector2((float)currentLineNumber, (float)column));
                        }
                    }
                }

                // Was there an engine number still buffered, but never added to the list because the line ended?
                if (currentEngineNumber != null)
                {
                    engineNumbers[currentLineNumber].Add(currentEngineNumber);
                }

                // Increment the current line number
                ++currentLineNumber;
            }

            // Set the schematic's engine numbers by row that was just parsed
            schematic.EngineNumbersByRow = engineNumbers;

            // Go through all symbols, find adjacent numbers, add them to a running sum
            int currentSumOfPartNumbers = 0;
            foreach (Vector2 symbolLocation in locationOfSymbols)
            {
                int row = (int)symbolLocation.X;
                int rowAbove = row - 1;
                int rowBelow = row + 1;
                int column = (int)symbolLocation.Y;
                int leftColumn = column - 1;
                int rightColumn = column + 1;

                List<EngineNumber> engineNumbersToConsider = [];

                // Top row numbers
                EngineNumber? topLeft = schematic.GetEngineNumberAtRowColumn(rowAbove, leftColumn);
                EngineNumber? topCenter = schematic.GetEngineNumberAtRowColumn(rowAbove, column);
                EngineNumber? topRight = schematic.GetEngineNumberAtRowColumn(rowAbove, rightColumn);

                EngineNumber? left = schematic.GetEngineNumberAtRowColumn(row, leftColumn);
                EngineNumber? right = schematic.GetEngineNumberAtRowColumn(row, rightColumn);

                EngineNumber? bottomLeft = schematic.GetEngineNumberAtRowColumn(rowBelow, leftColumn);
                EngineNumber? bottomCenter = schematic.GetEngineNumberAtRowColumn(rowBelow, column);
                EngineNumber? bottomRight = schematic.GetEngineNumberAtRowColumn(rowBelow, rightColumn);

                // Add the objects if they are not null
                if (topLeft != null) engineNumbersToConsider.Add(topLeft);
                if (topCenter != null) engineNumbersToConsider.Add(topCenter);
                if (topRight != null) engineNumbersToConsider.Add(topRight);
                if (left != null) engineNumbersToConsider.Add(left);
                if (right != null) engineNumbersToConsider.Add(right);
                if (bottomLeft != null) engineNumbersToConsider.Add(bottomLeft);
                if (bottomCenter != null) engineNumbersToConsider.Add(bottomCenter);
                if (bottomRight != null) engineNumbersToConsider.Add(bottomRight);

                var uniqueEngineNumbers = engineNumbersToConsider.Distinct().ToList();
                currentSumOfPartNumbers += uniqueEngineNumbers.Sum(eNumber => eNumber.Number);
            }

            return currentSumOfPartNumbers;
        }

        public int GetPart2Solution()
        {
            // 2D [row][column] map of every character in the input text
            List<List<char>> inputMap = [];

            EngineSchematic schematic = new();

            // Store the location of symbols as a Vector2 (Row, Column)
            List<Vector2> locationOfGears = [];

            // Store the found numbers, their row found, and column spans
            List<List<EngineNumber>> engineNumbers = [];

            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day3.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Store the current line number index as the row coordinate - 0 based
            int currentLineNumber = 0;

            // The current engine number Id to assign to an EngineNumber instance
            int engineIdAutoIncrement = 1;

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                // Create a new list for this line number
                inputMap.Add([]);
                engineNumbers.Add([]);

                EngineNumber? currentEngineNumber = null;

                for (int column = 0; column < line.Length; column++)
                {
                    char c = line[column];

                    // Add the current character to our input map
                    inputMap[currentLineNumber].Add(c);

                    if (Char.IsDigit(c))
                    {
                        // Is there a current engine number being parsed?
                        if (currentEngineNumber == null)
                        {
                            currentEngineNumber = new EngineNumber()
                            {
                                Id = engineIdAutoIncrement++,
                                Row = currentLineNumber,
                                Number = Int32.Parse(c.ToString())
                            };
                            currentEngineNumber.ColumnSpan.Add(column);
                        }
                        else
                        {
                            // Add the column span to the current engine, as well as the append the current digit to the number
                            currentEngineNumber.ColumnSpan.Add(column);
                            currentEngineNumber.Number = Int32.Parse(currentEngineNumber.Number.ToString() + c.ToString());
                        }
                    }
                    else
                    {
                        // Not a digit, was there a current engine number being parsed?
                        if (currentEngineNumber != null)
                        {
                            engineNumbers[currentLineNumber].Add(currentEngineNumber);
                            currentEngineNumber = null;
                        }

                        // For Part 2, only consider *s
                        if (c == '*')
                        {
                            locationOfGears.Add(new Vector2((float)currentLineNumber, (float)column));
                        }
                    }
                }

                // Was there an engine number still buffered, but never added to the list because the line ended?
                if (currentEngineNumber != null)
                {
                    engineNumbers[currentLineNumber].Add(currentEngineNumber);
                }

                // Increment the current line number
                ++currentLineNumber;
            }

            // Set the schematic's engine numbers by row that was just parsed
            schematic.EngineNumbersByRow = engineNumbers;

            // Go through all gears, narrow to only gears with two numbers, multiply those two numbers together
            // and add them to a totalGearRatio
            int totalGearRatio = 0;
            foreach (Vector2 gearLocation in locationOfGears)
            {
                int row = (int)gearLocation.X;
                int rowAbove = row - 1;
                int rowBelow = row + 1;
                int column = (int)gearLocation.Y;
                int leftColumn = column - 1;
                int rightColumn = column + 1;

                List<EngineNumber> engineNumbersToConsider = [];

                // Top row numbers
                EngineNumber? topLeft = schematic.GetEngineNumberAtRowColumn(rowAbove, leftColumn);
                EngineNumber? topCenter = schematic.GetEngineNumberAtRowColumn(rowAbove, column);
                EngineNumber? topRight = schematic.GetEngineNumberAtRowColumn(rowAbove, rightColumn);

                EngineNumber? left = schematic.GetEngineNumberAtRowColumn(row, leftColumn);
                EngineNumber? right = schematic.GetEngineNumberAtRowColumn(row, rightColumn);

                EngineNumber? bottomLeft = schematic.GetEngineNumberAtRowColumn(rowBelow, leftColumn);
                EngineNumber? bottomCenter = schematic.GetEngineNumberAtRowColumn(rowBelow, column);
                EngineNumber? bottomRight = schematic.GetEngineNumberAtRowColumn(rowBelow, rightColumn);

                // Add the objects if they are not null
                if (topLeft != null) engineNumbersToConsider.Add(topLeft);
                if (topCenter != null) engineNumbersToConsider.Add(topCenter);
                if (topRight != null) engineNumbersToConsider.Add(topRight);
                if (left != null) engineNumbersToConsider.Add(left);
                if (right != null) engineNumbersToConsider.Add(right);
                if (bottomLeft != null) engineNumbersToConsider.Add(bottomLeft);
                if (bottomCenter != null) engineNumbersToConsider.Add(bottomCenter);
                if (bottomRight != null) engineNumbersToConsider.Add(bottomRight);

                var uniqueEngineNumbers = engineNumbersToConsider.Distinct().ToList();

                // Only consider gears that are adjacent to 2 numbers
                if (uniqueEngineNumbers.Count == 2)
                {
                    totalGearRatio += uniqueEngineNumbers[0].Number * uniqueEngineNumbers[1].Number;
                }
            }

            return totalGearRatio;
        }
    }
}
