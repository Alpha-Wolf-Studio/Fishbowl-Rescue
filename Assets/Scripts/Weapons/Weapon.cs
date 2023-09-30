using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public PlayerWeaponHitterStats playerHitterStats;
    [SerializeField] protected List<InteractType> interactType = new List<InteractType>();

    [FormerlySerializedAs("glove")] [SerializeField]
    protected GameObject hook;
    [SerializeField] protected Transform hookPos;
    public UnityEvent<bool> IsWeaponActive { get; } = new UnityEvent<bool>();
    private Coroutine moveToTarget;
    public List<InteractType> InteractType => interactType;

    public UnityEvent<float> AddScore { get; } = new UnityEvent<float>();

    public void Awake()
    {
    }

    public void Shoot(RaycastHit hit, Vector3 finalPos)
    {
        if (moveToTarget != null)
        {
            StopCoroutine(nameof(MoveUpToTarget));
            Debug.Log("Already Shooting");
        }
        else
        {
            Debug.Log("Start");
            IsWeaponActive.Invoke(true);
            if (hit.collider)
            {
                moveToTarget = StartCoroutine(MoveUpToTarget(finalPos, collider: hit.collider));
            }
            else
            {
                moveToTarget = StartCoroutine(MoveUpToTarget(finalPos));
            }
        }
    }

    public abstract void StartAction(Collider collider);
    public abstract void EndAction(Collider collider1);

    IEnumerator MoveUpToTarget(Vector3 hitPoint, Collider collider = null)
    {
        Vector3 initialPos = hook.transform.position;
        float maxDuration = playerHitterStats.timeUntilTarget;
        float timer = 0;
        while (timer < maxDuration)
        {
            float t = timer / maxDuration;
            Vector3 currentPos = Vector3.Lerp(initialPos, hitPoint, t);
            hook.transform.position = currentPos;
            timer += Time.deltaTime;
            yield return null;
        }

        StartAction(collider);
        hook.transform.SetParent(null);
        StartCoroutine(ComeBack(collider));
        yield break;
    }

    IEnumerator ComeBack(Collider collider = null)
    {
        float timer = 0;
        while (timer < playerHitterStats.waitTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Vector3 initialPos = hook.transform.position;
        timer = 0;
        float maxDuration = playerHitterStats.timeUntilComeback;
        while (timer < maxDuration)
        {
            Vector3 finalPos = hookPos.position;
            float t = timer / maxDuration;
            Vector3 currentPos = Vector3.Lerp(initialPos, finalPos, t);
            hook.transform.position = currentPos;
            timer += Time.deltaTime;
            yield return null;
        }

        hook.transform.SetParent(hookPos);
        hook.transform.localPosition = Vector3.zero;
        moveToTarget = null;
        IsWeaponActive.Invoke(false);
        EndAction(collider);
    }

    
}