using System;
using System.Collections.Generic;
using UnityEngine;
using static SharkState;

public class SharkController : MonoBehaviour
{
    public event Action onPlayerFound;
    [SerializeField] private Animator animator;
    private List<SharkState> stats = new List<SharkState>();
    private SharkStateRunAway sharkStateRunAway;
    private SharkStatePatrol sharkStatePatrol;
    private SharkStateFollow sharkStateFollow;
    private SharkStateDamaged sharkStateDamaged;
    public PlayerController target;
    private SharkState currentState;
    public Vector3 targetPosition => target.transform.position;
    public Transform targetPoint;
    public SharkStats SharkStats;
    public bool isFocus;

    private void Awake ()
    {
        sharkStateRunAway = GetComponent<SharkStateRunAway>();
        sharkStatePatrol = GetComponent<SharkStatePatrol>();
        sharkStateFollow = GetComponent<SharkStateFollow>();
        sharkStateDamaged = GetComponent<SharkStateDamaged>();

        stats.Add(sharkStateRunAway);
        stats.Add(sharkStatePatrol);
        stats.Add(sharkStateFollow);
        stats.Add(sharkStateDamaged);

        for (int i = 0; i < stats.Count; i++)
        {
            stats[i].OnAwakeState(this, animator);
        }

        currentState = sharkStatePatrol;
    }

    private void Start ()
    {
        for (int i = 0; i < stats.Count; i++)
            stats[i].OnStartState();
    }

    private void Update ()
    {
        currentState.OnUpdateState();
    }

    private void FixedUpdate ()
    {
        currentState.OnFixedUpdateState();
    }

    public void ChangeStateToPatrol ()
    {
        isFocus = false;
        currentState.OnExitState();
        currentState = sharkStatePatrol;
        currentState.OnEnterState();

        onPlayerFound?.Invoke();
    }

    public void ChangeStateToFollow ()
    {
        isFocus = true;
        currentState.OnExitState();
        currentState = sharkStateFollow;
        currentState.OnEnterState();

        onPlayerFound?.Invoke();
    }

    public void ChangeStateToRunAway ()
    {
        isFocus = false;
        currentState.OnExitState();
        currentState = sharkStateRunAway;
        currentState.OnEnterState();

        onPlayerFound?.Invoke();
    }

    public void ChangeStateToDamaged ()
    {
        isFocus = false;
        currentState.OnExitState();
        currentState = sharkStateDamaged;
        currentState.OnEnterState();

        onPlayerFound?.Invoke();
    }

    public void OnReceiveAttack ()
    {
        ChangeStateToDamaged();
        GameManager.Instance.AddHit();
    }
}