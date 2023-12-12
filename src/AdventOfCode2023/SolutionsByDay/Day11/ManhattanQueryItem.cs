using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day11
{
    internal class ManhattanQueryItem
    {
        public required int Row { get; set; } = 0;
        public required int Column { get; set; } = 0;
        public required int Distance { get; set; } = 0;
    }
}
