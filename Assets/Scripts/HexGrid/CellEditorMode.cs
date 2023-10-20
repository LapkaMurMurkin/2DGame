using UnityEngine;
using UnityEngine.Events;

namespace Game.HexGrid
{
    [ExecuteAlways]
    public class CellEditorMode : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Cell _selfCell;

        public static UnityEvent<Cell> Created = new UnityEvent<Cell>();
        public static UnityEvent<Cell> Changed = new UnityEvent<Cell>();
        public static UnityEvent<Cell> Deleted = new UnityEvent<Cell>();

        private void Awake()
        {
            _selfCell = this.GetComponent<Cell>();
        }

        private void Start()
        {
            UpdateCoordinates();
            Created.Invoke(_selfCell);
        }

        private void Update()
        {
            if (transform.hasChanged)
            {
                UpdateCoordinates();
                Changed.Invoke(_selfCell);
                transform.hasChanged = false;
            }
        }

        private void OnDestroy()
        {
            Deleted.Invoke(_selfCell);
        }

        private void UpdateCoordinates()
        {
            CalculateCoordinatesInGrid();

            name = "Cell " + _selfCell.HexCoordinates;
            _selfCell.TextOverCell.text = "x: " + _selfCell.HexCoordinates.x + "\n" + "z: " + _selfCell.HexCoordinates.z;
        }

        private void CalculateCoordinatesInGrid()
        {
            float coordinateX = transform.localPosition.x;
            float coordinateZ = transform.localPosition.z;

            int hexCoordinateX = Mathf.RoundToInt(coordinateX / (Cell.CELL_OUTER_RADIUS * 1.5f));
            int hexCoordinateZ = Mathf.RoundToInt(coordinateZ / Cell.CELL_INNER_RADIUS - hexCoordinateX) / 2;

            _selfCell.HexCoordinates = new Vector3Int(hexCoordinateX, 0, hexCoordinateZ);

            float alignedCoordinateX = hexCoordinateX * (Cell.CELL_OUTER_RADIUS * 1.5f);
            float alignedCoordinateZ = hexCoordinateZ * Cell.CELL_INNER_RADIUS * 2 + hexCoordinateX * Cell.CELL_INNER_RADIUS;

            transform.position = new Vector3(alignedCoordinateX, 0, alignedCoordinateZ);
        }
    }
}