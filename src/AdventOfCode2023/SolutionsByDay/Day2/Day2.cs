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

                // Parse the game line into a proper Game object and list of GameRoll objects
                currentGame.ParseLineIntoGame(line);

                // Add the Game to a list of games
                listOfGames.Add(currentGame);
            }

            foreach(Game game in listOfGames)
            {
                // Add up the Ids of only games that are possible according to the Max color properties of that game
                if (game.IsGamePossible())
                {
                    currentSumOfPossibleGameIds += game.Id;
                }
            }

            return currentSumOfPossibleGameIds;
        }

        public int GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day2.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            // Store an accumulator for the sum of all maximum powers per game
            int currentSumOfMaximumPowerRolls = 0;

            List<Game> listOfGames = [];

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                Game currentGame = new();

                // Parse the game line into a proper Game object and list of GameRoll objects
                currentGame.ParseLineIntoGame(line);

                // Add the Game to a list of games
                listOfGames.Add(currentGame);
            }

            foreach (Game game in listOfGames)
            {
                // Gather the highest number of blue, red, and green cubes present across the game's entire GameRoll set
                int maxNumRedCubes = game.GameRolls
                    .OrderByDescending(gameRoll => gameRoll.NumRedCubes)
                    .First()
                    .NumRedCubes;

                int maxNumGreenCubes = game.GameRolls
                    .OrderByDescending(gameRoll => gameRoll.NumGreenCubes)
                    .First()
                    .NumGreenCubes;

                int maxNumBlueCubes = game.GameRolls
                    .OrderByDescending(gameRoll => gameRoll.NumBlueCubes)
                    .First()
                    .NumBlueCubes;

                // Then multiply those 3 maximums together
                currentSumOfMaximumPowerRolls += maxNumRedCubes * maxNumGreenCubes * maxNumBlueCubes;
            }

            return currentSumOfMaximumPowerRolls;
        }
    }
}
