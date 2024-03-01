using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get;  set; }
    public void Initialize (State startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void ChangeState(State aNewEnemyState)
    {
        CurrentState.ExitState();
        CurrentState = aNewEnemyState;
        CurrentState.EnterState();
    }
}
