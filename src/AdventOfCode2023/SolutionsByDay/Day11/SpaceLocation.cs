using AdventOfCode2023.SolutionsByDay.Day5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day11
{
    /// <summary>
    /// A location in space. Either empty space or a galaxy. GalaxyId is always 0 for empty space.
    /// </summary>
    internal class SpaceLocation
    {
        public int GalaxyId = 0;
        public int Row { get; set; } = 0;
        public int Column { get; set; } = 0;
        public bool IsEmptySpace { get; set; } = true;
    }
}
