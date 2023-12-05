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

        public bool IsEmpty()
        {
            return Start == End;
        }

        /// <summary>
        /// Calculates the intersection between this range and the provided other range. Returns null if there
        /// is no intersection.
        /// </summary>
        /// <param name="otherRange"></param>
        /// <returns></returns>
        public NumericRange? GetIntersection(NumericRange otherRange)
        {
            if (otherRange.Start > End || Start > otherRange.End)
            {
                return null;
            }

            return new NumericRange()
            {
                Start = Math.Max(Start, otherRange.Start),
                End = Math.Min(End, otherRange.End)
            };
        }

        /// <summary>
        /// Subtracts out a provided range from this range. Will modify the current range.
        /// 
        /// However, if this returns another range, then that is because the subtraction was in the middle of the current range.
        /// The current range will be modified to be the start of that subtraction. The returned range will be the end cap of the subtraction.
        /// </summary>
        /// <param name="otherRange"></param>
        /// <returns></returns>
        public NumericRange? Subtract(NumericRange otherRange)
        {
            // Determine if there will be two ranges as a result (otherRange is in the middle of this range)
            if (otherRange.Start > Start && otherRange.End < End)
            {
                // Two ranges
                // Modify this one to be the start
                var originalEnd = End;
                End = otherRange.Start - 1;

                return new NumericRange()
                {
                    Start = otherRange.End + 1,
                    End = originalEnd
                };
                /*return new List<NumericRange>()
                {
                    new NumericRange()
                    {
                        Start = Start,
                        End = otherRange.End
                    },
                    new NumericRange()
                    {
                        Start = otherRange.End,
                        End = End
                    }
                };*/
            }
            else
            {
                if (otherRange.Start == Start && otherRange.End == End)
                {
                    // This has been fully subtracted
                    // Set this to be empty
                    Start = End;
                    return null;
                }

                // Just one range
                if (otherRange.Start <= Start)
                {
                    /*return new NumericRange()
                    {
                        Start = otherRange.End,
                        End = End
                    };*/
                    Start = otherRange.End + 1;
                }
                else
                {
                    End = otherRange.Start - 1;
                    /*return new NumericRange()
                    {
                        Start = Start,
                        End = otherRange.Start
                    };*/
                }

                return null;
            }
        }
    }
}
