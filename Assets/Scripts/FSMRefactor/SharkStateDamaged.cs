using UnityEngine;

public class SharkStateDamaged : SharkState
{
    [SerializeField] private float currentTime;
    private SharkStats stats => SharkController.SharkStats;

    public override void OnStartState () { }

    public override void OnEnterState ()
    {
        ChangeAnimStateTo(AnimState.Damage);
    }

    public override void OnExitState ()
    {
        currentTime = 0;
    }

    public override void OnUpdateState ()
    {
        currentTime += Time.deltaTime;
        if (currentTime > stats.stunTime)
            SharkController.ChangeStateToRunAway();
    }

    public override void OnFixedUpdateState () { }
}