using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day5
{
    internal enum AlmanacParserState
    {
        SeedsLabel,
        Seeds,
        SeedToSoilMapLabel,
        SeedToSoilMap,
        SoilToFertilizerMapLabel,
        SoilToFertilizer,
        FertilzerToWaterMapLabel,
        FertilzerToWater,
        WaterToLightMapLabel,
        WaterToLight,
        LightToTemperatureMapLabel,
        LightToTemperature,
        TemperatureToHumidityMapLabel,
        TemperatureToHumidity,
        HumidityToLocationMapLabel,
        HumidityToLocation,
    }
}
