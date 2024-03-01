
using UnityEngine;

public class AfterHitEffectSOBase : ScriptableObject, IProjectileAfterHitEffect
{
    public virtual void AfterHitEffect(ProjectileBehaviour projectile)
    {
    }

    public virtual void ResetAfterHitEffect()
    {
    }
}
