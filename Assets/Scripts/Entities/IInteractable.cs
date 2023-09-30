using UnityEngine;
using UnityEngine.Events;

public abstract class IInteractable : MonoBehaviour
{
    public abstract UnityEvent<IInteractable> onInteract { get; set; }
    public abstract InteractType InteractType { get; set; }
    public abstract void OnInteract (InteractType interactType);
    public abstract void Activate (Transform parent);
}

[System.Serializable]
public enum InteractType
{
    Hit,
    Pick,
    Push
}