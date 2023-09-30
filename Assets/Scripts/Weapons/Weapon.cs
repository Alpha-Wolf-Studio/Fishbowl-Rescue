using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public PlayerWeaponHitterStats playerHitterStats;
    [SerializeField] protected List<InteractType> interactType = new List<InteractType>();

    [SerializeField] private GameObject glove;
    [SerializeField] private Transform hookPos;

    private Coroutine moveToTarget;
    public List<InteractType> InteractType => interactType;

    public void Shoot(Vector3 hitPoint)
    {
        if (moveToTarget != null)
        {
            StopCoroutine(nameof(MoveUpToTarget));
            Debug.Log("Stop");
        }
        else
        {
            Debug.Log("Start");
            moveToTarget = StartCoroutine(MoveUpToTarget(hitPoint));
        }
    }

    public abstract void DoStuff();

    IEnumerator MoveUpToTarget(Vector3 hitPoint)
    {
        Vector3 initialPos = glove.transform.position;
        float maxDuration = playerHitterStats.timeUntilTarget;
        float timer = 0;
        while (timer < maxDuration)
        {
            float t = timer / maxDuration;
            Vector3 currentPos = Vector3.Lerp(initialPos, hitPoint, t);
            glove.transform.position = currentPos;
            timer += Time.deltaTime;
            yield return null;
        }

        DoStuff();
        glove.transform.SetParent(null);
        Invoke(nameof(ActivateComeback), playerHitterStats.waitTime);
        yield break;
    }

    IEnumerator ComeBack()
    {
        Vector3 initialPos = glove.transform.position;
        float timer = 0;
        float maxDuration = playerHitterStats.timeUntilComeback;
        while (timer < maxDuration)
        {
            Vector3 finalPos = hookPos.position;
            float t = timer / maxDuration;
            Vector3 currentPos = Vector3.Lerp(initialPos, finalPos, t);
            glove.transform.position = currentPos;
            timer += Time.deltaTime;
            yield return null;
        }

        glove.transform.SetParent(hookPos);
        glove.transform.localPosition = Vector3.zero;
        moveToTarget = null;
    }

    private void ActivateComeback()
    {
        StartCoroutine(ComeBack());
    }
}