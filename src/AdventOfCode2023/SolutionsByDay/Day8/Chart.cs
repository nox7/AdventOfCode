using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day8
{
    /// <summary>
    /// Represents a line of the map document
    /// // E.g. AAA = (BBB, CCC)
    /// </summary>
    internal class Chart
    {
        public required string LocationAsString { get; set; }
        public required char[] LocationAsCharArray { get; set; }
        public required string LeftDirectionAsString { get; set; }
        public required string RightDirectionAsString { get; set; }
    }
}
