using AdventOfCode2023.SolutionsByDay.Day11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day12
{
    internal class Day12 : DaySolution
    {
        public long GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day12.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Iterate over each map line until the line is null
            long totalArrangements = 0;
            SpringArrangementsSolver solver = new();
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] lineSplit = line.Split(' ');
                string springs = lineSplit[0];
                string blockCountsString = lineSplit[1];
                int[] blockCounts = Array.ConvertAll(blockCountsString.Split(','), Int32.Parse);
                SpringArrangementsSolver.ArrangementCache.Clear();
                totalArrangements += solver.GetNumArrangements(
                    springs, blockCounts, 0, 0, 0
                    );
            }
            return totalArrangements;
        }

        public long GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day12.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Iterate over each map line until the line is null
            long totalArrangements = 0;
            SpringArrangementsSolver solver = new();
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] lineSplit = line.Split(' ');
                string springs = lineSplit[0];
                string blockCountsString = lineSplit[1];

                // In part 2, we only need copy the records five times
                // It also states to join the copies by a '?' on the springs
                // and join the block counts by a ','
                springs = String.Join('?', [
                    springs, springs, springs, springs, springs
                    ]);
                blockCountsString = String.Join(',', [
                    blockCountsString, blockCountsString, blockCountsString, blockCountsString, blockCountsString
                    ]);

                int[] blockCounts = Array.ConvertAll(blockCountsString.Split(','), Int32.Parse);

                // Clear the solved group arrangement cache before beginning each line's arrangement solve
                SpringArrangementsSolver.ArrangementCache.Clear();

                totalArrangements += solver.GetNumArrangements(
                    springs, blockCounts, 0, 0, 0
                    );
            }
            return totalArrangements;
        }
    }
}
