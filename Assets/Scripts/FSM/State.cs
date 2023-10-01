using System.Collections.Generic;

public class State
{
    public string name;
    protected State_Machine stateMachine;
    protected Dictionary<string, State> transitions = new();

    public State (string name, State_Machine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void OnEnter () { }

    public virtual void UpdateLogic () { }

    public virtual void UpdatePhysics () { }

    public virtual void OnExit () { }

    public virtual void AddStateTransitions (string transitionName, State transitionState)
    {
        transitions.Add(transitionName, transitionState);
    }
}