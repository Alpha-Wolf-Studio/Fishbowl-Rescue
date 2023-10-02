using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SharkStatePatrol : SharkState
{
    private SharkStats stats => SharkController.SharkStats;
    private Vector3 currentTarget;
    private IEnumerator lookAtPoint;

    public override void OnStartState () { }
    public override void OnFixedUpdateState () { }

    public override void OnEnterState ()
    {
        Debug.Log("Enter To Patrol");
        ChangeAnimStateTo(AnimState.Patrol);
        SetRandomPoint();
        //StartRotationToPoint();
    }

    public override void OnExitState ()
    {
        if (lookAtPoint != null)
            StopCoroutine(lookAtPoint);
    }

    public override void OnUpdateState ()
    {
        float distance = Vector3.Distance(transform.position, currentTarget);

        Vector3 direccionAlObjetivo = (currentTarget - transform.position).normalized;
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionAlObjetivo);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * stats.speedRotation);

        if (distance >= stats.minDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * stats.speedPatrolMovement);
        }
        else
        {
            SetRandomPoint();
            //StartRotationToPoint();
        }

        if (currentTarget.y < WaterManager.Instance.WaterPosition.y)
        {
            if (currentTarget.y > WaterManager.Instance.MinWaterPosition.y)
            {
                SetRandomPoint();
            }
        }

        if (TryFindPlayer())
        {
            SharkController.ChangeStateToFollow();
        }
    }

    private bool TryFindPlayer ()
    {
        Vector3 directionToPlayer = SharkController.targetPosition - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        float distance = Vector3.Distance(SharkController.targetPosition, transform.position);

        if (angle < stats.detectionAngle * 0.5 && distance < stats.maxDistVision)
        {
            if (!Physics.Raycast(transform.position, transform.forward, distance, stats.layerMaskHidePlayer))
            {
                return true;
            }
        }

        return false;
    }

    private void SetRandomPoint ()
    {
        bool isAvailablePoint = false;
        int counter = 500;

        while (!isAvailablePoint)
        {
            counter--;
            if (counter < 0)
            {
                isAvailablePoint = true;
                Debug.LogWarning("Punto no encontrado, seteado uno random");
            }

            currentTarget = Random.insideUnitSphere * stats.sphereRadius;
            if (currentTarget.y < WaterManager.Instance.WaterPosition.y)
            {
                if (currentTarget.y > WaterManager.Instance.MinWaterPosition.y)
                {
                    float distance = Vector3.Distance(transform.position, currentTarget);
                    if (!Physics.Raycast(transform.position, transform.forward, distance, stats.layerMaskHidePlayer))
                    {
                        isAvailablePoint = true;
                    }
                }
            }
        }

        SharkController.targetPoint.position = currentTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.sphereRadius);

        Gizmos.color = Color.red;

        Vector3 origen = transform.position;
        Vector3 direccionActual = transform.forward * stats.maxDistVision;

        Quaternion rotacionIzquierda = Quaternion.AngleAxis(-stats.detectionAngle / 2f, transform.up);
        Quaternion rotacionDerecha = Quaternion.AngleAxis(stats.detectionAngle / 2f, transform.up);

        Vector3 puntoIzquierdo = transform.position + rotacionIzquierda * direccionActual;
        Vector3 puntoDerecho = transform.position + rotacionDerecha * direccionActual;

        Gizmos.DrawLine(origen, puntoIzquierdo);
        Gizmos.DrawLine(origen, puntoDerecho);
        Gizmos.DrawLine(puntoIzquierdo, puntoDerecho);
    }
#endif
}