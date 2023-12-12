using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day12
{
    internal class SpringArrangementsSolver
    {

        public static Dictionary<string, long> ArrangementCache = [];

        /// <summary>
        /// Recursively solves a line's possible spring combinations while also utilizing a cache
        /// to avoid re-solving a similar outcome (makes Part 2 actually solveable in under 300ms as opposed to 400 years)
        /// </summary>
        /// <param name="springsLine"></param>
        /// <param name="springGroupCounts"></param>
        /// <param name="currentPosition"></param>
        /// <param name="currentSpringGroupCountIndex"></param>
        /// <param name="currentLengthOfHashtagGroup"></param>
        /// <returns></returns>
        public long GetNumArrangements(
            string springsLine, 
            int[] springGroupCounts, 
            int currentPosition, 
            int currentSpringGroupCountIndex,
            int currentLengthOfHashtagGroup
            )
        {
            string cacheKey = String.Format("{0}_{1}_{2}", currentPosition, currentSpringGroupCountIndex, currentLengthOfHashtagGroup);
            if (ArrangementCache.TryGetValue(cacheKey, out long numArrangements))
            {
                return numArrangements;
            }
            else
            {
                // Are we at the end of the springs entire line?
                if (currentPosition == springsLine.Length)
                {
                    if (currentSpringGroupCountIndex == springGroupCounts.Length && currentLengthOfHashtagGroup == 0)
                    {
                        return 1;
                    }
                    else if (currentSpringGroupCountIndex == springGroupCounts.Length - 1 && springGroupCounts[currentSpringGroupCountIndex] == currentLengthOfHashtagGroup)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    long answer = 0;
                    char[] validCharacters = ['.', '#'];
                    foreach (char c in validCharacters)
                    {
                        if (springsLine[currentPosition] == c || springsLine[currentPosition] == '?')
                        {
                            if (c == '.' && currentLengthOfHashtagGroup == 0)
                            {
                                answer += GetNumArrangements(
                                    springsLine, springGroupCounts, currentPosition + 1, currentSpringGroupCountIndex, 0
                                    );
                            }
                            else if (c == '.' && currentLengthOfHashtagGroup > 0 && currentSpringGroupCountIndex < springGroupCounts.Length && springGroupCounts[currentSpringGroupCountIndex] == currentLengthOfHashtagGroup)
                            {
                                answer += GetNumArrangements(
                                    springsLine, springGroupCounts, currentPosition + 1, currentSpringGroupCountIndex + 1, 0
                                    );
                            }
                            else if (c == '#')
                            {
                                answer += GetNumArrangements(
                                    springsLine, springGroupCounts, currentPosition + 1, 
                                    currentSpringGroupCountIndex, currentLengthOfHashtagGroup + 1
                                    );
                            }
                        }
                    }
                    ArrangementCache[cacheKey] = answer;
                    return answer;
                }
            }
        }
    }
}
