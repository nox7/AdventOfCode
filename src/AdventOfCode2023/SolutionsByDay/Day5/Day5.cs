using AdventOfCode2023.SolutionsByDay.Day4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day5
{
    internal class Day5 : DaySolution
    {
        public long GetPart1Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day5.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            Almanac almanac = Almanac.ParseFromInput(streamReader);

            List<long> seedLocations = [];

            // Get the location of each seed
            foreach(long seed in almanac.Seeds)
            {
                long soil = almanac.GetSoilFromSeed(seed);
                long fertilizer = almanac.GetFertilizerFromSoil(soil);
                long water = almanac.GetWaterFromFertilizer(fertilizer);
                long light = almanac.GetLightFromWater(water);
                long temperature = almanac.GetTemperatureFromLight(light);
                long humidity = almanac.GetHumidityFromTemperature(temperature);
                long location = almanac.GetLocationFromHumidity(humidity);
                seedLocations.Add(location);
            }

            seedLocations.Sort();

            return seedLocations[0];
        }

        public long GetPart2Solution()
        {
            // Open the file to read
            using var fileStream = File.OpenRead("SourceInputs/Day5.txt");

            // Stream the contents
            using var streamReader = new StreamReader(fileStream);

            Almanac almanac = Almanac.ParseFromInput(streamReader);

            // For each seed range,
            // - Loop through each soil range
            // - Create a new
            foreach(NumericRange range in almanac.SeedsAsRanges)
            {

            }

            return 0;
        }
    }
}
