using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FSM : IFSM
{
    public IFSMState CurrentState { get; private set; }
    private readonly Dictionary<Type, IFSMState> _states = new Dictionary<Type, IFSMState>();
    protected readonly IFSMData _data;

    virtual public void InitializeState(IFSMState state)
    {
        _states.Add(state.GetType(), state);
    }

    virtual public void SwitchState<T>() where T : IFSMState
    {
        Type type = typeof(T);

        if (CurrentState?.GetType() == type)
            return;

        if (_states.TryGetValue(type, out IFSMState newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }

    virtual public void Update()
    {
        CurrentState?.Update();
    }
}
