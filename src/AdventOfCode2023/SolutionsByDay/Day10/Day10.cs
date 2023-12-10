using AdventOfCode2023.SolutionsByDay.Day5;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day10
{
    internal class Day10 : DaySolution
    {
        public long GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day10.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            Grid pipeGrid = new();
            GridPoint? startingGridPoint = null;

            // Iterate over each map line until the line is null
            int LineNumber = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                List<GridPoint> lineGridPoints = [];
                int CharacterNumber = 0;
                foreach(char c in line.ToCharArray())
                {
                    var point = new GridPoint()
                    {
                        Character = c,
                        Row = LineNumber,
                        Column = CharacterNumber,
                    };

                    lineGridPoints.Add(point);

                    if (c == 'S')
                    {
                        startingGridPoint = point;
                        point.DistanceFromStart = 0;
                    }

                    CharacterNumber++; 
                }

                pipeGrid.Rows.Add(lineGridPoints);
                ++LineNumber;
            }

            if (startingGridPoint == null)
            {
                throw new Exception("aa");
            }

            int currentRow = startingGridPoint.Row;
            int currentColumn = startingGridPoint.Column;
            int currentDistanceFromStart = 0;

            // AoC says there are always only two connections to a pipe
            // Create two animals that traverse starting from the S and its two connections
            // Loop until they meet
            List<Animal> animals = [
                new Animal() { CurrentGridPoint = startingGridPoint },
                new Animal() { CurrentGridPoint = startingGridPoint },
            ];

            bool animalsMetEachOther = false;
            do
            {
                foreach (Animal animal in animals)
                {
                    List<GridPoint> pipeOptions = pipeGrid.GetConnectingUntraversedGridPoints(animal.CurrentGridPoint);
                    if (pipeOptions.Count > 0)
                    {
                        GridPoint chosenPipeToMoveTo = pipeOptions[0];
                        chosenPipeToMoveTo.DistanceFromStart = currentDistanceFromStart + 1;
                        animal.CurrentGridPoint = chosenPipeToMoveTo;
                    }
                    else
                    {
                        // Out of options. Most likely traversed everything
                        animalsMetEachOther = true;
                    }
                }

                ++currentDistanceFromStart;
            } while (!animalsMetEachOther);

            return currentDistanceFromStart;
        }

        public long GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day10.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            Grid pipeGrid = new();
            GridPoint? startingGridPoint = null;

            // Iterate over each map line until the line is null
            int LineNumber = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                List<GridPoint> lineGridPoints = [];
                int CharacterNumber = 0;
                foreach (char c in line.ToCharArray())
                {
                    var point = new GridPoint()
                    {
                        Character = c,
                        Row = LineNumber,
                        Column = CharacterNumber,
                    };

                    lineGridPoints.Add(point);

                    if (c == 'S')
                    {
                        startingGridPoint = point;
                        point.DistanceFromStart = 0;
                    }

                    CharacterNumber++;
                }

                pipeGrid.Rows.Add(lineGridPoints);
                ++LineNumber;
            }

            if (startingGridPoint == null)
            {
                throw new Exception("aa");
            }

            int currentRow = startingGridPoint.Row;
            int currentColumn = startingGridPoint.Column;
            int currentDistanceFromStart = 0;

            // AoC says there are always only two connections to a pipe
            // Create two animals that traverse starting from the S and its two connections
            // Loop until they meet
            List<Animal> animals = [
                new Animal() { CurrentGridPoint = startingGridPoint },
                new Animal() { CurrentGridPoint = startingGridPoint },
            ];

            bool animalsMetEachOther = false;
            do
            {
                foreach (Animal animal in animals)
                {
                    List<GridPoint> pipeOptions = pipeGrid.GetConnectingUntraversedGridPoints(animal.CurrentGridPoint);
                    if (pipeOptions.Count > 0)
                    {
                        GridPoint chosenPipeToMoveTo = pipeOptions[0];
                        chosenPipeToMoveTo.DistanceFromStart = currentDistanceFromStart + 1;
                        animal.CurrentGridPoint = chosenPipeToMoveTo;
                    }
                    else
                    {
                        // Out of options. Most likely traversed everything
                        animalsMetEachOther = true;
                    }
                }

                ++currentDistanceFromStart;
            } while (!animalsMetEachOther);

            // Now that the animals have met each other, we have traversed all maze paths and thus
            // have created a polygon

            // To determine how many tiles are _inside_ the loop
            // we can use a game-dev engine trick of polygon-raycasting method which determines if a point is within the polygon

            // To do this
            // 1. Iterate each line left-to-right
            // 2. If the character is PART of the animal-traversed piping system:
            // - If we are not tracking a ray:
            // - - If the character is a pipe that can go SOUTH
            // - - - Toggle the ray either on or off
            // - If we are tracking a ray
            // - - If the character is _not_ part of the animal-traversed piping system
            // - - - Add 1 to the counter of enclosed tiles
            int totalTilesEnclosedInLoop = 0;
            foreach(List<GridPoint> listOfGridPoints in pipeGrid.Rows)
            {
                bool inRay = false;
                foreach(GridPoint point in listOfGridPoints)
                {
                    // Check if the DistanceFromStart is none-null
                    // A Null DistanceFromStart means the point is NOT part of the animal-traversed piping system,
                    // A Non-Null DistanceFromStart means this is a pipe the animal traversed
                    if (point.DistanceFromStart != null)
                    {
                        if (point.Character == 'F' || point.Character == '7' || point.Character == '|' || point.Character == 'S')
                        {
                            // Toggle the ray
                            inRay = !inRay;
                        }
                    }
                    else
                    {
                        if (inRay)
                        {
                            ++totalTilesEnclosedInLoop;
                        }
                    }
                }
            }

            return totalTilesEnclosedInLoop;
        }
    }
}
