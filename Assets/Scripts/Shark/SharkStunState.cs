using System;
using Unity.VisualScripting;
using UnityEngine;

public class SharkStunState : SharkBaseState
{
    private float stunTimer;
    public Action OnSharkMoved;

    public SharkStunState(string name, State_Machine stateMachine, SharkInputController _sharkController) : base(name,
        stateMachine, _sharkController)
    {
    }

    public override void OnEnter()
    {
        stunTimer = 0;
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

    private void PlayMoveAnimation()
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