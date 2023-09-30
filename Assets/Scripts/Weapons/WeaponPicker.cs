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
        if (collider)
        {
            Debug.Log(collider.name);
            collider.transform.SetParent(hook.transform);
            collider.transform.localPosition = Vector3.zero;
        }
    }

    public override void EndAction(Collider collider1)
    {
        if (collider1)
        {
            Destroy(collider1.gameObject);
            AddScore.Invoke(10);
        }
    }
}