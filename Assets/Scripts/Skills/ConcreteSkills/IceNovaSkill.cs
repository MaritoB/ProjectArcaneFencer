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

    public override void UseSkill(Vector3 aDirection)
    { 
        if (IceNovaEffect == null)
        {
            if (IceNovaEffectPrefab != null)
            {
                GameObject obj = Instantiate(IceNovaEffectPrefab, null);
                IceNovaEffect = obj.GetComponent<ParticleSystem>();
            }
        }

        if ((int)mPlayerStats.iceNovaLevel.GetValue() != currentSkillLevel)
        {
            UpdateSkillLevel();
        }
        IceNovaEffect.transform.position = mPlayer.transform.position;
        AudioManager.instance.PlayOneShot(IceNovaSfx, mPlayer.transform.position);
        IceNovaEffect.Emit(25 * currentSkillLevel);
        Collider[] enemiesColliders = Physics.OverlapSphere(mPlayer.transform.position, currentRadius, EnemyLayer);
        foreach (Collider enemy in enemiesColliders)
        {
            IDamageable damageableEnemy = enemy.GetComponent<IDamageable>();
            if (damageableEnemy != null)
            {
                damageableEnemy.TakeDamage(attackInfo);
            }
        }
    }
    public override void UseSkill(Vector3 APosition, int aSkillLevel)
    {
        Debug.Log("Using Ice Nova");

        currentDamage = DamageBase + DamageMultiplier * aSkillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * aSkillLevel;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.COLD, false, true, 0f, mPlayer.gameObject);
        }
        else
        {
            attackInfo.damage = currentDamage;
        }
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
        foreach (Collider enemy in enemiesColliders)
        {
            IDamageable damageableEnemy = enemy.GetComponent<IDamageable>();
            if (damageableEnemy != null)
            {
                damageableEnemy.TakeDamage(attackInfo);
            }
        }
    }
    public void UpdateSkillLevel()
    {
        currentSkillLevel = (int)mPlayerStats.iceNovaLevel.GetValue();
        currentDamage = DamageBase + DamageMultiplier * currentSkillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * currentSkillLevel;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.COLD, false, true, 0f, mPlayer.gameObject);
        }
        else
        {
            attackInfo.damage = currentDamage;
        } 
        IceNovaEffectPrefab.transform.localScale = Vector3.one * currentRadius;
    }
    /*
    public override void SetSkillLevel(int aNewlevel)
    {
        skillLevel++;
        currentDamage = DamageBase + DamageMultiplier * skillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * skillLevel;
        if(attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.COLD, false, true, 0f, mPlayer.gameObject); 
        }
        else
        {
            attackInfo.damage = currentDamage;
        }

        IceNovaEffectPrefab.transform.localScale = Vector3.one * currentRadius; 
    }
     */

}