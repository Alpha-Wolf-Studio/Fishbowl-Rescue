using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkInputController : MonoBehaviour
{
    [SerializeField] private SharkStats _stats;
    [SerializeField] private GameObject _target;
    public Action OnSharkMove;
    public Action OnSharkPatrol;
    public Action OnSharkAttack;

    public float GetPatrolSpeed() { return _stats.patrolSpeed; }
    public float GetChaseSpeed() { return _stats.chaseSpeed; }
    public float GetRotationSpeed() { return _stats.rotationSpeed; }
    public float GetDetectionAngle() { return _stats.detectionAngle; }
    public float GetStopDistance() { return _stats.stopDistance; }
    public GameObject GetPlayer() { return _target; }

    public void Update()
    {
        DebugDrawDetectionArc();
        if (IsAtDetectionAngle()) 
            OnSharkMove.Invoke();
        else
            OnSharkPatrol.Invoke();
    }
    
    bool IsAtDetectionAngle()
    {
        Vector3 directionToPlayer = _target.transform.position - transform.position;
        float angulo = Vector3.Angle(_target.transform.forward, directionToPlayer);
        
        if (angulo < _stats.detectionAngle * 0.5) { return true; }
        return false;
    }
    
    void DebugDrawDetectionArc()
    {
        float detectionRadius = 20.0f;
        float halfAngle = _stats.detectionAngle * 0.5f;
        Quaternion leftRayRotation = Quaternion.Euler(0, -halfAngle, 0);
        Quaternion rightRayRotation = Quaternion.Euler(0, halfAngle, 0);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;

        // Dibuja las líneas de detección.
        Debug.DrawRay(transform.position, leftRayDirection * detectionRadius, Color.red);
        Debug.DrawRay(transform.position, rightRayDirection * detectionRadius, Color.red);
    }
}
