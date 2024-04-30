using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSM : FSM
{
    public readonly Character Character;
    protected new readonly CharacterFSMData _data;

    public CharacterFSM(Character character)
    {
        Character = character;
        _data = new CharacterFSMData();
    }

    public void InitializeState<T>() where T : CharacterFSMState
    {
        Type stateType = typeof(T);
        T state = Activator.CreateInstance(stateType, this, _data) as T;
        base.InitializeState(state);
    }
}
