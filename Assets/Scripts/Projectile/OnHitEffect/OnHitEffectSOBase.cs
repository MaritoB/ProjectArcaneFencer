using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitEffectSOBase : ScriptableObject, IProjectileOnHitEffect
{
    public virtual void OnHitEffect(Collider collider)
    {
    }
}
