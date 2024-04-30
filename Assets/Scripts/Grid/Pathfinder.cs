using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public static class Pathfinder
{
    private class AStarAlgorithmNode
    {
        public Cell Cell;
        public int PathDifficulty;
        public AStarAlgorithmNode CameFromNode;
    }

    private static AStarAlgorithmNode InitializeAStarNode(Cell currentCell, AStarAlgorithmNode cameFromNode)
    {
        AStarAlgorithmNode aStarNode = new AStarAlgorithmNode();
        aStarNode.Cell = currentCell;

        if (cameFromNode is not null)
            aStarNode.PathDifficulty = currentCell.PathDifficulty + cameFromNode.PathDifficulty;
        else
            aStarNode.PathDifficulty = currentCell.PathDifficulty;

        aStarNode.CameFromNode = cameFromNode;

        return aStarNode;
    }

    public static int CalculateDistanceBetweenCells(Cell startCell, Cell endCell)
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

    public static List<Cell> FindMovementPath(Cell startPosition, Cell targetPosition)
    {
        List<AStarAlgorithmNode> movementMap = CreateMovementMap(startPosition);
        AStarAlgorithmNode targetPositionNode = movementMap.Find(node => node.Cell == targetPosition);
        return ReconstructPathInCells(targetPositionNode);
    }

    public static List<Cell> FindMovementRadius(Cell currentPosition, int remainingMovementPoints)
    {
        List<AStarAlgorithmNode> movementMap = CreateMovementMap(currentPosition);
        List<Cell> movementRadius = new List<Cell>();

        foreach (AStarAlgorithmNode node in movementMap)
            if (node.PathDifficulty <= remainingMovementPoints)
                movementRadius.Add(node.Cell);

        movementRadius.Remove(currentPosition);
        
        return movementRadius;
    }

    private static List<AStarAlgorithmNode> CreateMovementMap(Cell startPosition)
    {
        List<AStarAlgorithmNode> nodesToCheck = new List<AStarAlgorithmNode>();
        List<AStarAlgorithmNode> nodesAlreadyChecked = new List<AStarAlgorithmNode>();

        AStarAlgorithmNode currentNode = InitializeAStarNode(startPosition, null);
        currentNode.PathDifficulty = 0;
        nodesToCheck.Add(currentNode);

        while (nodesToCheck.Any())
        {
            currentNode = FindMinPathDifficultyNode(nodesToCheck);
            nodesToCheck.Remove(currentNode);
            nodesAlreadyChecked.Add(currentNode);

            foreach (Cell adjacentCell in currentNode.Cell.AdjacentCells)
            {
                if (adjacentCell is null || adjacentCell.IsOccupied || nodesAlreadyChecked.Any(node => node.Cell == adjacentCell))
                {
                    continue;
                }

                AStarAlgorithmNode adjacentCellNode = nodesToCheck.Find(node => node.Cell == adjacentCell);

                if (adjacentCellNode is null)
                {
                    adjacentCellNode = InitializeAStarNode(adjacentCell, currentNode);
                    nodesToCheck.Add(adjacentCellNode);
                }
                else if (adjacentCellNode.PathDifficulty > adjacentCell.PathDifficulty + currentNode.PathDifficulty)
                {
                    adjacentCellNode = InitializeAStarNode(adjacentCell, currentNode);
                }
            }
        }

        return nodesAlreadyChecked;
    }

    private static AStarAlgorithmNode FindMinPathDifficultyNode(List<AStarAlgorithmNode> nodesToCheck)
    {
        AStarAlgorithmNode minPathDifficultyNode = nodesToCheck[0];

        foreach (AStarAlgorithmNode node in nodesToCheck)
        {
            if (node.PathDifficulty < minPathDifficultyNode.PathDifficulty)
                minPathDifficultyNode = node;
        }

        return minPathDifficultyNode;
    }

    private static List<Cell> ReconstructPathInCells(AStarAlgorithmNode lastNodeOfPath)
    {
        List<Cell> path = new List<Cell>();

        while (lastNodeOfPath is not null)
        {
            path.Insert(0, lastNodeOfPath.Cell);
            lastNodeOfPath = lastNodeOfPath.CameFromNode;
        }

        return path.Skip(1).ToList();
    }
}
