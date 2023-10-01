using UnityEngine;
using UnityEngine.Events;

public class Fish : Interactable
{
    public override UnityEvent<Interactable> onInteract { get; set; } = new UnityEvent<Interactable>();
    [field: SerializeField] public override InteractType InteractType { get; set; }
    private BoxCollider boxCollider;
    private Rigidbody rigidbody;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }

    public override void OnInteract(InteractType interactType)
    {
        if (interactType != InteractType)
            return;
        GameManager.Instance.AddScore(1);
        onInteract?.Invoke(this);
        boxCollider.enabled = false;
        rigidbody.isKinematic = true;
        gameObject.SetActive(false);
    }

    public override void Activate(Transform parent)
    {
        gameObject.SetActive(true);
        transform.SetParent(parent);
        transform.position = parent.position;
        transform.up = parent.up;
        boxCollider.enabled = true;
        rigidbody.isKinematic = false;
    }
}