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
        player.PlayerAttackBaseInstance.DoEnterLogic();
    }


    public override void ExitState()
    {
        base.ExitState();
        player.PlayerAttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.PlayerAttackBaseInstance.DoFrameUpdateLogic();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.PlayerAttackBaseInstance.DoPhysicsLogic();
    }

}
