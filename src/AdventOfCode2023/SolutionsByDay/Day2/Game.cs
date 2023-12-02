using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day2
{
    internal class Game
    {
        public int Id;
        public int MaxRedCubes;
        public int MaxGreenCubes;
        public int MaxBlueCubes;
        public List<GameRoll> GameRolls = [];

        /// <summary>
        /// Determines if this game is possible by verifying all game rolls have a number of cubes
        /// that are within the game's max cube properties
        /// </summary>
        /// <returns></returns>
        public bool IsGamePossible()
        {
            foreach (GameRoll roll in GameRolls)
            {
                if (roll.NumBlueCubes > MaxBlueCubes)
                {
                    return false;
                }

                if (roll.NumGreenCubes > MaxGreenCubes)
                {
                    return false;
                }

                if (roll.NumRedCubes > MaxRedCubes)
                {
                    return false;
                }
            }

            // If we get here, then none of the games had a number of cubes outside of range
            return true;
        }

        /// <summary>
        /// Parses a string line of a Game into an actual Game object with an Id and a list of GameRoll objects
        /// </summary>
        /// <param name="line"></param>
        /// <exception cref="Exception"></exception>
        public void ParseLineIntoGame(string line)
        {
            GameLineParserState parserState = GameLineParserState.GameLabel;
            GameRoll currentGameRoll = new();

            // Store the current buffer of the line consumed by the parser
            string currentBuffer = "";

            // The last roll count parsed. Will be used with the most recent roll color name parsed to
            // set the GameRoll property belonging to that color
            int currentRollCountParsed = 0;

            // Lexically parse the entire line
            char[] charArray = line.ToCharArray();
            for (int index = 0; index < charArray.Length; index++)
            {
                char c = charArray[index];
                switch (parserState)
                {
                    case GameLineParserState.GameLabel:
                        // Consume the "Game" label start of the string

                        // Change if we hit a digit
                        if (Char.IsDigit(c))
                        {
                            currentBuffer += c.ToString();
                            parserState = GameLineParserState.GameId;
                        }

                        break;
                    case GameLineParserState.GameId:
                        if (Char.IsDigit(c))
                        {
                            currentBuffer += c.ToString();
                        }
                        else if (c == ':')
                        {
                            // Hit a semicolon, change to consuming a game's roll count
                            parserState = GameLineParserState.GameCubeRollCount;

                            // Set the current game's Id as the current buffer, then clear the buffer
                            Id = Int32.Parse(currentBuffer);
                            currentBuffer = "";
                        }

                        break;
                    case GameLineParserState.GameCubeRollCount:
                        if (Char.IsDigit(c))
                        {
                            currentBuffer += c.ToString();
                        }
                        else if (c == ' ')
                        {
                            // Ignore spaces explicitly
                        }
                        else
                        {
                            // Any other character means store the current digits in the buffer as the current roll count
                            // and start parsing the roll color name
                            parserState = GameLineParserState.GameCubeColorName;
                            currentRollCountParsed = Int32.Parse(currentBuffer);

                            // Put the current character into a new, reset buffer.
                            currentBuffer = c.ToString();
                        }

                        break;
                    case GameLineParserState.GameCubeColorName:
                        if (c == ' ')
                        {
                            // Ignore spaces explicitly
                        }
                        else if (c == ';' || c == ',')
                        {
                            // Semicolon or comma means this is the end of the roll color name and the start of the next
                            // game roll entirely
                            // Switch back to parsing the roll count
                            parserState = GameLineParserState.GameCubeRollCount;

                            // The current buffer will be a lower-cased color name "red" "green" or "blue"
                            // Use this along with the currentRollCountParsed to set a current game roll property
                            SetGameRollPropertyFromCubeColorName(currentGameRoll, currentBuffer, currentRollCountParsed);
                            currentBuffer = "";
                            currentRollCountParsed = 0;

                            // Now, add the current game roll to the Game's list of rolls
                            // Then, create a new game roll if the 'c' is a semicolon - denoting an entire new roll
                            if (c == ';' || index == charArray.Length - 1)
                            {
                                GameRolls.Add(currentGameRoll);
                                currentGameRoll = new GameRoll();
                            }
                        }
                        else
                        {
                            currentBuffer += c.ToString();

                            // Check if this was the last character to ever be read
                            if (index == charArray.Length - 1)
                            {
                                SetGameRollPropertyFromCubeColorName(currentGameRoll, currentBuffer, currentRollCountParsed);
                                GameRolls.Add(currentGameRoll);
                            }
                        }

                        break;
                    default:
                        throw new Exception("Invalid parser state: " + parserState.ToString());
                }
            }
        }

        /// <summary>
        /// Sets either the NumRedCubes, NumBlueCubes, or NumGreenCubes property of the provided currentRoll to the provided rollCount
        /// based on the provided cubeColorName.
        /// </summary>
        /// <param name="currentRoll"></param>
        /// <param name="cubeColorName"></param>
        /// <param name="rollCount"></param>
        private void SetGameRollPropertyFromCubeColorName(GameRoll currentRoll, string cubeColorName, int rollCount)
        {
            if (cubeColorName == "red")
            {
                currentRoll.NumRedCubes = rollCount;
            }
            else if (cubeColorName == "blue")
            {
                currentRoll.NumBlueCubes = rollCount;
            }
            else if (cubeColorName == "green")
            {
                currentRoll.NumGreenCubes = rollCount;
            }
        }
    }
}
