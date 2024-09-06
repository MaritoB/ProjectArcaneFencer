
using UnityEngine;
[CreateAssetMenu(fileName = "OnHitEffect-Damage", menuName = "Projectile /On Hit Effects/Damage on hit")]
public class DamageOnHit : OnHitEffectSOBase
{  
    public int CriticalChance { get; private set; }
    public override void OnHitEffect(Collider collider)
    { 
        if(attackInfo == null)
        {
            Debug.Log("nullAttackInfo");
        }
        attackInfo.isCritical = Random.Range(1, 100) < CriticalChance; 
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(attackInfo);
        }
    }
}
