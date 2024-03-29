using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
{
    public PlayerBlockState( StateMachine playerStateMachine, PlayerController aPlayerController) : base(playerStateMachine, aPlayerController)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        player.mPlayerBlockInstance.DoEnterLogic();
    }


    public override void ExitState()
    {
        base.ExitState();
        player.mPlayerBlockInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        player.mPlayerBlockInstance.DoFrameUpdateLogic();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.mPlayerBlockInstance.DoPhysicsLogic();
    }

}
