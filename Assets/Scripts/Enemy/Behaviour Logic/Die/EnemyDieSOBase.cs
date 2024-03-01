
using UnityEngine;

public class EnemyDieSOBase : ScriptableObject
{

    protected Enemy enemy;
    protected Transform transform;
    protected Transform playerTransform;
    protected GameObject gameObject;
    public virtual void Initialize(GameObject gameObject, Enemy enemy, Transform PlayerTransform)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = PlayerTransform;
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


