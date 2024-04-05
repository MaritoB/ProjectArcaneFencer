
using UnityEngine;
[CreateAssetMenu(fileName = "OnHitEffect-Damage", menuName = "Projectile /On Hit Effects/Damage on hit")]
public class DamageOnHit : OnHitEffectSOBase
{
    [SerializeField]
    int damage;
    public override void OnHitEffect(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, null);
        }
    }
}
