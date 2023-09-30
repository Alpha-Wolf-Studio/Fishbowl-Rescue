using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SharkAttackState : SharkBaseState
{
    private float sphereRadius = 40.0f;
    private float maxDistanceDelta = 1.0f;
    private Vector3 randomPointInSphere;
    private Vector3 currentTarget;
    public event Action OnPlayerAttacked;
    public SharkAttackState(string name, State_Machine stateMachine,SharkInputController _sharkController) : base(name, stateMachine,_sharkController) { }
    public override void OnEnter()
    {
        _sharkController.GetPlayer().MoveByAttack(_sharkController.transform.forward,_sharkController.GetSharkForce());
        currentTarget = GetRandomPoint();
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        float distance = Vector3.Distance(_sharkController.transform.position,currentTarget);

        if (distance >= maxDistanceDelta)
        {
            _sharkController.transform.position =
                Vector3.MoveTowards(_sharkController.transform.position, currentTarget, maxDistanceDelta * Time.deltaTime * _sharkController.GetPatrolSpeed() * 2);
        }
        else
        {
            OnPlayerAttacked.Invoke();
        }
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    
    private void PlayAttackAnimation()
    {
        
    }
    
    private Vector3 GetRandomPoint()
    {
        randomPointInSphere = Random.insideUnitSphere;
        randomPointInSphere *= sphereRadius;
        return randomPointInSphere;
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
