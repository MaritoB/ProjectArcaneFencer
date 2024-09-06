using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitEffectSOBase : ScriptableObject, IProjectileOnHitEffect
{
    protected AttackInfo attackInfo;
    public void SetAttackInfo( AttackInfo aAttackInfo)
    {
        if (aAttackInfo == null)
        {
            Debug.Log("Null Attack Info ");
            return;
        }
        attackInfo = aAttackInfo;
    }
    public virtual void OnHitEffect(Collider collider)
    {
    }
}
