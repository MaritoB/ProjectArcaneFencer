
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
        Debug.Log("Knockback Enter");
        enemy.IsAttacking = false;
        enemy.CanMove = false;
        enemy.IsStunned = true;
        enemy.animator.SetTrigger("Knockback");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        if (enemy == null) return;
        enemy.CanMove = true;
        enemy.IsStunned = false;
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
