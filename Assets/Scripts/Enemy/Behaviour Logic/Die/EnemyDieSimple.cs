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
        enemy.mRigidbody.velocity = Vector3.zero;
        enemy.mRigidbody.useGravity = false;
        enemy.GetComponent<CapsuleCollider>().enabled = false;
        //enemy.enabled = false;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
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