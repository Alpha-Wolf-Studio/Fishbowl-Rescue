public interface IInteractable
{
    public InteractType InteractType { get; set; }
    public void OnInteract (InteractType interactType);
}

[System.Serializable]
public enum InteractType
{
    Hit,
    Pick,
    Push
}