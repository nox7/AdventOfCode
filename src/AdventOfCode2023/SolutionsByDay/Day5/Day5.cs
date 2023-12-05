using AdventOfCode2023.SolutionsByDay.Day4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            foreach (long seed in almanac.Seeds)
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

            // Keep track of the number ranges we are working with
            // These will be destinations, not sources
            List<NumericRange> activeRanges = [];

            // For each seed range,
            // - Loop through each soil range
            // - Does the Seed range intersect with the soil range's Source?
            // Get the intersection of SoilRangeSource.GetIntersection(SeedRangeSource)
            foreach (NumericRange range in almanac.SeedsAsRanges)
            {
                foreach (RowMap soilRangeMap in almanac.SeedToSoilMaps)
                {
                    var validSourceRange = soilRangeMap.Source.GetIntersection(range);

                    if (validSourceRange != null)
                    {
                        // Get the destination range from the source range
                        var validDestinationRange = new NumericRange()
                        {
                            Start = soilRangeMap.GetDestinationFromSource(validSourceRange.Start),
                            End = soilRangeMap.GetDestinationFromSource(validSourceRange.End)
                        };

                        activeRanges.Add(validDestinationRange);
                    }
                }
            }

            // Iterate over the active ranges
            // For Soil -> Fertilizer
            List<NumericRange> newActiveRanges = [];
            foreach (RowMap soilToFertilizerMap in almanac.SoilToFertilzerMaps)
            {
                foreach (NumericRange activeRange in activeRanges)
                {
                    var validSourceRange = soilToFertilizerMap.Source.GetIntersection(activeRange);
                    if (validSourceRange != null)
                    {
                        // Get the destination range from the source range
                        var validDestinationRange = new NumericRange()
                        {
                            Start = soilToFertilizerMap.GetDestinationFromSource(validSourceRange.Start),
                            End = soilToFertilizerMap.GetDestinationFromSource(validSourceRange.End)
                        };

                        newActiveRanges.Add(validDestinationRange);

                        // Subtract out the validSourceRange from the current active range
                        activeRange.Subtract(validSourceRange);
                    }
                }
            }

            // Clear out empty activeRanges
            activeRanges = [.. activeRanges.Where(range => !range.IsEmpty()).ToList(), .. newActiveRanges];

            // Iterate over the active ranges
            // For Fertilizer -> Water
            newActiveRanges = [];
            foreach (RowMap fertilizerToWaterMap in almanac.FertilzerToWaterMaps)
            {
                foreach (NumericRange activeRange in activeRanges)
                {
                    var validSourceRange = fertilizerToWaterMap.Source.GetIntersection(activeRange);
                    if (validSourceRange != null)
                    {
                        // Get the destination range from the source range
                        var validDestinationRange = new NumericRange()
                        {
                            Start = fertilizerToWaterMap.GetDestinationFromSource(validSourceRange.Start),
                            End = fertilizerToWaterMap.GetDestinationFromSource(validSourceRange.End)
                        };

                        newActiveRanges.Add(validDestinationRange);

                        // Subtract out the validSourceRange from the current active range
                        Console.WriteLine();
                        activeRange.Subtract(validSourceRange);
                        Console.WriteLine();
                    }
                }
            }

            // Clear out empty activeRanges
            activeRanges = [.. activeRanges.Where(range => !range.IsEmpty()).ToList(), .. newActiveRanges];

            // Iterate over the active ranges
            // For Water -> Light
            newActiveRanges = [];
            foreach (RowMap waterToLightMap in almanac.WaterToLightMaps)
            {
                foreach (NumericRange activeRange in activeRanges)
                {
                    var validSourceRange = waterToLightMap.Source.GetIntersection(activeRange);
                    if (validSourceRange != null)
                    {
                        // Get the destination range from the source range
                        var validDestinationRange = new NumericRange()
                        {
                            Start = waterToLightMap.GetDestinationFromSource(validSourceRange.Start),
                            End = waterToLightMap.GetDestinationFromSource(validSourceRange.End)
                        };

                        newActiveRanges.Add(validDestinationRange);

                        // Subtract out the validSourceRange from the current active range
                        Console.WriteLine();
                        activeRange.Subtract(validSourceRange);
                        Console.WriteLine();
                    }
                }
            }

            // Clear out empty activeRanges
            activeRanges = [.. activeRanges.Where(range => !range.IsEmpty()).ToList(), .. newActiveRanges];

            // Iterate over the active ranges
            // For Light -> Temperature
            newActiveRanges = [];
            foreach (RowMap lightToTemperatureMap in almanac.LightToTemperatureMaps)
            {
                foreach (NumericRange activeRange in activeRanges)
                {
                    var validSourceRange = lightToTemperatureMap.Source.GetIntersection(activeRange);
                    if (validSourceRange != null)
                    {
                        // Get the destination range from the source range
                        var validDestinationRange = new NumericRange()
                        {
                            Start = lightToTemperatureMap.GetDestinationFromSource(validSourceRange.Start),
                            End = lightToTemperatureMap.GetDestinationFromSource(validSourceRange.End)
                        };

                        newActiveRanges.Add(validDestinationRange);

                        // Subtract out the validSourceRange from the current active range
                        Console.WriteLine();
                        activeRange.Subtract(validSourceRange);
                        Console.WriteLine();
                    }
                }
            }

            // Clear out empty activeRanges
            activeRanges = [.. activeRanges.Where(range => !range.IsEmpty()).ToList(), .. newActiveRanges];

            // Iterate over the active ranges
            // For Temperature -> Humidity
            newActiveRanges = [];
            foreach (RowMap temperatureToHumidityMap in almanac.TemperatureToHumidtyMaps)
            {
                foreach (NumericRange activeRange in activeRanges)
                {
                    var validSourceRange = temperatureToHumidityMap.Source.GetIntersection(activeRange);
                    if (validSourceRange != null)
                    {
                        // Get the destination range from the source range
                        var validDestinationRange = new NumericRange()
                        {
                            Start = temperatureToHumidityMap.GetDestinationFromSource(validSourceRange.Start),
                            End = temperatureToHumidityMap.GetDestinationFromSource(validSourceRange.End)
                        };

                        newActiveRanges.Add(validDestinationRange);

                        // Subtract out the validSourceRange from the current active range
                        Console.WriteLine();
                        activeRange.Subtract(validSourceRange);
                        Console.WriteLine();
                    }
                }
            }

            // Clear out empty activeRanges
            activeRanges = [.. activeRanges.Where(range => !range.IsEmpty()).ToList(), .. newActiveRanges];

            // Iterate over the active ranges
            // For Humidity -> Location
            newActiveRanges = [];
            foreach (RowMap humidityToLocationMap in almanac.HumidityToLocationMaps)
            {
                foreach (NumericRange activeRange in activeRanges)
                {
                    var validSourceRange = humidityToLocationMap.Source.GetIntersection(activeRange);
                    if (validSourceRange != null)
                    {
                        // Get the destination range from the source range
                        var validDestinationRange = new NumericRange()
                        {
                            Start = humidityToLocationMap.GetDestinationFromSource(validSourceRange.Start),
                            End = humidityToLocationMap.GetDestinationFromSource(validSourceRange.End)
                        };

                        newActiveRanges.Add(validDestinationRange);

                        // Subtract out the validSourceRange from the current active range
                        Console.WriteLine();
                        activeRange.Subtract(validSourceRange);
                        Console.WriteLine();
                    }
                }
            }

            // Clear out empty activeRanges
            activeRanges = [.. activeRanges.Where(range => !range.IsEmpty()).ToList(), .. newActiveRanges];

            // Sort so the range with the lowest start is first
            activeRanges = activeRanges.OrderBy(range => range.Start).ToList();

            return activeRanges[0].Start;
        }
    }
}
