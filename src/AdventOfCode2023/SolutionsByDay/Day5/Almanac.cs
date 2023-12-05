using AdventOfCode2023.SolutionsByDay.Day4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day5
{
    internal class Almanac
    {
        public List<long> Seeds = [];
        public List<NumericRange> SeedsAsRanges = []; // For Day 5 part 2
        public List<RowMap> SeedToSoilMap = [];
        public List<RowMap> SoilToFertilzerMap = [];
        public List<RowMap> FertilzerToWaterMap = [];
        public List<RowMap> WaterToLightMap = [];
        public List<RowMap> LightToTemperatureMap = [];
        public List<RowMap> TemperatureToHumidtyMap = [];
        public List<RowMap> HumidityToLocationMap = [];

        public long GetSoilFromSeed(long seed)
        {
            foreach(RowMap map in SeedToSoilMap)
            {
                if (map.Source.IsInRange(seed))
                {
                    return map.GetDestinationFromSource(seed);
                }
            }

            // If we get here, it was not in any range
            // Return the same source as the destination - per AoC's Day 5 instructions
            return seed;
        }

        public long GetFertilizerFromSoil(long soil)
        {
            foreach (RowMap map in SoilToFertilzerMap)
            {
                if (map.Source.IsInRange(soil))
                {
                    return map.GetDestinationFromSource(soil);
                }
            }

            // If we get here, it was not in any range
            // Return the same source as the destination - per AoC's Day 5 instructions
            return soil;
        }

        public long GetWaterFromFertilizer(long fertilizer)
        {
            foreach (RowMap map in FertilzerToWaterMap)
            {
                if (map.Source.IsInRange(fertilizer))
                {
                    return map.GetDestinationFromSource(fertilizer);
                }
            }

            // If we get here, it was not in any range
            // Return the same source as the destination - per AoC's Day 5 instructions
            return fertilizer;
        }

        public long GetLightFromWater(long water)
        {
            foreach (RowMap map in WaterToLightMap)
            {
                if (map.Source.IsInRange(water))
                {
                    return map.GetDestinationFromSource(water);
                }
            }

            // If we get here, it was not in any range
            // Return the same source as the destination - per AoC's Day 5 instructions
            return water;
        }

        public long GetTemperatureFromLight(long light)
        {
            foreach (RowMap map in LightToTemperatureMap)
            {
                if (map.Source.IsInRange(light))
                {
                    return map.GetDestinationFromSource(light);
                }
            }

            // If we get here, it was not in any range
            // Return the same source as the destination - per AoC's Day 5 instructions
            return light;
        }

        public long GetHumidityFromTemperature(long temperature)
        {
            foreach (RowMap map in TemperatureToHumidtyMap)
            {
                if (map.Source.IsInRange(temperature))
                {
                    return map.GetDestinationFromSource(temperature);
                }
            }

            // If we get here, it was not in any range
            // Return the same source as the destination - per AoC's Day 5 instructions
            return temperature;
        }

        public long GetLocationFromHumidity(long humidity)
        {
            foreach (RowMap map in HumidityToLocationMap)
            {
                if (map.Source.IsInRange(humidity))
                {
                    return map.GetDestinationFromSource(humidity);
                }
            }

            // If we get here, it was not in any range
            // Return the same source as the destination - per AoC's Day 5 instructions
            return humidity;
        }

        private void ParseSeedsAsRanges()
        {
            for (int i = 0; i < Seeds.Count; i += 2)
            {
                long seed = Seeds[i];
                long seedRange = Seeds[i + 1];
                SeedsAsRanges.Add(new NumericRange()
                {
                    Start = seed,
                    End = seed + seedRange - 1
                });
            }
        }

        /// <summary>
        /// Parses an entire AoC Day 5 Almanac input into an Almanac instance given a streamReader that is reading
        /// from the Almanac input text file.
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        public static Almanac ParseFromInput(StreamReader streamReader)
        {
            Almanac result = new();
            AlmanacParserState parserState = AlmanacParserState.SeedsLabel;

            string? line = null;

            // Iterate over each line until the line is null
            while ((line = streamReader.ReadLine()) != null)
            {
                string currentLineBuffer = "";
                List<long> currentNumbersParsed = [];

                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    bool isLastCharacterOnLine = i == line.Length - 1;
                    switch (parserState)
                    {
                        case AlmanacParserState.SeedsLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.Seeds;
                            }

                            break;
                        case AlmanacParserState.Seeds:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    result.Seeds.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    // There is a seed digit in the buffer, emit it
                                    result.Seeds.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (Char.IsAsciiLetter(c))
                            {
                                // A letter was hit, move to seed-to-soil map label
                                parserState = AlmanacParserState.SeedToSoilMapLabel;
                            }
                            break;
                        case AlmanacParserState.SeedToSoilMapLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.SeedToSoilMap;
                                currentNumbersParsed = [];
                            }

                            break;
                        case AlmanacParserState.SeedToSoilMap:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    result.SeedToSoilMap.Add(new RowMap()
                                    {
                                        Destination = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[0],
                                            End = currentNumbersParsed[0] + currentNumbersParsed[2] - 1
                                        },
                                        Source = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[1],
                                            End = currentNumbersParsed[1] + currentNumbersParsed[2] - 1
                                        }
                                    });
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (Char.IsAsciiLetter(c))
                            {
                                // Starts the next map
                                parserState = AlmanacParserState.SoilToFertilizerMapLabel;
                            }

                            break;
                        case AlmanacParserState.SoilToFertilizerMapLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.SoilToFertilizer;
                                currentNumbersParsed = [];
                            }

                            break;
                        case AlmanacParserState.SoilToFertilizer:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    result.SoilToFertilzerMap.Add(new RowMap()
                                    {
                                        Destination = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[0],
                                            End = currentNumbersParsed[0] + currentNumbersParsed[2] - 1
                                        },
                                        Source = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[1],
                                            End = currentNumbersParsed[1] + currentNumbersParsed[2] - 1
                                        }
                                    });
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (Char.IsAsciiLetter(c))
                            {
                                // Starts the next map
                                parserState = AlmanacParserState.FertilzerToWaterMapLabel;
                            }

                            break;
                        case AlmanacParserState.FertilzerToWaterMapLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.FertilzerToWater;
                                currentNumbersParsed = [];
                            }

                            break;
                        case AlmanacParserState.FertilzerToWater:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    result.FertilzerToWaterMap.Add(new RowMap()
                                    {
                                        Destination = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[0],
                                            End = currentNumbersParsed[0] + currentNumbersParsed[2] - 1
                                        },
                                        Source = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[1],
                                            End = currentNumbersParsed[1] + currentNumbersParsed[2] - 1
                                        }
                                    });
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (Char.IsAsciiLetter(c))
                            {
                                // Starts the next map
                                parserState = AlmanacParserState.WaterToLightMapLabel;
                            }

                            break;
                        case AlmanacParserState.WaterToLightMapLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.WaterToLight;
                                currentNumbersParsed = [];
                            }

                            break;
                        case AlmanacParserState.WaterToLight:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    result.WaterToLightMap.Add(new RowMap()
                                    {
                                        Destination = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[0],
                                            End = currentNumbersParsed[0] + currentNumbersParsed[2] - 1
                                        },
                                        Source = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[1],
                                            End = currentNumbersParsed[1] + currentNumbersParsed[2] - 1
                                        }
                                    });
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (Char.IsAsciiLetter(c))
                            {
                                // Starts the next map
                                parserState = AlmanacParserState.LightToTemperatureMapLabel;
                            }

                            break;
                        case AlmanacParserState.LightToTemperatureMapLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.LightToTemperature;
                                currentNumbersParsed = [];
                            }

                            break;
                        case AlmanacParserState.LightToTemperature:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    result.LightToTemperatureMap.Add(new RowMap()
                                    {
                                        Destination = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[0],
                                            End = currentNumbersParsed[0] + currentNumbersParsed[2] - 1
                                        },
                                        Source = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[1],
                                            End = currentNumbersParsed[1] + currentNumbersParsed[2] - 1
                                        }
                                    });
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (Char.IsAsciiLetter(c))
                            {
                                // Starts the next map
                                parserState = AlmanacParserState.TemperatureToHumidityMapLabel;
                            }

                            break;
                        case AlmanacParserState.TemperatureToHumidityMapLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.TemperatureToHumidity;
                                currentNumbersParsed = [];
                            }

                            break;
                        case AlmanacParserState.TemperatureToHumidity:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    result.TemperatureToHumidtyMap.Add(new RowMap()
                                    {
                                        Destination = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[0],
                                            End = currentNumbersParsed[0] + currentNumbersParsed[2] - 1
                                        },
                                        Source = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[1],
                                            End = currentNumbersParsed[1] + currentNumbersParsed[2] - 1
                                        }
                                    });
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }
                            else if (Char.IsAsciiLetter(c))
                            {
                                // Starts the next map
                                parserState = AlmanacParserState.HumidityToLocationMapLabel;
                            }

                            break;
                        case AlmanacParserState.HumidityToLocationMapLabel:
                            if (c == ':')
                            {
                                parserState = AlmanacParserState.HumidityToLocation;
                                currentNumbersParsed = [];
                            }

                            break;
                        case AlmanacParserState.HumidityToLocation:
                            if (Char.IsDigit(c))
                            {
                                currentLineBuffer += c;

                                if (isLastCharacterOnLine)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    result.HumidityToLocationMap.Add(new RowMap()
                                    {
                                        Destination = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[0],
                                            End = currentNumbersParsed[0] + currentNumbersParsed[2] - 1
                                        },
                                        Source = new NumericRange()
                                        {
                                            Start = currentNumbersParsed[1],
                                            End = currentNumbersParsed[1] + currentNumbersParsed[2] - 1
                                        }
                                    });
                                }
                            }
                            else if (c == ' ')
                            {
                                if (currentLineBuffer.Length > 0)
                                {
                                    currentNumbersParsed.Add(Int64.Parse(currentLineBuffer));
                                    currentLineBuffer = "";
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }

            // For part 2
            result.ParseSeedsAsRanges();

            return result;
        }
    }
}
