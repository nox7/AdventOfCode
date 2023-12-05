using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day5
{
    internal class NumericRange
    {
        public required long Start;
        public required long End;

        /// <summary>
        /// Determines if a provided source number, is within the range of SourceStart + (Range - 1).
        /// We subtract one from the Range because the AoC says the numbers are inclusive, so the SourceStart
        /// is considered a valid answer.
        /// </summary>
        /// <returns></returns>
        public bool IsInRange(long Value)
        {
            return Value >= Start && Value <= End;
        }
    }
}
