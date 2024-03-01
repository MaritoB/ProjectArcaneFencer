using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{

    protected StateMachine stateMachine;
    public State(StateMachine aStateMachine)
    {
        this.stateMachine = aStateMachine;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType) { }
}
