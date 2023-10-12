using UnityEngine;
using UnityEngine.Events;

using UnityEditor;

namespace Game.HexGrid
{
    [ExecuteAlways]
    public class CellEditorMode : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Cell selfCell;

        public static UnityEvent<Cell> Created = new UnityEvent<Cell>();
        public static UnityEvent<Cell> Changed = new UnityEvent<Cell>();
        public static UnityEvent<Cell> Deleted = new UnityEvent<Cell>();

        private void Awake()
        {
            selfCell = this.GetComponent<Cell>();
        }

        private void Start()
        {
            UpdateCoordinates();
            Created.Invoke(selfCell);
        }

        private void Update()
        {
            if (transform.hasChanged)
            {
                UpdateCoordinates();
                Changed.Invoke(selfCell);
                transform.hasChanged = false;
            }
        }

        private void OnDestroy()
        {
            Deleted.Invoke(selfCell);
        }

        private void UpdateCoordinates()
        {
            CalculateCoordinatesInGrid();

            name = "Cell " + selfCell.HexCoordinates;
            selfCell.TextOverCell.text = "x: " + selfCell.HexCoordinates.x + "\n" + "z: " + selfCell.HexCoordinates.z;

            PrefabUtility.RecordPrefabInstancePropertyModifications(selfCell);
            PrefabUtility.RecordPrefabInstancePropertyModifications(selfCell.TextOverCell);
        }

        private void CalculateCoordinatesInGrid()
        {
            float coordinateX = transform.localPosition.x;
            float coordinateZ = transform.localPosition.z;

            int hexCoordinateX = Mathf.RoundToInt(coordinateX / (Cell.CELL_OUTER_RADIUS * 1.5f));
            int hexCoordinateZ = Mathf.RoundToInt(coordinateZ / Cell.CELL_INNER_RADIUS - hexCoordinateX) / 2;

            selfCell.HexCoordinates = new Vector3Int(hexCoordinateX, 0, hexCoordinateZ);

            float alignedCoordinateX = hexCoordinateX * (Cell.CELL_OUTER_RADIUS * 1.5f);
            float alignedCoordinateZ = hexCoordinateZ * Cell.CELL_INNER_RADIUS * 2 + hexCoordinateX * Cell.CELL_INNER_RADIUS;

            transform.position = new Vector3(alignedCoordinateX, 0, alignedCoordinateZ);
        }
    }
}