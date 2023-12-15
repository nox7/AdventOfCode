using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day14
{
	internal class RockGrid
	{
		public required string[] Lines { get; set; }

		public Dictionary<int, Dictionary<int, char>> GetLinesAsDictionaries()
		{
			Dictionary<int, Dictionary<int, char>> rocksGridAsDictionary = [];
			for (int row = 0; row < Lines.Length; row++)
			{
				if (!rocksGridAsDictionary.TryGetValue(row, out Dictionary<int, char>? line))
				{
					// If there is no Dictionary for this row, create one
					rocksGridAsDictionary[row] = [];
				}

				for (int column = 0; column < Lines[0].Length; column++)
				{
					rocksGridAsDictionary[row].Add(column, Lines[row][column]);
				}
			}

			return rocksGridAsDictionary;
		}

		public void RollAllRocksNorth(ref Dictionary<int, Dictionary<int, char>> rocksGridAsDictionary)
		{
			/*Dictionary<int, Dictionary<int, char>> rolledDictionary = [];
			for (int row = 0; row < Lines.Length; row++)
			{
				rolledDictionary[row] = 
			}*/

			for (int column = 0; column < Lines[0].Length; column++)
			{
				for (int _ = 0; _ < Lines.Length; _++){
					for (int row = 0; row < Lines.Length; row++)
					{
						// Is this row below the top row and currently a rock?
						// Then, is the row above row and column a free space?
						if (rocksGridAsDictionary[row][column] == 'O' && row > 0 && rocksGridAsDictionary[row - 1][column] == '.')
						{
							// Modify the current row column to be an empty space now
							rocksGridAsDictionary[row][column] = '.';

							// Modify the above row to be the moved rock
							rocksGridAsDictionary[row - 1][column] = 'O';
						}
					}
				}
			}
		}

		public long GetLoadFromRocks(Dictionary<int, Dictionary<int, char>> rockGridAsDictionary)
		{
			long loadFromRocks = 0;
			for (int row = 0; row < Lines.Length; row++)
			{
				for (int column = 0; column < Lines[0].Length; column++)
				{
					if (rockGridAsDictionary[row][column] == 'O')
					{
						// Calculate load by getting the current 1-based index of the current row
						loadFromRocks += Lines.Length - row;
					}
				}
			}

			return loadFromRocks;
		}

		public void RotateRockGrid90Degrees(ref Dictionary<int, Dictionary<int, char>> rockGridAsDictionary)
		{
			Dictionary<int, Dictionary<int, char>> rotatedGrid = [];

			// Fill in blank rows
			for (int row = 0; row < Lines.Length; row++)
			{
				rotatedGrid[row] = [];
			}

				
			for(int row = 0; row < Lines.Length; row++)
			{
				for (int column = 0; column < Lines[0].Length; column++)
				{
					rotatedGrid[column][Lines.Length - 1 - row] = rockGridAsDictionary[row][column];
				}
			}

			rockGridAsDictionary = rotatedGrid;
		}
	}
}
