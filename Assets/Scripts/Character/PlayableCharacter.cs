using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

using Game.HexGrid;
using Game.InputSystem;


namespace Game.Character
{
    public class PlayableCharacter : MonoBehaviour
    {
        [ReadOnly]
        public Cell CharacterPositionOnGrid;

        private Pathfinder _pathfinder = new Pathfinder();
        private List<Cell> _path = null;

        public static UnityEvent<GameObject> PlayableCharacterSelected = new UnityEvent<GameObject>();

        private void OnEnable()
        {
            InputSystem.MouseListener.MouseClicked.AddListener(OnMouseClick);
            Cell.MouseEntered.AddListener(OnCursorEnteredCell);
            Cell.MouseExited.AddListener(OnCursorExitedCell);
        }

        private void OnDisable()
        {
            InputSystem.MouseListener.MouseClicked.RemoveListener(OnMouseClick);
            Cell.MouseExited.RemoveListener(OnCursorEnteredCell);
            Cell.MouseExited.RemoveListener(OnCursorExitedCell);
        }

        private void OnMouseClick(MouseClick mouseClick)
        {
            if (mouseClick.Button == MouseClick.ButtonName.LEFT && mouseClick.Phase == MouseClick.PhaseName.CANCELED && mouseClick.ClickedObject != null && mouseClick.ClickedObject.CompareTag("Cell"))
            {
                StartCoroutine("Move", mouseClick);

                //Debug.Log(mouseClick.ClickedObject);
                Debug.Log("Cell");
                //MoveToCell();
            }

            if (mouseClick.Button == MouseClick.ButtonName.LEFT && mouseClick.Phase == MouseClick.PhaseName.CANCELED && mouseClick.ClickedObject == this.gameObject)
            {
                SetCharacterSelected();
            }

            if (mouseClick.Button == MouseClick.ButtonName.RIGHT && mouseClick.Phase == MouseClick.PhaseName.CANCELED)
            {
                SetCharacterDeselected();
            }
        }

        IEnumerator Move(MouseClick mouseClick)
        {
            foreach (Cell cell in _path.Skip(1))
            {
                while (transform.position != cell.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, cell.transform.position, 0.2f);
                    yield return new WaitForFixedUpdate();
                }
                
                CharacterPositionOnGrid = cell;
            }
        }

        private void SetCharacterSelected()
        {
            PlayableCharacterSelected.Invoke(this.gameObject);
            Debug.Log("Selected");
        }

        private void SetCharacterDeselected()
        {
            Debug.Log("Deselected");
        }

        public void OnCursorEnteredCell(Cell CursorEnteredCell)
        {
            Debug.Log("enter " + CursorEnteredCell);

            int distanceToCell = _pathfinder.CalculateDistanceBetweenCells(CharacterPositionOnGrid, CursorEnteredCell);
            Debug.Log(distanceToCell);

            _path = _pathfinder.FindPath(CharacterPositionOnGrid, CursorEnteredCell);

            foreach (Cell cell in _path.Skip(1))
            {
                cell.EnableHighlight();
            }
        }

        public void OnCursorExitedCell(Cell CursorExitedCell)
        {
            foreach (Cell cell in _path.Skip(1))
            {
                cell.DisableHighlight();
            }

            Debug.Log("exit " + CursorExitedCell);
        }
    }
}