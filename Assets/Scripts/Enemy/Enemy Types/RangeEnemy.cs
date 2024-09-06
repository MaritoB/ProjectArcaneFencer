using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField]
    private ProjectileSpawner projectileSpawner;

    [SerializeField]
    AfterHitEffectSOBase AfterHitEffect;
    [SerializeField]
    OnHitEffectSOBase OnHitEffect;

    
    public override void Start()
    {
        base.Start();
        if (projectileSpawner != null)
        {
            OnHitEffect = Instantiate(OnHitEffect);
            OnHitEffect.SetAttackInfo(attackInfo);
            projectileSpawner.SetupProjectilePool(OnHitEffect, AfterHitEffect);
        }
    }
      
    public override void Attack()
    {
        if (!IsAttacking || CurrentHealth < 1) return;
        base.Attack();
        IsAttacking = false;
        if (projectileSpawner != null)
        {
            projectileSpawner.ShootProjectileForwardFromPool();
        }
    }

}
