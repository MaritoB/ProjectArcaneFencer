
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Run Away", menuName = "Enemy Logic/Chase Logic/Run Away ")]
public class EnemyChaseRunAway : EnemyChaseSOBase
{
    [SerializeField] private float _runAwaySpeed = 1f;
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
        Vector3 RunDirection = -(playerTransform.position - transform.position).normalized;
        enemy.MoveEnemy(RunDirection * _runAwaySpeed);
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
