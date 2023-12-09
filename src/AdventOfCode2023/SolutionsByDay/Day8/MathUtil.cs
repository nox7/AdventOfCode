using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day8
{
    internal class MathUtil
    {
        public static long GreatestCommonDivisor(long a, long b)
        {
            return b == 0 ? a: GreatestCommonDivisor(b, a % b);
        }

        public static long LeastCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }
    }
}
