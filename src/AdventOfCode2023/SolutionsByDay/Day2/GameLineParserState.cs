using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day2
{
    internal enum GameLineParserState
    {
        GameLabel,
        GameId,
        GameCubeRollCount,
        GameCubeColorName
    }
}
