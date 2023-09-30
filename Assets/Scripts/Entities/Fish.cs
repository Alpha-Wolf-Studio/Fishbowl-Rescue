using UnityEngine;
using UnityEngine.Events;

public class Fish : IInteractable
{
    public override UnityEvent<IInteractable> onInteract { get; set; } = new UnityEvent<IInteractable>();
    [field: SerializeField] public override InteractType InteractType { get; set; }

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
    }
}