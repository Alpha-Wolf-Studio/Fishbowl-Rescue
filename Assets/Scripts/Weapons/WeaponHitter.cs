using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponHitter : Weapon
{
    

    private void Start()
    {
    }

    private void Update()
    {
    }



    public override void StartAction(Collider collider)
    {
        if (collider && collider.TryGetComponent<SharkStateMachine>(out SharkStateMachine shark))
        {
            Debug.Log(shark.currentState,gameObject);
            shark.GetSharkInput().Stun();
        }
    }

    public override void EndAction(Collider collider1)
    {
   
    }
}