
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHitStun", menuName = "Enemy Logic/Stun Logic/EnemyHitStun")]
public class EnemyHitStun : EnemyStunSOBase
{
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.StopMovement();
        enemy.CanMove = false;
        enemy.IsAttacking = false;
        enemy.animator.SetTrigger("OnHitStun");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
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
