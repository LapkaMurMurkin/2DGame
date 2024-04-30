using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCharacterInputState : CharacterFSMState
{
    private readonly HexGridVisualizer _hexGridVisualizer;
    public PlayableCharacterInputState(CharacterFSM fsm, CharacterFSMData data) : base(fsm, data)
    {
        _hexGridVisualizer = _character.PositionOnGrid.HexGrid.Visualizer;
    }

    public override void Enter()
    {
        Cell.CursorEntered += ShowMovementPath;
        Cell.CursorExited += HideMovementPath;
        Cell.Clicked += Move;
        PlayableCharacter.Clicked += SelectPlayableCharacter;
        Enemy.Clicked += Attack;


        CharacterPanel.Instance.BindPanelToCharacter(_character);

        _data.MovementRadius = Pathfinder.FindMovementRadius(_character.PositionOnGrid, _character.RemainingMovementPoints.Value);
        _hexGridVisualizer.ShowMovementRadius(_data.MovementRadius);
    }
    public override void Exit()
    {
        Cell.CursorEntered -= ShowMovementPath;
        Cell.CursorExited -= HideMovementPath;
        Cell.Clicked -= Move;
        PlayableCharacter.Clicked -= SelectPlayableCharacter;
        Enemy.Clicked -= Attack;

        _hexGridVisualizer.HideMovementRadius();
        _hexGridVisualizer.HideMovementPath();
        _hexGridVisualizer.HideCursorPosition();
    }

    private void ShowMovementPath(Cell cell)
    {
        _hexGridVisualizer.ShowCursorPosition(cell);

        if (_hexGridVisualizer.isMovementPathFreezed is false)
        {
            List<Cell> path = Pathfinder.FindMovementPath(_character.PositionOnGrid, cell);
            _hexGridVisualizer.ShowMovementPath(path, _character.RemainingMovementPoints.Value);
        }
    }

    private void HideMovementPath(Cell cell)
    {
        if (_hexGridVisualizer.isMovementPathFreezed is false)
            _hexGridVisualizer.HideMovementPath();

        _hexGridVisualizer.HideCursorPosition();
    }

    private void Move(Cell cell, PointerEventData clickData)
    {
        bool isLeftButton = clickData.button is PointerEventData.InputButton.Left;
        bool isMiddleButton = clickData.button is PointerEventData.InputButton.Middle;
        bool isRightButton = clickData.button is PointerEventData.InputButton.Right;

        bool isDoubleClick = MouseClick.CheckDoubleClick(clickData);

        if (isLeftButton)
        {
            _data.MovementPath = Pathfinder.FindMovementPath(_character.PositionOnGrid, cell);
            _hexGridVisualizer.ShowMovementPath(_data.MovementPath, _character.RemainingMovementPoints.Value);
        }

        if (isLeftButton && isDoubleClick)
        {
            _fsm.SwitchState<CharacterMovementState>();
        }

        if (isRightButton)
        {
            _hexGridVisualizer.HideMovementPath();
        }
    }

    private void SelectPlayableCharacter(PlayableCharacter clickedPlayableCharacter)
    {
        if (clickedPlayableCharacter == _character)
            return;

        _fsm.SwitchState<CharacterIdleState>();
        clickedPlayableCharacter.FSM.SwitchState<PlayableCharacterInputState>();
    }

    private void Attack(Enemy enemy, PointerEventData clickData)
    {
        int distance = Pathfinder.CalculateDistanceBetweenCells(_character.PositionOnGrid, enemy.PositionOnGrid);

        if (_character.RemainingActionPoints.Value > 0 && distance == 1)
        {
            _character.RemainingActionPoints.Value--;
            Debug.Log("Attack");
        }
    }
}
