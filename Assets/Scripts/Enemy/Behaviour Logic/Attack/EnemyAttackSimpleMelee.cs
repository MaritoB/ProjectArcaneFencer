
using UnityEngine;


[CreateAssetMenu(fileName = "Attack-Simple-Melee", menuName = "Enemy Logic/Attack Logic/Simple Melee ")]
public class EnemyAttackSimpleMelee : EnemyAttackSOBase
{

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
       // enemy.MoveEnemy(Vector3.zero);
        if (enemy.IsAttacking) return;

        if (!enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyChaseState);
            return;
        }

        if (CanAttack())
        {
            enemy.IsAttacking = true;
            _currentAttackTime = enemy.GetAttackRate();
            enemy.animator.SetTrigger("Attack");
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

}

