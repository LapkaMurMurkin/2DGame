using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSMState : FSMState
{
    protected readonly Character _character;
    protected new readonly CharacterFSMData _data;
    public CharacterFSMState(CharacterFSM fsm, CharacterFSMData data) : base(fsm, data)
    {
        _character = fsm.Character;
        _data = data;
    }
}

