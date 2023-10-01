using UnityEngine;
using UnityEngine.Events;

public class WaterTap : Interactable
{
    public override UnityEvent<Interactable> onInteract { get; set; } = new UnityEvent<Interactable>();
    [field: SerializeField] public override InteractType InteractType { get; set; }

    private void Start () { }

    private void Update () { }

    public override void OnInteract (InteractType interactType)
    {
        if (interactType != InteractType)
            return;

        WaterManager.Instance.AddWater(10);
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