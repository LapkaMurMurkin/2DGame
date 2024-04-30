using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSM
{
    public void InitializeState(IFSMState state);

    public void SwitchState<T>() where T : IFSMState;

    public void Update();
}
