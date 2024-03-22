
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
        enemy.CanMove = false;
        enemy.animator.SetTrigger("KnockBack");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.IsAttacking = false;
        enemy.CanMove = true;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        //enemy.MoveEnemy(Vector3.zero);
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
