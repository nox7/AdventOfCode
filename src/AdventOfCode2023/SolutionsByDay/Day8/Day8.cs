using AdventOfCode2023.SolutionsByDay.Day8;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day7
{
    internal class Day8 : DaySolution
    {
        public int GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day8.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            string directionsLine = streamReader.ReadLine()!;
            char[] directions = directionsLine.ToCharArray();

            // Next line is empty, read it to move to the line after
            streamReader.ReadLine();

            List<Chart> charts = [];

            // Map out the charts by their Location
            Dictionary<string, Chart> chartsDictionary = [];

            // Iterate over each map line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] parts = line.Split(" = ");
                string location = parts[0];
                string routes = parts[1];
                string[] routeParts = routes.Replace("(", "").Replace(")", "").Replace(",", "").Split(" ");
                Chart newChart = new()
                {
                    LocationAsString = location,
                    LocationAsCharArray = location.ToCharArray(),
                    LeftDirectionAsString = routeParts[0],
                    RightDirectionAsString = routeParts[1]
                };

                charts.Add(newChart);
                chartsDictionary[location] = newChart;
            }

            // AoC P1, start with AAA
            Chart currentChart = chartsDictionary["AAA"];
            int lineDirectionIndex = 0;
            int numberOfSteps = 0;
            do
            {
                ++numberOfSteps;
                // Check if we need to go back to the start of the directions
                if (lineDirectionIndex > directions.Length - 1)
                {
                    // Per AoC, reset back to 0
                    lineDirectionIndex = 0;
                }

                char currentDirection = directions[lineDirectionIndex];
                if (currentDirection == 'R')
                {
                    // Right
                    currentChart = chartsDictionary[currentChart.RightDirectionAsString];
                }
                else
                {
                    // Left
                    currentChart = chartsDictionary[currentChart.LeftDirectionAsString];
                }

                ++lineDirectionIndex;
            } while (currentChart.LocationAsString != "ZZZ"); // End when the current chart is ZZZ

            return numberOfSteps;
        }

        public long GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day8.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            // Store the current line
            string? line = null;

            string directionsLine = streamReader.ReadLine()!;
            char[] directions = directionsLine.ToCharArray();

            // Next line is empty, read it to move to the line after
            streamReader.ReadLine();

            List<Chart> charts = [];

            // Map out the charts by their Location
            Dictionary<string, Chart> chartsDictionary = [];

            // Iterate over each map line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] parts = line.Split(" = ");
                string location = parts[0];
                string routes = parts[1];
                string[] routeParts = routes.Replace("(", "").Replace(")", "").Replace(",", "").Split(" ");
                Chart newChart = new()
                {
                    LocationAsString = location,
                    LocationAsCharArray = location.ToCharArray(),
                    LeftDirectionAsString = routeParts[0],
                    RightDirectionAsString = routeParts[1]
                };

                charts.Add(newChart);
                chartsDictionary[location] = newChart;
            }

            // AoC P2, start with all nodes ending with 'A'
            List<Chart> currentNodes = [];

            // Keep track of each node (the key) and how many steps it took until it hit the first end
            // Once all of these are non-zero, use LCM (least common multiple) to find the AoC part 2 answer
            Dictionary<int, int> multiplesUntilEndIsHit = [];

            foreach (Chart chart in charts)
            {
                if (chart.LocationAsCharArray[2] == 'A')
                {
                    // Add it to the start list
                    currentNodes.Add(chart);

                    // Prefill multiplesUntilEndIsHit with 0s
                    multiplesUntilEndIsHit.Add(multiplesUntilEndIsHit.Count, 0);
                }
            }

            int lineDirectionIndex = 0;
            int numberOfSteps = 0;
            do
            {
                ++numberOfSteps;

                // Build a list of new nodes to replace currentNodes with
                List<Chart> newCurrentNodes = [];

                // Check if we need to go back to the start of the directions
                if (lineDirectionIndex > directions.Length - 1)
                {
                    // Per AoC, reset back to 0
                    lineDirectionIndex = 0;
                }

                char currentDirection = directions[lineDirectionIndex];
                if (currentDirection == 'R')
                {
                    // Move all nodes right
                    int index = 0;
                    foreach(Chart currentChart in currentNodes)
                    {
                        Chart newChart = chartsDictionary[currentChart.RightDirectionAsString];
                        newCurrentNodes.Add(newChart);
                        if (newChart.LocationAsCharArray[2] == 'Z' && multiplesUntilEndIsHit[index] == 0){
                            // Fill in this chart's index as the current steps
                            multiplesUntilEndIsHit[index] = numberOfSteps;
                        }
                        ++index;
                    }
                }
                else
                {
                    // Move all nodes right
                    int index = 0;
                    foreach (Chart currentChart in currentNodes)
                    {
                        Chart newChart = chartsDictionary[currentChart.LeftDirectionAsString];
                        newCurrentNodes.Add(newChart);
                        if (newChart.LocationAsCharArray[2] == 'Z' && multiplesUntilEndIsHit[index] == 0)
                        {
                            // Fill in this chart's index as the current steps
                            multiplesUntilEndIsHit[index] = numberOfSteps;
                        }
                        ++index;
                    }
                }

                currentNodes = newCurrentNodes;
                ++lineDirectionIndex;
            } while (multiplesUntilEndIsHit.All(kvp => kvp.Value > 0) == false); // End when all nodes end with a Z location

            // Find the LCM of all the multiples
            long LCM = MathUtil.LeastCommonMultiple(
                multiplesUntilEndIsHit[0],
                MathUtil.LeastCommonMultiple(
                    multiplesUntilEndIsHit[1],
                    MathUtil.LeastCommonMultiple(
                        multiplesUntilEndIsHit[2],
                        MathUtil.LeastCommonMultiple(
                            multiplesUntilEndIsHit[3],
                            MathUtil.LeastCommonMultiple(
                                multiplesUntilEndIsHit[4],
                                multiplesUntilEndIsHit[5]
                                )
                            )
                        )
                    )
                );

            return LCM;
        }
    }
}
