using System.Collections;
using UnityEngine;

public class SharkStateFollow : SharkState
{
    private IEnumerator lookAtPoint;
    private SharkStats stats => SharkController.SharkStats;

    private bool isAttacking = false;

    public override void OnEnterState ()
    {
        Debug.Log("Enter To Follow");
        isAttacking = false;
        ChangeAnimStateTo(AnimState.Run);
        //StartRotationToPoint();
    }

    public override void OnExitState ()
    {
        if (lookAtPoint != null)
            StopCoroutine(lookAtPoint);
        isAttacking = false;
    }

    public override void OnStartState () { }

    public override void OnUpdateState ()
    {
        float distance = Vector3.Distance(transform.position, SharkController.targetPosition);

        Vector3 direccionAlObjetivo = (SharkController.targetPosition - transform.position).normalized;
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionAlObjetivo);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * stats.speedRotation);

        if (distance >= stats.minDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, SharkController.targetPosition, Time.deltaTime * stats.speedFollowMovement);
            if (!IsPlayerInVision() && !isAttacking)
            {
                SharkController.ChangeStateToPatrol();
            }
        }
        else if (!isAttacking)
        {
            StartCoroutine(Attacking());
        }
    }

    private bool IsPlayerInVision()
    {
        float distance = Vector3.Distance(SharkController.targetPosition, transform.position);
        if (distance < stats.maxDistVision)
        {
            if (!Physics.Raycast(transform.position, transform.forward, distance, stats.layerMaskHidePlayer))
            {
                return true;
            }
        }
        
        return false;
    }

    public override void OnFixedUpdateState () { }
    
    private IEnumerator Attacking ()
    {
        Debug.Log("Attacking");
        isAttacking = true;
        ChangeAnimStateTo(AnimState.Attack);
        yield return new WaitForSeconds(0.5f);
        SharkController.target.ReceiveDamage(stats.damage);
        yield return new WaitForSeconds(0.5f);
        SharkController.ChangeStateToRunAway();
    }

    //private void StartRotationToPoint ()
    //{
    //    if (lookAtPoint != null)
    //        StopCoroutine(lookAtPoint);
    //    lookAtPoint = LookAtPoint();
    //    StartCoroutine(LookAtPoint());
    //}
    //
    //private IEnumerator LookAtPoint ()
    //{
    //    Quaternion initialRot = transform.rotation;
    //    Quaternion finalRot = Quaternion.LookRotation(SharkController.targetPosition - transform.position, Vector3.up);
    //    float elapsedTime = 0f;
    //
    //    while (elapsedTime < stats.speedRotation)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        transform.rotation = Quaternion.Lerp(initialRot, finalRot, elapsedTime);
    //        yield return null;
    //    }
    //}
}