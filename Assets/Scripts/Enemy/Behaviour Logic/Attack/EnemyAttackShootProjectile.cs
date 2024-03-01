using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Attack-Shoot Projectile", menuName = "Enemy Logic/Attack Logic/Shoot Projectile")]
public class EnemyAttackShootProjectile : EnemyAttackSOBase
{
    RangeEnemy _rangeEnemy;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        enemy.AimPlayerPosition();
        enemy.MoveEnemy(Vector3.zero);
        if (enemy.IsAttacking)
        {
            return;
        }
        if (CanAttack())
        {
            enemy.animator.SetTrigger("Attack");
            enemy.IsAttacking = true;
            _currentAttackTime = AttackSpeed;
        }
        if (!enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyChaseState);
        }
        if (enemy.IsWithinFleeDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyFleeState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy, Transform PlayerTransform)
    {
        base.Initialize(gameObject, enemy, PlayerTransform);
        _rangeEnemy = gameObject.GetComponent<RangeEnemy>();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

}


