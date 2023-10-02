using System;
using UnityEngine;
using UnityEngine.Events;

public class Shark : Interactable
{
    public override UnityEvent<Interactable> onInteract { get; set; } = new UnityEvent<Interactable>();
    [field: SerializeField] public override InteractType InteractType { get; set; }
    public SharkController SharkController;

    private void Awake ()
    {
        SharkController = GetComponent<SharkController>();
    }

    private void Start () { }

    private void Update () { }

    public override void OnInteract (InteractType interactType)
    {
        if (interactType != InteractType)
            return;

        onInteract?.Invoke(this);
    }

    public override void Activate (Transform parent)
    {
        gameObject.SetActive(true);
        transform.SetParent(parent);
        transform.position = parent.position;
        transform.up = parent.up;
        transform.forward = parent.forward;
    }
}