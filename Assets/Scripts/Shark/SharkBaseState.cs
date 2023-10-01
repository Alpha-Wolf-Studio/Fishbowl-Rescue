using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBaseState : State
{
    public SharkInputController _sharkController;
    public SharkBaseState(string name, State_Machine stateMachine,SharkInputController _sharkController) : base(name, stateMachine)
    {
        this._sharkController = _sharkController;
    }
}