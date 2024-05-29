
using UnityEngine;
[CreateAssetMenu(fileName = "OnHitEffect-Damage", menuName = "Projectile /On Hit Effects/Damage on hit")]
public class DamageOnHit : OnHitEffectSOBase
{
    [SerializeField]
    int damage, criticalChance;
    [SerializeField]
    bool isMagic;
    AttackInfo attackInfo;
    GameObject Source;

    public int CriticalChance { get; private set; }

    public void SetDamage(int aDamage)
    {
        damage = aDamage;
    }
    public void SetCriticalChance(int aCriticalChance)
    {
        criticalChance = aCriticalChance;
    }
    public void SetSource(GameObject aSource)
    {
        Source = aSource;
    }
    public override void OnHitEffect(Collider collider)
    {
        bool isCritical = false;

        if (Random.Range(1, 100) < CriticalChance)
        {
            isCritical = true;
        }
        attackInfo = new AttackInfo(isCritical?damage*2:damage, isMagic, isCritical, Source);
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(attackInfo);
        }
    }
}
