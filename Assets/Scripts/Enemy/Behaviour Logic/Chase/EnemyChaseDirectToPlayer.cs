
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Chase Direct ")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _MovementSpeed = 1.5f;
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
        Vector3 Direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(Direction * _MovementSpeed);
        if (enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyAttackState);
        }
        if (!enemy.IsWithinAggroDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy, Transform aPlayerTransform)
    {
        base.Initialize(gameObject, enemy, aPlayerTransform);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
