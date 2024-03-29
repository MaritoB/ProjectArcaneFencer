using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState( StateMachine playerStateMachine, PlayerController aPlayerController) : base(playerStateMachine, aPlayerController)
    {
    }

    /*
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);
        //player.PlayerRunBaseInstance.DoAnimationTriggerEventLogic(aTriggerType);
    }
     */

    public override void EnterState()
    {
        base.EnterState();
        player.mPlayerAttackBaseInstance.DoEnterLogic();
    }


    public override void ExitState()
    {
        base.ExitState();
        player.mPlayerAttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.mPlayerAttackBaseInstance.DoFrameUpdateLogic();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.mPlayerAttackBaseInstance.DoPhysicsLogic();
    }

}
