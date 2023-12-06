using AdventOfCode2023.SolutionsByDay.Day4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day6
{
    internal class Day6 : DaySolution
    {
        public long GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day6.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            string line1 = streamReader.ReadLine()!;
            string line2 = streamReader.ReadLine()!;

            // Get the numbers from lines one and two
            string[] line1Numbers = Regex.Replace(line1.Split(":")[1], @"\s+", " ").Trim().Split(" ");
            string[] line2Numbers = Regex.Replace(line2.Split(":")[1], @"\s+", " ").Trim().Split(" ");

            // Convert them into races by matching the numbers from each column
            List<Race> races = [];

            for (int i = 0; i < line1Numbers.Length; i++)
            {
                long Time = Int64.Parse(line1Numbers[i]);
                long RecordDistance = Int64.Parse(line2Numbers[i]);
                races.Add(new Race()
                {
                    Time = Time,
                    DistanceToBeat = RecordDistance,
                });
            }

            int accumulationOfProductsOfNumWaysToWinRace = 1;
            // For each race, solve an inequality of:
            // DistanceToBeat < x * (Time - x)
            // Solving for X, distribute x
            // -> DistanceToBeat < xTime - x^2
            // Rewrite into standard quadratic form
            // -> 0 < -x^2 + xTime - DistanceToBeat
            // Solve with quadratic equation for upper and lower bounds
            // A = -1, B = Time, C = -DistanceToBeat
            // Then, get all integers between the bounds
            foreach (Race race in races)
            {
                double a = -1;
                double b = race.Time;
                double c = -race.DistanceToBeat;
                double bound1 = (-b - Math.Sqrt(Math.Pow(b, 2.0) - 4 * a * c)) / (2 * a);
                double bound2 = (-b + Math.Sqrt(Math.Pow(b, 2.0) - 4 * a * c)) / (2 * a);
                double lowerBound = Math.Min(bound1, bound2);
                double upperBound = Math.Max(bound1, bound2);
                int lowerBoundIntInsideInterval = (int)Math.Ceiling(lowerBound + 0.01); // + 0.01 in case of whole numbers
                int upperBoundIntInsideInterval = (int)Math.Floor(upperBound - 0.01); // - 0.01 in case of whole numbers
                // We add/subtract 0.01 because if the lower/upper bound is a whole number then we have to _at least_
                // beat that bound (we can't be equal to the DistanceToBeat, because then we didn't beat it)
                int numOfSolutions = upperBoundIntInsideInterval - lowerBoundIntInsideInterval + 1; // + 1 to make it inclusive
                accumulationOfProductsOfNumWaysToWinRace *= numOfSolutions;
            }

            return accumulationOfProductsOfNumWaysToWinRace;
        }

        public long GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day6.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            string line1 = streamReader.ReadLine()!;
            string line2 = streamReader.ReadLine()!;

            // Get the numbers from lines one and two
            // For part 2, remove all spaces between numbers to treat them as one large number
            string[] line1Numbers = Regex.Replace(line1.Split(":")[1], @"\s+", "").Trim().Split(" ");
            string[] line2Numbers = Regex.Replace(line2.Split(":")[1], @"\s+", "").Trim().Split(" ");

            // Convert them into races by matching the numbers from each column
            List<Race> races = [];

            for (int i = 0; i < line1Numbers.Length; i++)
            {
                long Time = Int64.Parse(line1Numbers[i]);
                long RecordDistance = Int64.Parse(line2Numbers[i]);
                races.Add(new Race()
                {
                    Time = Time,
                    DistanceToBeat = RecordDistance,
                });
            }

            int accumulationOfProductsOfNumWaysToWinRace = 1;
            // For each race, solve an inequality of:
            // DistanceToBeat < x * (Time - x)
            // Solving for X, distribute x
            // -> DistanceToBeat < xTime - x^2
            // Rewrite into standard quadratic form
            // -> 0 < -x^2 + xTime - DistanceToBeat
            // Solve with quadratic equation for upper and lower bounds
            // A = -1, B = Time, C = -DistanceToBeat
            // Then, get all integers between the bounds
            foreach (Race race in races)
            {
                double a = -1;
                double b = race.Time;
                double c = -race.DistanceToBeat;
                double bound1 = (-b - Math.Sqrt(Math.Pow(b, 2.0) - 4 * a * c)) / (2 * a);
                double bound2 = (-b + Math.Sqrt(Math.Pow(b, 2.0) - 4 * a * c)) / (2 * a);
                double lowerBound = Math.Min(bound1, bound2);
                double upperBound = Math.Max(bound1, bound2);
                int lowerBoundIntInsideInterval = (int)Math.Ceiling(lowerBound + 0.01); // + 0.01 in case of whole numbers
                int upperBoundIntInsideInterval = (int)Math.Floor(upperBound - 0.01); // - 0.01 in case of whole numbers
                // We add/subtract 0.01 because if the lower/upper bound is a whole number then we have to _at least_
                // beat that bound (we can't be equal to the DistanceToBeat, because then we didn't beat it)
                int numOfSolutions = upperBoundIntInsideInterval - lowerBoundIntInsideInterval + 1; // + 1 to make it inclusive
                accumulationOfProductsOfNumWaysToWinRace *= numOfSolutions;
            }

            return accumulationOfProductsOfNumWaysToWinRace;
        }

    }
}
