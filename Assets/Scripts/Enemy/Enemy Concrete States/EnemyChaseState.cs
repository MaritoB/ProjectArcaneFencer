using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform _playerTransform;
    public EnemyChaseState(Enemy enemy, StateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        //_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);

        enemy.EnemyChaseBaseInstance.DoAnimationTriggerEventLogic(aTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyChaseBaseInstance.DoEnterLogic();
    }


    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyChaseBaseInstance.DoEnterLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.EnemyChaseBaseInstance.DoFrameUpdateLogic();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.EnemyChaseBaseInstance.DoPhysicsLogic();
    }

}
