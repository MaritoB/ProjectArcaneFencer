
using UnityEngine;

[CreateAssetMenu(fileName = "KnockBack-Simple", menuName = "Enemy Logic/KnockBack Logic/KnockBack-simple")]
public class EnemyKnockBack : EnemyKnockBackSOBase
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
        enemy.MoveEnemy(Vector3.zero);
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
