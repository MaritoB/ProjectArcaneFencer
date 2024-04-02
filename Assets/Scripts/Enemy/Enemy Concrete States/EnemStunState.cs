using System;
using System.Collections;
using UnityEngine;

public class EnemStunState : EnemyState
{

    public EnemStunState(Enemy enemy, StateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);
        enemy.EnemyStunBaseInstance.DoAnimationTriggerEventLogic(aTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyStunBaseInstance.DoEnterLogic();
    }



    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyStunBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.EnemyStunBaseInstance.DoFrameUpdateLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.EnemyStunBaseInstance.DoPhysicsLogic();
    }

}
