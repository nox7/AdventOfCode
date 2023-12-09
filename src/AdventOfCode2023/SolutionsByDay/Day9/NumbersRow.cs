using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day9
{
    internal class NumbersRow
    {
        /// <summary>
        /// Individual numbers parsed from a single input line
        /// </summary>
        public List<long> Numbers = [];

        /// <summary>
        /// This will stay null if the sum of all numbers in Numbers is 0
        /// </summary>
        public NumbersRow? ExtrapolatedRow = null;

        /// <summary>
        /// Extrapolates the Numbers into another NumberRow based off the difference between pairs of numbers.
        /// Recursively does this until a number row of all 0s is encountered.
        /// </summary>
        public void ExtrapolateNumbersIntoRows()
        {
            NumbersRow extrapolatedRow = new();
            for (int i = 0; i < Numbers.Count - 1; i++)
            {
                long diff = Numbers[i + 1] - Numbers[i];
                extrapolatedRow.Numbers.Add(diff);
            }

            // Set this object's property
            ExtrapolatedRow = extrapolatedRow;
            
            // Check if this row is not a 0-row and needs to be further extrapolated
            if (extrapolatedRow.Numbers.All(num => num == 0) == false)
            {
                // The extrapolated row also needs to be extrapolated
                extrapolatedRow.ExtrapolateNumbersIntoRows();
            }
        }

        /// <summary>
        /// Uses the extrapolated rows to determine the next expected number for the row.
        /// 
        /// AoC Day 9 P1 says to use the end of the current line's number + the end of the row below's number. If the row below is null, then it is just 0.
        /// </summary>
        /// <returns></returns>
        public long GetNextExpectedNumberInRow()
        {
            // Return 0 for 0-rows
            if (ExtrapolatedRow == null)
            {
                return 0;
            }

            long endOfCurrentRow = Numbers[Numbers.Count - 1];
            long endOfBelowRow = ExtrapolatedRow.GetNextExpectedNumberInRow();
            return endOfCurrentRow + endOfBelowRow;
        }

        /// <summary>
        /// Uses the extrapolated rows to determine the previous expected number for the row.
        /// 
        /// AoC Day 9 P2 says to use the start of the current line's number + the start of the row below's number. If the row below is null, then it is just 0.
        /// </summary>
        /// <returns></returns>
        public long GetPreviousExpectedNumberInRow()
        {
            // Return 0 for 0-rows
            if (ExtrapolatedRow == null)
            {
                return 0;
            }

            long startOfCurrentRow = Numbers[0];
            long startOfBelowRow = ExtrapolatedRow.GetPreviousExpectedNumberInRow();
            return startOfCurrentRow - startOfBelowRow;
        }
    }
}
