

using UnityEngine;

public class EnemyKnockBackSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Enemy enemy, Transform aPlayerTransform)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = aPlayerTransform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    {
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
