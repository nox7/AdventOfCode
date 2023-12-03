using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day3
{
    internal class EngineNumber : IEquatable<EngineNumber>
    {
        public int Id;
        public int Row;
        public int Number;

        /// <summary>
        /// In the schematic, the indices that this number spans on the Row it was found
        /// </summary>
        public List<int> ColumnSpan = [];

        public bool Equals(EngineNumber? other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }
    }
}
