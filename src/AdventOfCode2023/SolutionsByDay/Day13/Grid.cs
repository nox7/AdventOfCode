using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day13
{
	internal class Grid
	{
		public string[] GridLines { get; set; } = [];

		/// <summary>
		/// Gets the reflection score as defined by AoC day 3.
		/// 
		/// maxInvalidMatchesPerReflectionTest allows part 2 to be solved by allowing for one mismatch (a single invalid character
		/// between two comparing columns or rows).
		/// 
		/// We keep track of the invalid characters for this reason and do not break on the first invalid character.
		/// </summary>
		/// <param name="maxInvalidMatchesPerReflectionTest"></param>
		/// <returns></returns>
		public long GetColumnSymmetryScore(int maxInvalidMatchesPerReflectionTest)
		{
			long score = 0;

			// Number of columns will always be the same on any line, so just use the first as reference
			long numColumnsPerLine = GridLines[0].Length;

			// Check for reflections between column bisecting lines
			for (int column = 0; column < numColumnsPerLine - 1; column++)
			{
				// Keep track of how many invalid reflections there were
				// For part 1, this will need to be 0
				int invalidMatches = 0;
				for (int columnDifference = 0; columnDifference < numColumnsPerLine; columnDifference++)
				{
					int leftColumn = column - columnDifference;
					int rightColumn = column + 1 + columnDifference;
					if (leftColumn >= 0 && rightColumn < numColumnsPerLine)
					{
						// Go through the rows for each column pair
						for (int row = 0; row < GridLines.Length; row++)
						{
							char leftChar = GridLines[row][leftColumn];
							char rightChar = GridLines[row][rightColumn];
							if (leftChar != rightChar)
							{
								invalidMatches++;
							}
						}
					}
				}

				if (invalidMatches == maxInvalidMatchesPerReflectionTest)
				{
					// Part 1 says add the column number (1-based index, so +1)
					// to the total score for columns
					Console.WriteLine($"Column {column} reflects.");
					score += column + 1;
				}
			}

			Console.WriteLine($"Column answer: {score}");

			// Check for reflections between row bisecting lines
			for (int row = 0; row < GridLines.Length - 1; row++)
			{
				int invalidMatches = 0;
				for (int rowDifference = 0; rowDifference < GridLines.Length; rowDifference++)
				{
					int rowAbove = row - rowDifference; 
					int rowBelow = row + 1 + rowDifference;
					if (rowAbove >= 0 && rowBelow < GridLines.Length)
					{
						for (int column = 0; column < numColumnsPerLine; column++)
						{
							char charAbove = GridLines[rowAbove][column];
							char charBelow = GridLines[rowBelow][column];
							if (charAbove != charBelow)
							{
								// Using a 1-based index (row + 1), add the row number multiplied by 100
								// ^^ AoC's instructions
								invalidMatches++;
							}
						}
					}
				}

				if (invalidMatches == maxInvalidMatchesPerReflectionTest)
				{
					score += 100 * (row + 1);
				}
			}

			return score;
		}
	}
}
