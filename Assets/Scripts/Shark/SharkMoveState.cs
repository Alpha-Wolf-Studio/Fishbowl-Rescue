using System;
using UnityEngine;

public class SharkMoveState : SharkBaseState
{
    private float maxDistanceDelta = 5.0f;
    public event Action<Vector3> OnLerpToPlayer;
    public event Action OnReadyToAttack;
    public SharkMoveState(string name, State_Machine stateMachine,SharkInputController _sharkController) : base(name, stateMachine,_sharkController) { }
    public override void OnEnter()
    {
        OnLerpToPlayer.Invoke(_sharkController.GetPlayer().transform.position);
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        float distance = Vector3.Distance(_sharkController.transform.position,_sharkController.GetPlayer().transform.position);
        OnLerpToPlayer.Invoke(_sharkController.GetPlayer().transform.position);
        if (distance >= maxDistanceDelta)
        {
            _sharkController.transform.position =
                Vector3.MoveTowards(_sharkController.transform.position, _sharkController.GetPlayer().transform.position, maxDistanceDelta * Time.deltaTime * _sharkController.GetChaseSpeed());
        }
        else
        {
            OnReadyToAttack.Invoke();
        }

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
