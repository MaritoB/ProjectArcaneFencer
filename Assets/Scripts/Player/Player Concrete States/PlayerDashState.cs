using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState( StateMachine playerStateMachine, PlayerController aPlayerController) : base(playerStateMachine, aPlayerController)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        player.mPlayerDashInstance.DoEnterLogic();
    }


    public override void ExitState()
    {
        base.ExitState();
        player.mPlayerDashInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.mPlayerDashInstance.DoFrameUpdateLogic();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.mPlayerDashInstance.DoPhysicsLogic();
    }

}
