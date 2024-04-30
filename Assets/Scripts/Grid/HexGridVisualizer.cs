using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexGridVisualizer
{
    public bool isMovementPathFreezed { get; private set; }
    public List<Cell> MovementPath { get; private set; }
    private BezierPath _bezierPath;
    public List<Cell> MovementRadius { get; private set; }
    public Cell CursorPosition { get; private set; }

    public void ShowCursorPosition(Cell cell)
    {
        if (CursorPosition is not null)
            HideCursorPosition();

        CursorPosition = cell;
        CursorPosition.Visualizer.EnableCursorHighlight();
    }

    public void HideCursorPosition()
    {
        if (CursorPosition is not null)
            CursorPosition.Visualizer.DisableCursorHighlight();

        CursorPosition = null;
    }


    public void ShowMovementRadius(List<Cell> movementRadius)
    {
        if (MovementRadius is not null)
            HideMovementRadius();

        MovementRadius = movementRadius;

        foreach (Cell cell in MovementRadius ?? Enumerable.Empty<Cell>())
            cell.Visualizer.EnableRadiusHighlight();
    }

    public void HideMovementRadius()
    {
        foreach (Cell cell in MovementRadius ?? Enumerable.Empty<Cell>())
            cell.Visualizer.DisableRadiusHighlight();

        MovementRadius = null;
    }

    public void ShowMovementPath(List<Cell> movementPath, int movementPoints)
    {
        if (MovementPath is not null)
            HideMovementPath();

        MovementPath = movementPath;

        foreach (Cell cell in MovementPath ?? Enumerable.Empty<Cell>())
            if (movementPoints > 0)
            {
                cell.Visualizer.SwitchPathHighlightToReachable();
                movementPoints--;
            }
            else
                cell.Visualizer.SwitchPathHighlightToUnreachable();

               // _bezierPath = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void HideMovementPath()
    {
        foreach (Cell cell in MovementPath ?? Enumerable.Empty<Cell>())
            cell.Visualizer.DisabelPathHighlight();

        MovementPath = null;
    }

    public void FreezeMovementPath()
    {
        isMovementPathFreezed = true;
    }

    public void UnfreezeMovementPath()
    {
        isMovementPathFreezed = false;
    }
}
