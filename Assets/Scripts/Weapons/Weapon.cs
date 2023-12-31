using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public PlayerWeaponHitterStats playerHitterStats;
    [SerializeField] protected List<InteractType> interactType = new List<InteractType>();
    [SerializeField] protected GameObject hook; 
    [SerializeField] protected GameObject rope;
    [SerializeField] protected Transform hookPos;
    protected AudioSource audioSource;
    [SerializeField] protected AudioClip clipImpact;
    [SerializeField] protected AudioClip clipShoot;
    [SerializeField] protected AudioClip clipReloaded;

    public UnityEvent<bool> IsWeaponActive { get; } = new UnityEvent<bool>();
    private Coroutine moveToTarget;
    public List<InteractType> InteractType => interactType;

    public void Awake()
    {
        rope.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot(RaycastHit hit, Vector3 finalPos)
    {
        audioSource.clip = clipShoot;
        audioSource.Play();

        if (moveToTarget != null)
        {
            StopCoroutine(nameof(MoveUpToTarget));
            //Debug.Log("Already Shooting");
        }
        else
        {
            //Debug.Log("Start");
            IsWeaponActive.Invoke(true);
            if (hit.collider)
            {
                moveToTarget = StartCoroutine(MoveUpToTarget(finalPos, hit.collider));
            }
            else
            {
                //Debug.Log(finalPos);
                moveToTarget = StartCoroutine(MoveUpToTarget(finalPos));
            }
        }
    }

    public virtual void StartAction(Collider collider)
    {
        audioSource.clip = clipImpact;
        audioSource.Play();

        if (collider && collider.TryGetComponent<Interactable>(out Interactable interact) &&
            interact.InteractType == global::InteractType.Push)
        {
            interact.OnInteract(global::InteractType.Push);
        }
    }

    public virtual void EndAction (Collider collider1)
    {
        hook.transform.localPosition = Vector3.zero;
        hook.transform.localRotation = Quaternion.identity;
        rope.SetActive(false);
        audioSource.clip = clipReloaded;
        audioSource.Play();
    }

    IEnumerator MoveUpToTarget(Vector3 hitPoint, Collider collider = null)
    {
        
        Vector3 initialPos = hook.transform.position;
        float maxDuration = playerHitterStats.timeUntilTarget;

        rope.SetActive(true);
        rope.transform.LookAt(hitPoint);
        float initialScaleZ = rope.transform.localScale.z;

        float timer = 0;
        while (timer < maxDuration)
        {
            float t = timer / maxDuration;

            float dist = Vector3.Distance(initialPos, hitPoint);
            float maxScaleZ = dist;
            float lerpScale = Mathf.Lerp(initialScaleZ, maxScaleZ, t);
            rope.transform.localScale = new Vector3(1, 1, lerpScale);

            Vector3 currentPos = Vector3.Lerp(initialPos, hitPoint, t);
            hook.transform.position = currentPos;


            rope.transform.LookAt(hook.transform.position);

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
            rope.transform.LookAt(hook.transform.position);
            float dist = Vector3.Distance(rope.transform.position, hook.transform.position);
            rope.transform.localScale = new Vector3(1, 1, dist);
            timer += Time.deltaTime;
            yield return null;
        }

        Vector3 initialPos = hook.transform.position;

        float initialScaleZ = rope.transform.localScale.z;
        float maxScaleZ = 1;

        timer = 0;
        float maxDuration = playerHitterStats.timeUntilComeback;
        while (timer < maxDuration)
        {
            rope.transform.LookAt(hook.transform.position);
            Vector3 finalPos = hookPos.position;
            float t = timer / maxDuration;
            float lerpScale = Mathf.Lerp(initialScaleZ, maxScaleZ, t);
            Vector3 currentPos = Vector3.Lerp(initialPos, finalPos, t);
            hook.transform.position = currentPos;
            rope.transform.localScale = new Vector3(1, 1, lerpScale);
            timer += Time.deltaTime;
            yield return null;
        }

        hook.transform.SetParent(hookPos);
        hook.transform.localPosition = Vector3.zero;
        hook.transform.localRotation = Quaternion.identity;
        moveToTarget = null;
        IsWeaponActive.Invoke(false);
        EndAction(collider);
    }
}