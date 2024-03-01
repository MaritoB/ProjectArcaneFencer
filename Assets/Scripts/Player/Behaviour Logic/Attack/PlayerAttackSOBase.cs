using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSOBase : ScriptableObject
{
    protected PlayerController player;
    protected GameObject gameObject;
    [SerializeField] protected float AttackSpeed;
    protected float _currentAttackTime = 0f;

    public virtual void Initialize(GameObject aGameObject, PlayerController aPlayer)
    {
        this.gameObject = aGameObject;
        this.player = aPlayer;
    }

    public bool CanAttack()
    {
        return _currentAttackTime < 0;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
        _currentAttackTime -= Time.deltaTime;
    }
    public virtual void DoPhysicsLogic() { }
   // public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}

