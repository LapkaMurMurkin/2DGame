using System;
using System.Collections.Generic;

using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;

[ExecuteAlways]
public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public HexGrid HexGrid = null;
    public Vector3Int HexCoordinates;
    public int PathDifficulty = 1;

    public List<Cell> AdjacentCells = null;

    [SerializeField]
    public TextMeshPro TextOverCell = null;
    public CellVisualizer Visualizer = null;

    public PlacedOnGridObject ObjectOnCell = null;
    public bool IsOccupied = false;
    public bool HasEnemy = false;
    public bool HasPlayableCharacter = false;

    public static Action<Cell> CursorEntered;
    public static Action<Cell> CursorExited;
    public static Action<Cell, PointerEventData> Clicked;

    private void Awake()
    {
        Visualizer = new CellVisualizer(this);
    }

    private void OnEnable()
    {
        HexGrid.Cells.Add(this);
    }

    private void OnDisable()
    {
        HexGrid.Cells.Remove(this);
    }

    private void OnTransformParentChanged()
    {
        HexGrid = transform.parent.GetComponent<HexGrid>();
    }

    public void FreeCell()
    {
        ObjectOnCell = null;
        IsOccupied = false;
        HasEnemy = false;
        HasPlayableCharacter = false;
    }

    public void OccupyCell(PlacedOnGridObject placedOnGridObject)
    {
        FreeCell();

        ObjectOnCell = placedOnGridObject;

        IsOccupied = true;

        if (ObjectOnCell is Enemy)
            HasEnemy = true;

        if (ObjectOnCell is PlayableCharacter)
            HasPlayableCharacter = true;
    }

    public void UpdateAdjacentCellsInclusive()
    {
        UpdateAdjacentCells();
        foreach (Cell adjacentCell in AdjacentCells)
        {
            if (adjacentCell is not null)
                adjacentCell.UpdateAdjacentCells();
        }
    }

    public void UpdateAdjacentCells()
    {
        Vector3Int up = new Vector3Int(0, 0, 1) + HexCoordinates; // up
        Vector3Int upRight = new Vector3Int(1, 0, 0) + HexCoordinates; // upRight
        Vector3Int downRight = new Vector3Int(1, 0, -1) + HexCoordinates; // downRight
        Vector3Int down = new Vector3Int(0, 0, -1) + HexCoordinates; // down
        Vector3Int downLeft = new Vector3Int(-1, 0, 0) + HexCoordinates; // downLeft
        Vector3Int upLeft = new Vector3Int(-1, 0, 1) + HexCoordinates; // upLeft

        Vector3Int[] sides = new[] { up, upRight, downRight, down, downLeft, upLeft };

        AdjacentCells = new List<Cell>();

        foreach (Vector3Int side in sides)
        {
            Cell sideCell = HexGrid.Cells.Find(cell => cell.HexCoordinates == side);
            AdjacentCells.Add(sideCell);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorEntered?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorExited?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this, eventData);
    }
}
