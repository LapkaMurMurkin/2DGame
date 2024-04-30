using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSMState
{
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
