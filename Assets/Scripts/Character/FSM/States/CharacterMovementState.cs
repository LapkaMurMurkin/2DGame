using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovementState : CharacterFSMState
{
    private Cell _nextCell;
    
    public CharacterMovementState(CharacterFSM fsm, CharacterFSMData data) : base(fsm, data) { }

    public override void Enter()
    {
        if (GetNextCell() is null || _character.RemainingMovementPoints.Value is 0)
            _fsm.SwitchState<PlayableCharacterInputState>();
    }

    public override void Update()
    {
        Vector3 currentCharacterPosition = _character.transform.position;
        Vector3 cellPosition = _nextCell.transform.position;

        if (currentCharacterPosition != cellPosition)
        {
            currentCharacterPosition = Vector3.MoveTowards(currentCharacterPosition, cellPosition, Time.deltaTime * Constants.VISUAL_MOVEMENT_SPEED);
            _character.transform.position = currentCharacterPosition;
            _character.transform.LookAt(cellPosition);
        }
        else
        {
            _character.RemainingMovementPoints.Value -= _nextCell.PathDifficulty;
            _character.PositionOnGrid = _nextCell;

            _data.MovementPath.Remove(_nextCell);

            if (GetNextCell() is null || _character.RemainingMovementPoints.Value is 0)
                _fsm.SwitchState<PlayableCharacterInputState>();
        }
    }

    private Cell GetNextCell()
    {
        if (_data.MovementPath.Count is 0)
            return null;
        else
            return _nextCell = _data.MovementPath.First<Cell>();
    }
}
