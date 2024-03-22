using System;
using System.Collections;
using UnityEngine;

public class EnemyKnockBackState : EnemyState
{

    public EnemyKnockBackState(Enemy enemy, StateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);
        enemy.EnemyKnockBackBaseInstance.DoAnimationTriggerEventLogic(aTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyKnockBackBaseInstance.DoEnterLogic();
    }



    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyKnockBackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.EnemyKnockBackBaseInstance.DoFrameUpdateLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.EnemyKnockBackBaseInstance.DoPhysicsLogic();
    }

}
