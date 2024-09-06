
using SkeletonEditor;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnHitEffect-AoEDamage", menuName = "Projectile /On Hit Effects/AoE Damage on hit")]
public class AoEDamageOnHit : OnHitEffectSOBase
{
    [SerializeField]
    float radius; 
    [SerializeField]
    LayerMask EnemyLayer;
    AttackInfo attackInfo;
    public override void OnHitEffect(Collider collider)
    {
        Collider[] enemiesColliders =  Physics.OverlapSphere(collider.transform.position, radius, EnemyLayer);
        foreach (Collider enemy in enemiesColliders)
        {
            IDamageable damageableEnemy = enemy.GetComponent<IDamageable>();
            if (damageableEnemy != null)
            {
                damageableEnemy.TakeDamage(attackInfo);
            }
        }
    }

    public void SetAttackInfo(AttackInfo aAttackInfo)
    {
        attackInfo = aAttackInfo;
    }
    public void SetRadius(float aRadius)
    {
        radius = aRadius;
    }
}
