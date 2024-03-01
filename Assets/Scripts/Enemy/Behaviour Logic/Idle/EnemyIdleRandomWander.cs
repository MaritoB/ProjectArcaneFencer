
using UnityEngine;
using static UnityEngine.LightAnchor;

[CreateAssetMenu(fileName = "idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float timeToChangeDirection = 5f;
    private float currentTimeToChangeDirection = 5f;
    [SerializeField]private float RandomMovementRange = 5f;
    [SerializeField] private float RandomMovementSpeed = 1f;
    private Vector3 _targetPosition;
    private Vector3 _direction;
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        _targetPosition = GetRandomPointInCircle();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        currentTimeToChangeDirection -= Time.deltaTime;
        if (currentTimeToChangeDirection < 0)
        {
            currentTimeToChangeDirection = timeToChangeDirection;
            _targetPosition = GetRandomPointInCircle();
        }
        _direction = (_targetPosition - enemy.transform.position).normalized;
        enemy.AimDirection(_direction);
        enemy.MoveEnemy(_direction * RandomMovementSpeed);
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
    private Vector3 GetRandomPointInCircle()
    {

        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }
}
