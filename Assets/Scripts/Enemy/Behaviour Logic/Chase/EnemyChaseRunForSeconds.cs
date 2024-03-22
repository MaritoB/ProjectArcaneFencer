
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-RunForSeconds", menuName = "Enemy Logic/Chase Logic/RunForSeconds ")]
public class EnemyChaseRunForSeconds : EnemyChaseSOBase
{
    [SerializeField] private float _runAwaySpeed = 1f;
    [SerializeField] private float MinSeconds = 0.5f;
    [SerializeField] private float MaxSeconds = 1.5f;
    private float CurrentSeconds;
    Vector3 RunDirection;
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("ChangeDirection");
        CurrentSeconds = Random.Range(MinSeconds, MaxSeconds);
        Quaternion newRotation = new Quaternion(0, CurrentSeconds, 0, 0);
        RunDirection = (newRotation * enemy.mRigidbody.velocity).normalized;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic(); CurrentSeconds -= Time.deltaTime;
        if (CurrentSeconds < 0)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyIdleState);
        }
        enemy.AimDirection(RunDirection);
        enemy.MoveEnemy(RunDirection * _runAwaySpeed);
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
