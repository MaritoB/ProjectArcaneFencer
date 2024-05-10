using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField]
    private ProjectileSpawner projectileSpawner;
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
