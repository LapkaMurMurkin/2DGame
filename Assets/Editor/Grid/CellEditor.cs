using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;



[CustomEditor(typeof(Cell))]
[CanEditMultipleObjects]
public class CellEditor : Editor
{
    private void OnEnable()
    {
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    private void DuringSceneGUI(SceneView SceneView)
    {
        foreach (Cell cell in serializedObject.targetObjects)
        {
            if (cell.transform.hasChanged)
            {
                UpdateCoordinates(cell);
                cell.transform.hasChanged = false;
            }

            cell.UpdateAdjacentCellsInclusive();
            
            PrefabUtility.RecordPrefabInstancePropertyModifications(cell);
            PrefabUtility.RecordPrefabInstancePropertyModifications(cell.TextOverCell);
        }
    }

    private void UpdateCoordinates(Cell cell)
    {
        CalculateCoordinatesInGrid(cell);

        cell.name = "Cell " + cell.HexCoordinates;

        cell.TextOverCell.text = "x: " + cell.HexCoordinates.x + "\n" + "z: " + cell.HexCoordinates.z;
    }

    private void CalculateCoordinatesInGrid(Cell cell)
    {
        float coordinateX = cell.transform.localPosition.x;
        float coordinateZ = cell.transform.localPosition.z;

        int hexCoordinateX = Mathf.RoundToInt(coordinateX / (Constants.CELL_OUTER_RADIUS * 1.5f));
        int hexCoordinateZ = Mathf.RoundToInt(coordinateZ / Constants.CELL_INNER_RADIUS - hexCoordinateX) / 2;

        cell.HexCoordinates = new Vector3Int(hexCoordinateX, 0, hexCoordinateZ);

        float alignedCoordinateX = hexCoordinateX * (Constants.CELL_OUTER_RADIUS * 1.5f);
        float alignedCoordinateZ = hexCoordinateZ * Constants.CELL_INNER_RADIUS * 2 + hexCoordinateX * Constants.CELL_INNER_RADIUS;

        cell.transform.position = new Vector3(alignedCoordinateX, 0, alignedCoordinateZ);
    }
}
