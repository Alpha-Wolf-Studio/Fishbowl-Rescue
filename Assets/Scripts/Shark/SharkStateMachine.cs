using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkStateMachine : State_Machine
{
    [SerializeField] private SharkInputController sharkController;
    private SharkMoveState _moveState;
    private SharkPatrolState _patrolState;
    private SharkAttackState _attackState;

    private void OnEnable()
    {
        if (sharkController == null)
            sharkController = GetComponent<SharkInputController>();
        if (!sharkController)
        {
            Debug.LogError(message: $"{name}: (logError){nameof(sharkController)} is null");
            enabled = false;
        }

        sharkController.OnSharkMove += OnSharkMove;
        sharkController.OnSharkPatrol += OnSharkPatrol;
        sharkController.OnSharkAttack += OnSharkAttack;

        _moveState = new SharkMoveState(nameof(_moveState),this,sharkController);
        _patrolState = new SharkPatrolState(nameof(_patrolState),this,sharkController);
        _attackState = new SharkAttackState(nameof(_attackState),this,sharkController);
        
        base.OnEnable();
    }
    
    private void OnSharkMove()
    {
        SetState(_moveState);
    }
    private void OnSharkPatrol()
    {
        SetState(_patrolState);
    }
    private void OnSharkAttack()
    {
        SetState(_attackState);
    }

    protected override State GetInitialState()
    {
        base.GetInitialState();
        return _patrolState;
    }
    
    private void OnDisable()
    {
        sharkController.OnSharkMove -= OnSharkMove;
        sharkController.OnSharkPatrol -= OnSharkPatrol;
        sharkController.OnSharkAttack -= OnSharkAttack;
    }
}
