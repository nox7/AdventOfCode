using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day5
{
    internal class RowMap
    {
        public required NumericRange Destination;
        public required NumericRange Source;

        public long GetDestinationFromSource(long SourceValue)
        {
            long Distance = SourceValue - Source.Start;
            return Destination.Start + Distance;
        }
    }
}
