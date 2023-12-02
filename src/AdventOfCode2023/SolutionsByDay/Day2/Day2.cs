using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day2
{
    internal class Day2 : DaySolution
    {

        public int GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day2.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Store an accumulator for the sum of all valid/possible game Ids
            int currentSumOfPossibleGameIds = 0;

            List<Game> listOfGames = [];

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                Game currentGame = new()
                {
                    // Set the max cubes each game given by the AoC prompt
                    MaxRedCubes = 12,
                    MaxGreenCubes = 13,
                    MaxBlueCubes = 14
                };

                currentGame.ParseLineIntoGame(line);
                listOfGames.Add(currentGame);
            }

            foreach(Game game in listOfGames)
            {
                if (game.IsGamePossible())
                {
                    currentSumOfPossibleGameIds += game.Id;
                }
            }

            return currentSumOfPossibleGameIds;
        }
    }
}
