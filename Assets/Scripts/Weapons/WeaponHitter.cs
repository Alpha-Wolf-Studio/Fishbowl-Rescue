using UnityEngine;

public class WeaponHitter : Weapon
{
    public override void StartAction (Collider collider)
    {
        base.StartAction(collider);
        if (collider && collider.TryGetComponent<SharkController>(out SharkController shark))
            shark.OnReceiveAttack();
    }

    public override void EndAction (Collider collider1)
    {
        base.EndAction(collider1);
    }
}