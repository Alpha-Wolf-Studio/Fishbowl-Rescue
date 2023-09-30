using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected List<InteractType> interactType = new List<InteractType>();

    public List<InteractType> InteractType => interactType;
}