using System;
using System.Collections;
using UnityEngine;

public class EnemyDieState : EnemyState
{

    public EnemyDieState(Enemy enemy, StateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);
        enemy.EnemyDieBaseInstance.DoAnimationTriggerEventLogic(aTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyDieBaseInstance.DoEnterLogic();
    }



    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyDieBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.EnemyDieBaseInstance.DoFrameUpdateLogic();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.EnemyDieBaseInstance.DoPhysicsLogic();
    }

}
