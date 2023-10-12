using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using UnityEditor;

using TMPro;

namespace Game.HexGrid
{
    [RequireComponent(typeof(CellEditorMode))]
    public class Cell : MonoBehaviour
    {
        public const float CELL_OUTER_RADIUS = 1;
        public const float CELL_INNER_RADIUS = CELL_OUTER_RADIUS * 0.866025404f;

        [ReadOnly]
        public Vector3Int HexCoordinates;
        [ReadOnly]
        public TextMeshPro TextOverCell;

        [ReadOnly]
        public States State;
        public enum States
        {
            ACTIVE,
            INACTIVE,
            BLOCKED
        };

        [ReadOnly]
        public List<Cell> AdjacentCells;
        public enum Side
        {
            UP,
            RIGHT_UP,
            RIGHT_DOWN,
            DOWN,
            LEFT_DOWN,
            LEFT_UP
        };

        public static UnityEvent<Cell> MouseEntered = new UnityEvent<Cell>();
        public static UnityEvent<Cell> MouseExited = new UnityEvent<Cell>();

        private void Awake()
        {
            TextOverCell = GetComponent<TextMeshPro>();
        }

        private void OnMouseEnter()
        {
            MouseEntered.Invoke(this);
            Debug.Log(this.name);
        }

        private void OnMouseExit()
        {
            MouseExited.Invoke(this);
        }

        public void SetTextOverCell(string text)
        {
            TextOverCell.text = text;
        }

        public void SetActiveState()
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            State = States.ACTIVE;
        }

        public void SetInactiveState()
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.gray);
            State = States.INACTIVE;
        }

        public void SetBlockedState()
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            State = States.BLOCKED;
        }

        public void EnableHighlight()
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        }

        public void DisableHighlight()
        {
            gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.gray);
        }
    }
}