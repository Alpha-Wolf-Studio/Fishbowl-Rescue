using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour, IInteractable
{
    [field: SerializeField] public InteractType InteractType { get; set; }

    private void Start ()
    {

    }

    private void Update ()
    {

    }

    public void OnInteract (InteractType interactType)
    {
        if (interactType != InteractType)
            return;


    }
}