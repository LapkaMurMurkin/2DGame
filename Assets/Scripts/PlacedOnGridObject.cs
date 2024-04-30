using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class PlacedOnGridObject : MonoBehaviour
{
    [SerializeField]
    private Cell _positionOnGrid = null;
    public Cell PositionOnGrid
    {
        get { return _positionOnGrid; }

        set
        {
            if (_positionOnGrid is not null)
            {
                _positionOnGrid.FreeCell();
                PrefabUtility.RecordPrefabInstancePropertyModifications(_positionOnGrid);
            }

            _positionOnGrid = value;

            _positionOnGrid.OccupyCell(this);
            PrefabUtility.RecordPrefabInstancePropertyModifications(_positionOnGrid);
        }
    }
}
