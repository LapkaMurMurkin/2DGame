using System;
using System.Collections.Generic;

namespace Game.HexGrid
{
    public class Pathfinder
    {
        public int CalculateDistanceBetweenCells(Cell startCell, Cell endCell)
        {
            int distanceProjection_XAxis = startCell.HexCoordinates.x - endCell.HexCoordinates.x;
            int distanceProjection_ZAxis = startCell.HexCoordinates.z - endCell.HexCoordinates.z;

            int distanceProjection_XAxisModule = Math.Abs(distanceProjection_XAxis);
            int distanceProjection_ZAxisModule = Math.Abs(distanceProjection_ZAxis);

            bool isHasSameSign = Math.Sign(distanceProjection_XAxis) == Math.Sign(distanceProjection_ZAxis);

            if (isHasSameSign)
                return distanceProjection_XAxisModule + distanceProjection_ZAxisModule;
            else
                return Math.Max(distanceProjection_XAxisModule, distanceProjection_ZAxisModule);
        }

        public List<Cell> FindPath(Cell startCell, Cell endCell)
        {
            List<Cell> path = new List<Cell>();

            path.Add(startCell);

            int distance = CalculateDistanceBetweenCells(startCell, endCell);

            while (startCell != endCell)
            {
                foreach (Cell sideCell in startCell.AdjacentCells)
                {
                    if (sideCell is not null)
                    {
                        int sideCellDistance = CalculateDistanceBetweenCells(sideCell, endCell);

                        if (sideCellDistance < distance)
                        {
                            startCell = sideCell;
                            distance = sideCellDistance;
                        }
                    }
                }
                path.Add(startCell);
            }

            return path;
        }
    }
}