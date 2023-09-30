using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMoveState : SharkBaseState
{
    public SharkMoveState(string name, State_Machine stateMachine,SharkInputController _sharkController) : base(name, stateMachine,_sharkController) { }
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    
    private void playAttackAnimation()
    {
        
    }

    public override void AddStateTransitions(string transitionName, State transitionState)
    {
        base.AddStateTransitions(transitionName, transitionState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
