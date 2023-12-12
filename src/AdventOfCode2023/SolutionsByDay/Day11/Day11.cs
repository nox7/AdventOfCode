using AdventOfCode2023.SolutionsByDay.Day5;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day11
{
    internal class Day11 : DaySolution
    {
        public long GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day11.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            SpaceGrid grid = new();
            List<SpaceLocation> galaxies = new();

            // Iterate over each map line until the line is null
            int currentGalaxyId = 1;
            int row = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                List<SpaceLocation> locations = new();
                int column = 0;
                foreach (char c in line)
                {
                    // Create a new space location, flag if the location is empty space or not
                    if (c == '.')
                    {
                        locations.Add(new SpaceLocation() { IsEmptySpace = true, Row = row, Column = column++ });
                    }
                    else if (c == '#')
                    {
                        var location = new SpaceLocation() { IsEmptySpace = false, GalaxyId = currentGalaxyId++, Row = row, Column = column++ };
                        locations.Add(location);
                        galaxies.Add(location);
                    }
                }

                grid.Objects.Add(locations);
                ++row;
            }

            // Iterate over every combination by starting at one galaxy, and iterating over every galaxy in the array beyond that galaxy
            // E.g. [galaxy[i], galaxy[i + 1]], [galaxy[i], galaxy[i + 2]], and so on
            long totalManhattanDistances = 0;
            for (int i = 0; i < galaxies.Count; i++)
            {
                SpaceLocation pairItem1 = galaxies[i];
                for (int ii = i + 1; ii < galaxies.Count; ii++)
                {
                    SpaceLocation pairItem2 = galaxies[ii];

                    if (pairItem1 != pairItem2)
                    {
                        // Calculate Manhattan distance
                        var manhattanDistance = grid.ManhattanDistance(pairItem1, pairItem2, 1); // Space expanded 1 time
                        totalManhattanDistances += manhattanDistance;
                    }

                }
            }

            return totalManhattanDistances;
        }

        public long GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day11.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            SpaceGrid grid = new();
            List<SpaceLocation> galaxies = new();

            // Iterate over each map line until the line is null
            int currentGalaxyId = 1;
            int row = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                List<SpaceLocation> locations = new();
                int column = 0;
                foreach (char c in line)
                {
                    // Create a new space location, flag if the location is empty space or not
                    if (c == '.')
                    {
                        locations.Add(new SpaceLocation() { IsEmptySpace = true, Row = row, Column = column++ });
                    }
                    else if (c == '#')
                    {
                        var location = new SpaceLocation() { IsEmptySpace = false, GalaxyId = currentGalaxyId++, Row = row, Column = column++ };
                        locations.Add(location);
                        galaxies.Add(location);
                    }
                }

                grid.Objects.Add(locations);
                ++row;
            }

            // Iterate over every combination by starting at one galaxy, and iterating over every galaxy in the array beyond that galaxy
            // E.g. [galaxy[i], galaxy[i + 1]], [galaxy[i], galaxy[i + 2]], and so on
            long totalManhattanDistances = 0;
            for (int i = 0; i < galaxies.Count; i++)
            {
                SpaceLocation pairItem1 = galaxies[i];
                for (int ii = i + 1; ii < galaxies.Count; ii++)
                {
                    SpaceLocation pairItem2 = galaxies[ii];

                    if (pairItem1 != pairItem2)
                    {
                        // Calculate Manhattan distance
                        var manhattanDistance = grid.ManhattanDistance(pairItem1, pairItem2, 1000000 - 1); // Space expanded 999,999 times (one million times larger)
                        totalManhattanDistances += manhattanDistance;
                    }

                }
            }

            return totalManhattanDistances;
        }
    }
}
