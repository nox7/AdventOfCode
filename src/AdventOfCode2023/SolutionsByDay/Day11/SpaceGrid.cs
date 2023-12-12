using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day11
{
    internal class SpaceGrid
    {
        public List<List<SpaceLocation>> Objects { get; set; } = [];

        /// <summary>
        /// A cache of whether or not a row is an empty space to avoid duplicate lookups and save a ton of processing power.
        /// </summary>
        public Dictionary<int, bool> RowIsEmptySpaceCache { get; set; } = [];
        /// <summary>
        /// A cache of whether or not a column is an empty space to avoid duplicate lookups and save a ton of processing power.
        /// </summary>
        public Dictionary<int, bool> ColumnIsEmptySpaceCache { get; set; } = [];

        /// <summary>
        /// Determines if an entire row of space is empty
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool IsRowEmpty(int rowIndex)
        {
            if (!RowIsEmptySpaceCache.TryGetValue(rowIndex, out bool result))
            {
                List<SpaceLocation> row = Objects[rowIndex];
                foreach (SpaceLocation s in row)
                {
                    if (!s.IsEmptySpace)
                    {
                        RowIsEmptySpaceCache[rowIndex] = false;
                        return false;
                    }
                }

                RowIsEmptySpaceCache[rowIndex] = true;
                return true;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Determines if an entire column of space is empty
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public bool IsColumnEmpty(int columnIndex)
        {
            if (!ColumnIsEmptySpaceCache.TryGetValue(columnIndex, out bool result))
            {
                foreach (List<SpaceLocation> row in Objects)
                {
                    SpaceLocation s = row[columnIndex];
                    if (!s.IsEmptySpace)
                    {
                        ColumnIsEmptySpaceCache[columnIndex] = false;
                        return false;
                    }
                }

                ColumnIsEmptySpaceCache[columnIndex] = true;
                return true;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Calculates the number of empty rows between two space locations
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public int GetNumEmptyRowsBetweenLocations(SpaceLocation location1, SpaceLocation location2)
        {
            int startRow = Math.Min(location1.Row, location2.Row);
            int endRow = Math.Max(location1.Row, location2.Row);
            
            if (startRow == endRow)
            {
                return 0;
            }
            else
            {
                int counter = 0;
                for (int i = startRow; i < endRow; i++)
                {
                    if (IsRowEmpty(i))
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }

        /// <summary>
        /// Calculates the number of empty columns between two space locations
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public int GetNumEmptyColumnsBetweenLocations(SpaceLocation location1, SpaceLocation location2)
        {
            int startColumn = Math.Min(location1.Column, location2.Column);
            int endColumn = Math.Max(location1.Column, location2.Column);

            if (startColumn == endColumn)
            {
                return 0;
            }
            else
            {
                int counter = 0;
                for (int i = startColumn; i < endColumn; i++)
                {
                    if (IsColumnEmpty(i))
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }

        /// <summary>
        /// Determines the sum of the horizontal + vertical distance. Providing spaceExpansionModifier of 1 will add 1 for every empty row or column there is (space expansion). Set it to 0 for no expansion.
        /// 
        /// For part to, send in 999,999 and not 1,000,000 because it will add 999,999 for a total of one-million in expansion.
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <param name="spaceExpansionModifier"></param>
        /// <returns></returns>
        public long ManhattanDistance(SpaceLocation location1, SpaceLocation location2, int spaceExpansionModifier)
        {
            int largestRow = Math.Max(location1.Row, location2.Row);
            int smallestRow = Math.Min(location1.Row, location2.Row);
            int largestColumn = Math.Max(location1.Column, location2.Column);
            int smallestColumn = Math.Min(location1.Column, location2.Column);

            int numEmptyRowsBetweenLocations = GetNumEmptyRowsBetweenLocations(location1, location2) * spaceExpansionModifier;
            int numEmptyColumnsBetweenLocations = GetNumEmptyColumnsBetweenLocations(location1, location2) * spaceExpansionModifier;

            return (largestRow - smallestRow + numEmptyRowsBetweenLocations) 
                + (largestColumn - smallestColumn + numEmptyColumnsBetweenLocations);
        }
    }
}
