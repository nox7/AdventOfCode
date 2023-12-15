using AdventOfCode2023.SolutionsByDay.Day11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day14
{
	internal class Day14 : DaySolution
	{
		public long GetPart1Solution()
		{
			// Open the file to read
			using var fileStream = File.OpenRead("SourceInputs/Day14.txt");

			// Stream the contents
			using var streamReader = new StreamReader(fileStream);
			string fileContents = streamReader.ReadToEnd();

			string[] rockGridLines = fileContents.Split("\r\n");
			var rockGrid = new RockGrid()
			{
				Lines = rockGridLines
			};

			var rocksAsDictionary = rockGrid.GetLinesAsDictionaries();
			rockGrid.RollAllRocksNorth(ref rocksAsDictionary);

			foreach (var dictionary in rocksAsDictionary.Values)
			{
				foreach(var symbol in dictionary.Values)
				{
					Console.Write(symbol);
				}
				Console.WriteLine();
			}

			long load = rockGrid.GetLoadFromRocks(rocksAsDictionary);

			return load;
		}

		public long GetPart2Solution()
		{
			// Open the file to read
			using var fileStream = File.OpenRead("SourceInputs/Day14.txt");

			// Stream the contents
			using var streamReader = new StreamReader(fileStream);
			string fileContents = streamReader.ReadToEnd();

			string[] rockGridLines = fileContents.Split("\r\n");
			var rockGrid = new RockGrid()
			{
				Lines = rockGridLines
			};

			var rocksAsDictionary = rockGrid.GetLinesAsDictionaries();

			long maxIterations = 1000000000;
			List<long> fourthCycleMemory = [];
			for (long cycleIndex = 1; cycleIndex <= maxIterations; cycleIndex++)
			{
				// Todo: Store each rotationIndex's load in lists,
				// and when they all appear in each list already then push the cycleIndex

				// Roll, then rotate. A cycle is 4 rolls and 4 rotations
				for (long rotationIndex = 1; rotationIndex <= 4; rotationIndex++)
				{
					rockGrid.RollAllRocksNorth(ref rocksAsDictionary);
					rockGrid.RotateRockGrid90Degrees(ref rocksAsDictionary);
					long loadAfterRolling = rockGrid.GetLoadFromRocks(rocksAsDictionary);
					Console.WriteLine($"Cycle {cycleIndex}. Rotation {rotationIndex}. Load: {loadAfterRolling}");

					if (rotationIndex == 4 && cycleIndex < 950000000)
					{
						if (fourthCycleMemory.Contains(loadAfterRolling))
						{
							cycleIndex = maxIterations - cycleIndex - 1;
						}
						else
						{
							fourthCycleMemory.Add(loadAfterRolling);
						}
					}
				}
			}

			return 0;
		}
	}
}
