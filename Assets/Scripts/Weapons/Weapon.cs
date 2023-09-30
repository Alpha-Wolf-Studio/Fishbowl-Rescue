using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected List<InteractType> interactType = new List<InteractType>();

    public List<InteractType> InteractType => interactType;

    public abstract void Shoot (Vector3 hitPoint);
}