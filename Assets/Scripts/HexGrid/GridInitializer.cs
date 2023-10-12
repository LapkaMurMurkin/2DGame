using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Scripting;

namespace Game.HexGrid
{
    [ExecuteAlways]
    public class GridInitializer : MonoBehaviour
    {
        [ReadOnly]
        public List<Cell> Grid;

        private const float CELL_OUTER_RADIUS = 1;
        private const float CELL_INNER_RADIUS = CELL_OUTER_RADIUS * 0.866025404f;

        private void Awake()
        {
            Grid = new List<Cell>();
        }

        private void OnEnable()
        {
            CellEditorMode.Created.AddListener(AcceptCellCreation);
            CellEditorMode.Changed.AddListener(AcceptCellChanges);
            CellEditorMode.Deleted.AddListener(AcceptCellDeletion);
        }

        private void OnDisable()
        {
            CellEditorMode.Created.RemoveListener(AcceptCellCreation);
            CellEditorMode.Changed.RemoveListener(AcceptCellChanges);
            CellEditorMode.Deleted.RemoveListener(AcceptCellDeletion);
        }

        private void AcceptCellCreation(Cell cell)
        {
            Grid.Add(cell);
            cell.transform.SetParent(this.transform);
            UpdateAdjacentCellsInclusive(cell);
            SortGridByName();
        }

        private void AcceptCellChanges(Cell cell)
        {
            UpdateAdjacentCellsInclusive(cell);
            SortGridByName();
        }

        private void AcceptCellDeletion(Cell cell)
        {
            Grid.Remove(cell);
        }

        private void SortGridByName()
        {
            Grid = Grid.OrderBy(cell => cell.name).ToList();

            for (int i = 0; i < Grid.Count; i++)
            {
                Grid[i].transform.SetSiblingIndex(i);
            }
        }

        private void UpdateAdjacentCellsInclusive(Cell cell)
        {
            UpdateAdjacentCells(cell);
            foreach (Cell adjacentCell in cell.AdjacentCells)
            {
                if (adjacentCell is not null)
                    UpdateAdjacentCells(adjacentCell);
            }
        }

        private void UpdateAdjacentCells(Cell cell)
        {
            Vector3Int up = new Vector3Int(0, 0, 1) + cell.HexCoordinates; // up
            Vector3Int upRight = new Vector3Int(1, 0, 0) + cell.HexCoordinates; // upRight
            Vector3Int downRight = new Vector3Int(1, 0, -1) + cell.HexCoordinates; // downRight
            Vector3Int down = new Vector3Int(0, 0, -1) + cell.HexCoordinates; // down
            Vector3Int downLeft = new Vector3Int(-1, 0, 0) + cell.HexCoordinates; // downLeft
            Vector3Int upLeft = new Vector3Int(-1, 0, 1) + cell.HexCoordinates; // upLeft

            Vector3Int[] sides = new[] { up, upRight, downRight, down, downLeft, upLeft };

            cell.AdjacentCells = new List<Cell>();

            foreach (Vector3Int side in sides)
            {
                Cell sideCell = Grid.Find(cell => cell.HexCoordinates == side);
                cell.AdjacentCells.Add(sideCell);
            }
        }
    }
}