
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Blocking", menuName = "Enemy Logic/Chase Logic/ChaseBlocking")]
public class EnemyChaseBlocking : EnemyChaseSOBase
{
    [SerializeField] private float _MovementSpeed = 1.5f;
    BossSkeletonEnemy enemyWithShield = null;
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        if (enemyWithShield == null)
        {
            enemyWithShield = enemy.GetComponent<BossSkeletonEnemy>();
        }
        if (enemyWithShield != null)
        {
            enemyWithShield.animator.SetBool("WalkBlock", true);
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemyWithShield.IsBlocking = false;
        enemyWithShield.animator.SetBool("WalkBlock", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyAttackState);
        }
        if (!enemy.IsWithinAggroDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        }
        if(playerTransform == null)
        {
            return;
        }
        enemy.AimPlayerPosition();
        Vector3 Direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(Direction * _MovementSpeed);
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
