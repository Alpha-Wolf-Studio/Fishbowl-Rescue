using System.Collections;
using UnityEngine;

public class SharkStateRunAway : SharkState
{
    private SharkStats stats => SharkController.SharkStats;
    private Vector3 currentTarget;
    private IEnumerator lookAtPoint;

    public override void OnEnterState ()
    {
        Debug.Log("Enter Run");
        ChangeAnimStateTo(AnimState.Run);
        Vector3 direction = transform.position - SharkController.targetPosition;
        currentTarget = direction * stats.distanceRun;
        SharkController.targetPoint.position = currentTarget;
    }

    public override void OnExitState ()
    {
        if (lookAtPoint != null)
            StopCoroutine(lookAtPoint);
    }

    public override void OnStartState () { }

    public override void OnUpdateState ()
    {
        float distance = Vector3.Distance(transform.position, currentTarget);

        Vector3 direccionAlObjetivo = (currentTarget - transform.position).normalized;
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionAlObjetivo);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * stats.speedRotation);

        if (distance >= stats.minDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * stats.speedRunMovement);
        }
        else
        {
            SharkController.ChangeStateToPatrol();
        }
    }

    public override void OnFixedUpdateState () { }
}