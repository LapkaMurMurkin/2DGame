using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState : IFSMState
{
    protected readonly IFSM _fsm;
    protected readonly IFSMData _data;

    public FSMState(IFSM fsm, IFSMData data)
    {
        _fsm = fsm;
        _data = data;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
