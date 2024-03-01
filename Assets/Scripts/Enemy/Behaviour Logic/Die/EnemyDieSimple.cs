using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Simple Die", menuName = "Enemy Logic/Die Logic/Simple Die ")]
public class EnemyDieSimple : EnemyDieSOBase
{
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.SetTrigger("Die");
        enemy.Rigidbody.velocity = Vector3.zero;
        enemy.Rigidbody.useGravity = false;
        enemy.GetComponent<CapsuleCollider>().enabled = false;
        enemy.enabled = false;
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