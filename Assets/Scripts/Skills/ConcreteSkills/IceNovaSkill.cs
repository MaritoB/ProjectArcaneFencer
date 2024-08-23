using UnityEngine;

[CreateAssetMenu(fileName = "Skill-IceNova", menuName = "Skills/IceNova")]
public class IceNovaSkill : SkillSOBase
{
    [SerializeField]
    LayerMask EnemyLayer;
    [SerializeField]
    GameObject IceNovaEffectPrefab;
    ParticleSystem IceNovaEffect;
    [SerializeField]
    FMODUnity.EventReference IceNovaSfx;
    [SerializeField] float RadiusBase, RadiusMultiplier;
    float currentRadius;

    [SerializeField] int DamageBase, DamageMultiplier;
    int currentDamage;

    public override void UseSkill(Vector3 APosition)
    {
        Debug.Log("Using Ice Nova");
        if (IceNovaEffect == null)
        {
            if (IceNovaEffectPrefab != null)
            {
                GameObject obj = Instantiate(IceNovaEffectPrefab, null);
                IceNovaEffect = obj.GetComponent<ParticleSystem>();
            }
        }
        IceNovaEffect.transform.position = APosition;
        AudioManager.instance.PlayOneShot(IceNovaSfx, APosition);
        IceNovaEffect.Emit(25 * skillLevel);
        Collider[] enemiesColliders = Physics.OverlapSphere(APosition, currentRadius, EnemyLayer);
        AttackInfo newAttack = new AttackInfo(currentDamage, true, false, mPlayer.gameObject);
        foreach (Collider enemy in enemiesColliders)
        {
            IDamageable damageableEnemy = enemy.GetComponent<IDamageable>();
            if (damageableEnemy != null)
            {
                damageableEnemy.TakeDamage(newAttack);
            }
        }
    }
    public override void UseSkill(Vector3 APosition, int aSkillLevel)
    {
        Debug.Log("Using Ice Nova");

        currentDamage = DamageBase + DamageMultiplier * aSkillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * aSkillLevel;
        IceNovaEffectPrefab.transform.localScale = Vector3.one * currentRadius; 

        if (IceNovaEffect == null)
        {
            if (IceNovaEffectPrefab != null)
            {
                GameObject obj = Instantiate(IceNovaEffectPrefab, null);
                IceNovaEffect = obj.GetComponent<ParticleSystem>();
            }
        }
        IceNovaEffect.transform.position = APosition;
        AudioManager.instance.PlayOneShot(IceNovaSfx, APosition);
        IceNovaEffect.Emit(25 * aSkillLevel);
        Collider[] enemiesColliders = Physics.OverlapSphere(APosition, currentRadius, EnemyLayer);
        AttackInfo newAttack = new AttackInfo(currentDamage, true, false, mPlayer.gameObject);
        foreach (Collider enemy in enemiesColliders)
        {
            IDamageable damageableEnemy = enemy.GetComponent<IDamageable>();
            if (damageableEnemy != null)
            {
                damageableEnemy.TakeDamage(newAttack);
            }
        }
    }

    public override void SetSkillLevel(int aNewlevel)
    {
        skillLevel++;
        currentDamage = DamageBase + DamageMultiplier * skillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * skillLevel;
        IceNovaEffectPrefab.transform.localScale = Vector3.one * currentRadius;
        Debug.Log("IceNova lvl " + skillLevel + ", Damage " + currentDamage + ", Radius  "+ currentRadius);
    }

}