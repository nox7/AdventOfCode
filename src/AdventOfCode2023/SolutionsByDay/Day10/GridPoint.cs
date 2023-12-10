using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day10
{
    internal class GridPoint
    {
        public required int Row { get; set; }
        public required int Column { get; set; }
        public required char Character { get; set; }
        public int? DistanceFromStart { get; set; } = null;

        public Direction GetDirectionToOtherPoint(GridPoint otherPoint)
        {
            if (otherPoint.Row == Row)
            {
                // Same row. East or west
                if (otherPoint.Column > Column)
                {
                    return Direction.East;
                }
                else
                {
                    return Direction.West;
                }
            }
            else
            {
                if (otherPoint.Row > Row)
                {
                    return Direction.South;
                }
                else
                {
                    return Direction.North;
                }
            }
        }

        public bool CanMoveTo(GridPoint otherPoint)
        {
            char currentCharacter = Character;
            char otherCharacter = otherPoint.Character;
            Direction inDirection = GetDirectionToOtherPoint(otherPoint);
            return MovementValidator.CanMoveTo(currentCharacter, otherCharacter, inDirection);
        }
    }
}
