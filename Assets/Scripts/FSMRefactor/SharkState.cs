using UnityEngine;

public abstract class SharkState : MonoBehaviour
{
    [HideInInspector] public SharkController SharkController;
    private Animator Animator;
    private static readonly int State = Animator.StringToHash("State");

    public enum AnimState { Patrol, Run, Attack, Damage }

    public void OnAwakeState (SharkController sharkController, Animator animator)
    {
        SharkController = sharkController;
        Animator = animator;
    }

    protected void ChangeAnimStateTo (AnimState state) => Animator.SetInteger(State, (int)state);
    public abstract void OnStartState ();
    public abstract void OnEnterState ();
    public abstract void OnExitState ();
    public abstract void OnUpdateState ();
    public abstract void OnFixedUpdateState ();
}