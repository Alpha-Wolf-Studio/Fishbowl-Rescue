using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SharkStateMachine : State_Machine
{
    [FormerlySerializedAs("sharkController")] [SerializeField] private SharkInputController _sharkController;
    private IEnumerator _lookAtTarget;
    private SharkMoveState _moveState;
    private SharkPatrolState _patrolState;
    private SharkAttackState _attackState;

    private void OnEnable()
    {
        if (_sharkController == null)
            _sharkController = GetComponent<SharkInputController>();
        if (!_sharkController)
        {
            Debug.LogError(message: $"{name}: (logError){nameof(_sharkController)} is null");
            enabled = false;
        }
        
        _moveState = new SharkMoveState(nameof(_moveState),this,_sharkController);
        _patrolState = new SharkPatrolState(nameof(_patrolState),this,_sharkController);
        _attackState = new SharkAttackState(nameof(_attackState),this,_sharkController);

        _sharkController.OnSharkMove += OnSharkMove;
        _sharkController.OnSharkPatrol += OnSharkPatrol;
        _sharkController.OnSharkAttack += OnSharkAttack;
        _patrolState.OnLerpToTarget += OnPatrolLerpToTarget;
        _moveState.OnLerpToPlayer += OnPatrolLerpToTarget;
        _moveState.OnReadyToAttack += OnSharkAttack;
        _attackState.OnPlayerAttacked += OnSharkPatrol;

        
        base.OnEnable();
    }
    
    private void OnPatrolLerpToTarget(Vector3 obj)
    {
        if (_lookAtTarget != null)
        {
            StopCoroutine(_lookAtTarget);
        }

        _lookAtTarget = LookAtTarget(obj);
        StartCoroutine(_lookAtTarget);
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
    
    IEnumerator LookAtTarget(Vector3 currentTarget)
    {
        Quaternion initialRot = _sharkController.transform.rotation;
        Quaternion finalRot = Quaternion.LookRotation(currentTarget - transform.position , Vector3.up);
        float elapsedTime = 0f;

        while (elapsedTime < _sharkController.GetRotationSpeed())
        {
            elapsedTime += Time.deltaTime;
            _sharkController.transform.rotation = Quaternion.Lerp(initialRot,finalRot,elapsedTime);
            yield return null;
        }
    }

    protected override State GetInitialState()
    {
        base.GetInitialState();
        return _patrolState;
    }
    
    private void OnDisable()
    {
        _sharkController.OnSharkMove -= OnSharkMove;
        _sharkController.OnSharkPatrol -= OnSharkPatrol;
        _sharkController.OnSharkAttack -= OnSharkAttack;
        _patrolState.OnLerpToTarget -= OnPatrolLerpToTarget;
        _moveState.OnLerpToPlayer -= OnPatrolLerpToTarget;
        _moveState.OnReadyToAttack -= OnSharkAttack;
    }
}
