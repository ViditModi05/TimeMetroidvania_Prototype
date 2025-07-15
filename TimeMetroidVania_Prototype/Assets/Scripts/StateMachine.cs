using UnityEngine;

public class StateMachine 
{
    [Header("Current State")]
    public EntityState currentState {  get; private set; }

    public void Initialize(EntityState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EntityState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }
}
