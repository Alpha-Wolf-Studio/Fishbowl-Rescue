using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkInputController : MonoBehaviour
{
    [SerializeField] private SharkStats _stats;

    public Action OnSharkMove;
    public Action OnSharkPatrol;
    public Action OnSharkAttack;

    public float GetPatrolSpeed() { return _stats.patrolSpeed; }
    public float GetChaseSpeed() { return _stats.chaseSpeed; }
    public float GetRotationSpeed() { return _stats.rotationSpeed; }
    
}
