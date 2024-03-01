
using UnityEngine;

[CreateAssetMenu(fileName = "OnHitEffect-AoEDamage", menuName = "Projectile /On Hit Effects/AoE Damage on hit")]
public class AoEDamageOnHit : OnHitEffectSOBase
{
    [SerializeField]
    float radius;
    [SerializeField]
    int damage;
    [SerializeField]
    LayerMask EnemyLayer;
    public override void OnHitEffect(Collider collider)
    {
        Collider[] enemiesColliders =  Physics.OverlapSphere(collider.transform.position, radius, EnemyLayer);
        foreach (Collider enemy in enemiesColliders)
        {
            IDamageable damageableEnemy = enemy.GetComponent<IDamageable>();
            if (damageableEnemy != null)
            {
                damageableEnemy.TakeDamage(damage);
            }
        }
    }
}
