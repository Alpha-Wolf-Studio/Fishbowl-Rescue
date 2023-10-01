using System;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public event Action onPlayerFound;
    [SerializeField] private Animator animator;
    private List<SharkState> stats = new List<SharkState>();
    private SharkStateRunAway sharkStateRunAway;
    private SharkStatePatrol sharkStatePatrol;
    private SharkStateFollow sharkStateFollow;
    public PlayerController target;
    private SharkState currentState;
    public Vector3 targetPosition => target.transform.position;
    public Transform targetPoint;
    public SharkStats SharkStats;

    private void Awake ()
    {
        sharkStateRunAway = GetComponent<SharkStateRunAway>();
        sharkStatePatrol = GetComponent<SharkStatePatrol>();
        sharkStateFollow = GetComponent<SharkStateFollow>();

        stats.Add(sharkStateRunAway);
        stats.Add(sharkStatePatrol);
        stats.Add(sharkStateFollow);

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
        currentState.OnExitState();
        currentState = sharkStatePatrol;
        currentState.OnEnterState();

        onPlayerFound?.Invoke();
    }

    public void ChangeStateToFollow ()
    {
        currentState.OnExitState();
        currentState = sharkStateFollow;
        currentState.OnEnterState();

        onPlayerFound?.Invoke();
    }

    public void ChangeStateToRunAway ()
    {
        currentState.OnExitState();
        currentState = sharkStateRunAway;
        currentState.OnEnterState();

        onPlayerFound?.Invoke();
    }

    public void OnReceiveAttack ()
    {
        ChangeStateToRunAway();
        GameManager.Instance.AddHit();
    }
}