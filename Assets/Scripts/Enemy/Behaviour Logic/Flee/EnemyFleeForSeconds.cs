using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Flee-Flee for Seconds", menuName = "Enemy Logic/Flee Logic/ Flee For Seconds ")]
public class EnemyFleeForSeconds : EnemyFleeSOBase
{
    [SerializeField] private float _runAwaySpeed = 1f;
    [SerializeField] private float MinSeconds = 0.5f;
    [SerializeField] private float MaxSeconds = 2f;
    private float CurrentSeconds;
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        CurrentSeconds = Random.Range(MinSeconds,MaxSeconds);

    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        CurrentSeconds -=Time.deltaTime;
        if (CurrentSeconds < 0)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        }
        Vector3 RunDirection = -(playerTransform.position - transform.position).normalized;
        enemy.AimDirection(RunDirection);
        enemy.MoveEnemy(RunDirection * _runAwaySpeed);
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