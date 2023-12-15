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
			long lastLoad = 0;

			// Keep track of each stringified, rolled, and rotated grid of rocks
			// When we hit a duplicate, then we've found a pattern and can skip to the end of the billion cycle indices
			Dictionary<string, long> gridsInMemory = [];
			bool skipped = false;
			for (long cycleIndex = 1; cycleIndex <= maxIterations; cycleIndex++)
			{

				// Roll, then rotate. A cycle is 4 rolls and 4 rotations
				for (long rotationIndex = 1; rotationIndex <= 4; rotationIndex++)
				{
					rockGrid.RollAllRocksNorth(ref rocksAsDictionary);
					rockGrid.RotateRockGrid90Degrees(ref rocksAsDictionary);
					long loadAfterRolling = rockGrid.GetLoadFromRocks(rocksAsDictionary);
					Console.WriteLine($"Cycle {cycleIndex}. Rotation {rotationIndex}. Load: {loadAfterRolling}");

					if (!skipped)
					{
						string rockGridString = rocksAsDictionary.Values.Aggregate("", (str, dictionary) => {
							return str + dictionary.Aggregate("", (innerStr, kvp) => innerStr + kvp.Value.ToString());
						});
						if (gridsInMemory.TryGetValue(rockGridString, out long cycleIndexCycleStartedAt))
						{
							long lengthOfCyclePattern = cycleIndex - cycleIndexCycleStartedAt;
							long amountToSkip = (long)Math.Floor((maxIterations - cycleIndex) / (double)lengthOfCyclePattern);
							cycleIndex += amountToSkip * lengthOfCyclePattern;
							skipped = true;
						}
						gridsInMemory[rockGridString] = cycleIndex;
					}
					lastLoad = loadAfterRolling;
				}
			}

			return lastLoad;
		}
	}
}
