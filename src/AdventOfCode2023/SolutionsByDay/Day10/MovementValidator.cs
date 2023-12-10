using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day10
{
    internal class MovementValidator
    {
        /// <summary>
        /// Determines if the animal can move inDirection to nextPipe from currentPipe
        /// </summary>
        /// <param name="currentPipe"></param>
        /// <param name="nextPipe"></param>
        /// <param name="inDirection">The direction to move towards</param>
        /// <returns></returns>
        public static bool CanMoveTo(char currentPipe, char nextPipe, Direction inDirection)
        {
            if (nextPipe == '.')
            {
                return false;
            }

            if (nextPipe == 'S')
            {
                return true;
            }

            if (currentPipe == '|')
            {
                // Only North and South
                if (inDirection == Direction.South)
                {
                    // Next pipe muset accept from the north
                    return nextPipe == '|' || nextPipe == 'L' || nextPipe == 'J';
                }
                else if (inDirection == Direction.North)
                {
                    // Next pipe must accept from the south
                    return nextPipe == '|' || nextPipe == '7' || nextPipe == 'F';
                }
            }
            else if (currentPipe == '-')
            {
                // Only East and West
                if (inDirection == Direction.East)
                {
                    // Next pipe must accept from the west
                    return nextPipe == '-' || nextPipe == 'J' || nextPipe == '7';
                }
                else if (inDirection == Direction.West)
                {
                    // Next pipe must accept from the east
                    return nextPipe == '-' || nextPipe == 'L' || nextPipe == 'F';
                }
            }
            else if (currentPipe == 'L')
            {
                // Only north and east
                if (inDirection == Direction.North)
                {
                    // Next pipe must accept from the south
                    return nextPipe == '|' || nextPipe == '7' || nextPipe == 'F';
                }
                else if (inDirection == Direction.East)
                {
                    // Next pipe must accept from the west
                    return nextPipe == '-' || nextPipe == 'J' || nextPipe == '7';
                }
            }
            else if (currentPipe == 'J')
            {
                // Only north and west
                if (inDirection == Direction.North)
                {
                    // Next pipe must accept from the south
                    return nextPipe == '|' || nextPipe == '7' || nextPipe == 'F';
                }
                else if (inDirection == Direction.West)
                {
                    // Next pipe must accept from the east
                    return nextPipe == '-' || nextPipe == 'L' || nextPipe == 'F';
                }
            }
            else if (currentPipe == '7')
            {
                // Only south and west
                if (inDirection == Direction.South)
                {
                    // Next pipe must accept from the north
                    return nextPipe == '|' || nextPipe == 'L' || nextPipe == 'J';
                }
                else if (inDirection == Direction.West)
                {
                    // Next pipe must accept from the east
                    return nextPipe == '-' || nextPipe == 'L' || nextPipe == 'F';
                }
            }
            else if (currentPipe == 'F')
            {
                // Only south and east
                if (inDirection == Direction.South)
                {
                    // Next pipe must accept from the north
                    return nextPipe == '|' || nextPipe == 'L' || nextPipe == 'J';
                }
                else if (inDirection == Direction.East)
                {
                    // Next pipe must accept from the west
                    return nextPipe == '-' || nextPipe == 'J' || nextPipe == '7';
                }
            }
            else if (currentPipe == 'S')
            {
                // All directions
                if (inDirection == Direction.North)
                {
                    // Next pipe must accept from the south
                    return nextPipe == '|' || nextPipe == '7' || nextPipe == 'F';
                }
                else if (inDirection == Direction.East)
                {
                    // Next pipe must accept from the west
                    return nextPipe == '-' || nextPipe == 'J' || nextPipe == '7';
                }
                else if (inDirection == Direction.South)
                {
                    // Next pipe must accept from the north
                    return nextPipe == '|' || nextPipe == 'L' || nextPipe == 'J';
                }
                else if (inDirection == Direction.West)
                {
                    // Next pipe must accept from the east
                    return nextPipe == '-' || nextPipe == 'L' || nextPipe == 'F';
                }
            }

            return false;
        }
    }
}
