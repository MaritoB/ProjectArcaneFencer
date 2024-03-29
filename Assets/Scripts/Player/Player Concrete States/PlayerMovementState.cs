using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    public PlayerMovementState( StateMachine playerStateMachine, PlayerController aPlayerController) : base(playerStateMachine, aPlayerController)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);
        //player.PlayerRunBaseInstance.DoAnimationTriggerEventLogic(aTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        player.mPlayerRunInstance.DoEnterLogic();
    }


    public override void ExitState()
    {
        base.ExitState();
        player.mPlayerRunInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.mPlayerRunInstance.DoFrameUpdateLogic();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.mPlayerRunInstance.DoPhysicsLogic();
    }

}
