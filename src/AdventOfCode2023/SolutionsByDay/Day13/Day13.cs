using AdventOfCode2023.SolutionsByDay.Day11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day13
{
    internal class Day13 : DaySolution
    {
        public long GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day13.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);
            string fileContents = streamReader.ReadToEnd();

            List<Grid> grids = [];

            // Stores the pattern note summary score for column and row reflections
            long patternNoteSummary = 0;

            // Grids are separated by another newline
            // Get all grids
            string[] gridsAsStrings = fileContents.Split("\r\n\r\n");
            foreach(string grid in gridsAsStrings)
            {
                // Split the grid into individual newlines
                string[] gridLines = grid.Split("\r\n");

                // Create a new Grid object with the grid's lines
                var newGrid = new Grid()
                {
                    GridLines = gridLines,
                };

                patternNoteSummary += newGrid.GetColumnSymmetryScore(0);

				grids.Add(newGrid);
            }

            return patternNoteSummary;
        }

        public long GetPart2Solution()
        {
			// Open the file to read
			using var fileStream = File.OpenRead("SourceInputs/Day13.txt");

			// Stream the contents
			using var streamReader = new StreamReader(fileStream);
			string fileContents = streamReader.ReadToEnd();

			List<Grid> grids = [];

			// Stores the pattern note summary score for column and row reflections
			long patternNoteSummary = 0;

			// Grids are separated by another newline
			// Get all grids
			string[] gridsAsStrings = fileContents.Split("\r\n\r\n");
			foreach (string grid in gridsAsStrings)
			{
				// Split the grid into individual newlines
				string[] gridLines = grid.Split("\r\n");

				// Create a new Grid object with the grid's lines
				var newGrid = new Grid()
				{
					GridLines = gridLines,
				};

                // For part two, we simply pass the allowance of a single invalid character per column/row reflection test
                // A single invalid matching character signifies a smudge that would otherwise allow
                // the reflection to be 100% symmetrical
				patternNoteSummary += newGrid.GetColumnSymmetryScore(1);

				grids.Add(newGrid);
			}

			return patternNoteSummary;
		}
    }
}
