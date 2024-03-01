
using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
{

    protected Enemy enemy;
    protected Transform transform;
    protected Transform playerTransform;
    protected GameObject gameObject;
    [SerializeField] protected float AttackSpeed;
    protected float _currentAttackTime = 0f;



    public virtual void Initialize(GameObject gameObject, Enemy enemy, Transform PlayerTransform)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;
        playerTransform = PlayerTransform;
    }
    public bool CanAttack()
    {
        return _currentAttackTime < 0;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {
        _currentAttackTime -= Time.deltaTime;
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
