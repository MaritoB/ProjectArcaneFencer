using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeState : EnemyState
{
    private Transform _playerTransform;
    public EnemyFleeState(Enemy enemy, StateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        //_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType aTriggerType)
    {
        base.AnimationTriggerEvent(aTriggerType);

        enemy.EnemyFleeBaseInstance.DoAnimationTriggerEventLogic(aTriggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyFleeBaseInstance.DoEnterLogic();
    }


    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyFleeBaseInstance.DoEnterLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.EnemyFleeBaseInstance.DoFrameUpdateLogic();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.EnemyFleeBaseInstance.DoPhysicsLogic();
    }

}
