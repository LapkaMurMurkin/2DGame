using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlacedOnGridObject), true)]
public class PlacedOnGridObjectEditor : Editor
{
    private Raycaster _raycaster;

    private void Awake()
    {
        _raycaster = new Raycaster();
    }

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
        foreach (PlacedOnGridObject gridObject in serializedObject.targetObjects)
        {
            if (gridObject.transform.hasChanged)
            {
                UpdateCoordinates(gridObject);
                gridObject.transform.hasChanged = false;
            }

            PrefabUtility.RecordPrefabInstancePropertyModifications(gridObject);
        }
    }

    private void UpdateCoordinates(PlacedOnGridObject gridObject)
    {
        if (_raycaster.CastRayDownOnCellLayer(gridObject.transform.position + new Vector3(0, 0.1f, 0)))
        {
            GameObject cellObject = _raycaster.GetRayHitObject();
            Cell cellComponent = cellObject.GetComponent<Cell>();

            gridObject.transform.position = cellObject.transform.position;
            UpdateCellStates(gridObject.PositionOnGrid, cellComponent);

            gridObject.PositionOnGrid = cellComponent;
        }
    }
    private void UpdateCellStates(Cell exited, Cell entered)
    {
        if (exited is not null)
        {
            exited.FreeCell();
            PrefabUtility.RecordPrefabInstancePropertyModifications(exited);
        }

        entered.OccupyCell(serializedObject.targetObject as PlacedOnGridObject);
        PrefabUtility.RecordPrefabInstancePropertyModifications(entered);
    }
}
