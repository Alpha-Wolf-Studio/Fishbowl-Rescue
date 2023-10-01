using System;
using UnityEngine;

public class WeaponPicker : Weapon
{
    private void Start()
    {
    }

    private void Update()
    {
    }

    public override void StartAction(Collider collider)
    {
        base.StartAction(collider);
        if (collider && collider.TryGetComponent<Interactable>(out Interactable interactable) &&
            interactable.InteractType == global::InteractType.Pick)
        {
            collider.transform.SetParent(hook.transform);
            collider.transform.localPosition = Vector3.zero;
        }
        
    }

    public override void EndAction(Collider collider1)
    {
        if (collider1 &&collider1.TryGetComponent<Interactable>(out Interactable interactable))
        {
            interactable.OnInteract(global::InteractType.Pick);
            collider1.transform.SetParent(null);
        }
    }
}