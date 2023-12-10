using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.SolutionsByDay.Day10
{
    internal class Grid
    {
        public List<List<GridPoint>> Rows = [];

        public GridPoint GetGridPointAtPosition(int Row, int Column)
        {
            if (Row >= 0 && Row < Rows.Count)
            {
                if (Column >= 0 && Column < Rows[Row].Count)
                {
                    return Rows[Row][Column];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<GridPoint?> GetAdjacentGridPoints(GridPoint fromPoint)
        {
            return [
                GetGridPointAtPosition(fromPoint.Row - 1, fromPoint.Column),
                GetGridPointAtPosition(fromPoint.Row, fromPoint.Column + 1),
                GetGridPointAtPosition(fromPoint.Row + 1, fromPoint.Column),
                GetGridPointAtPosition(fromPoint.Row, fromPoint.Column - 1)
            ];
        }

        public List<GridPoint> GetConnectingUntraversedGridPoints(GridPoint fromPoint)
        {
            List<GridPoint> untraversedGridPoints = [];
            List<GridPoint?> adjacentGridPoints = GetAdjacentGridPoints(fromPoint);
            foreach(GridPoint? point in adjacentGridPoints)
            {
                if (point != null)
                {
                    if (point.DistanceFromStart == null)
                    {
                        if (fromPoint.CanMoveTo(point))
                        {
                            untraversedGridPoints.Add(point);
                        }
                    }
                }
            }

            return untraversedGridPoints;
        }
    }
}
