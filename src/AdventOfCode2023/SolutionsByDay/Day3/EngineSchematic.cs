using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day3
{
    internal class EngineSchematic
    {
        public List<List<EngineNumber>> EngineNumbersByRow { get; set; } = new();

        /// <summary>
        /// Returns an engine number that relates to the Day 3 input map's line number (row)
        /// and the column of the engine schematic.
        /// 
        /// This is _not_ the [row][column of the EngineNumbersByRow multi-dimensional list
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public EngineNumber? GetEngineNumberAtRowColumn(int row, int column)
        {
            if (row > -1 && row < EngineNumbersByRow.Count)
            {
                // Valid row
                if (column > -1)
                {
                    // column _does not_ refer to the second index of the list
                    // it refers to one of the columns defined in EnguneNumber.ColumnSpan
                    foreach (EngineNumber engineNumber in EngineNumbersByRow[row])
                    {
                        if (engineNumber.ColumnSpan.Contains(column))
                        {
                            // Found the EngineNumber instance that spans the requested column
                            return engineNumber;
                        }
                    }
                }
            }

            return null;
        }
    }
}
