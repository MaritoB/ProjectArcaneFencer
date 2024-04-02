
using UnityEngine;

[CreateAssetMenu(fileName = "KnockBack-Simple", menuName = "Enemy Logic/Stun Logic/KnockBack-simple")]
public class EnemyKnockBack : EnemyStunSOBase
{
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        if (enemy == null) return;

        enemy.IsAttacking = false;
        enemy.CanMove = false;
        enemy.animator.SetTrigger("KnockBack");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        if (enemy == null) return;
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
