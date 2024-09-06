using UnityEngine;

[CreateAssetMenu(fileName = "Skill-ChainLightnning", menuName = "Skills/ChainLightnning")]
public class ChainLightningSkill : SkillSOBase
{
    [SerializeField]
    DamageOnHit damageOnHit;
    [SerializeField]
    ChainAfterHitEffect chainHitEffect;
    DamageOnHit damageOnHitInstance = null;
    ChainAfterHitEffect chainHitEffectInstance = null;
    [SerializeField]
    GameObject ChainLightningSpawnerPrefab;
    ProjectileSpawner ChainLightningSpawner;
    [SerializeField]
    FMODUnity.EventReference chainLightningSfx;
    [SerializeField] float RadiusBase, RadiusMultiplier;
    float currentRadius;
    [SerializeField] int DamageBase, DamageMultiplier;
    [SerializeField]
    float ProjectileSpeedBase, ProjectileSpeedMultiplier;

    public override void StartSetUp()
    {
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(DamageBase, DamageType.LIGHTNING, false, true, 0f, mPlayer.gameObject);
        }
        else
        {
            attackInfo.damage = DamageBase;
        }
        damageOnHitInstance = Instantiate(damageOnHit);
        damageOnHitInstance.SetAttackInfo(attackInfo);
        GameObject obj = Instantiate(ChainLightningSpawnerPrefab, mPlayer.transform);
        ChainLightningSpawner = obj.GetComponent<ProjectileSpawner>();
        if (ChainLightningSpawner != null)
        {
            ChainLightningSpawner.ShootPosition = mPlayer.GetComponent<PlayerController>().AttackTransform;
            ChainLightningSpawner.SetupProjectilePool(damageOnHitInstance, chainHitEffect);
        } 
    }
    public override void UseSkill(Vector3 aDirection)
    {
        if (ChainLightningSpawner == null)
        {
            GameObject obj = Instantiate(ChainLightningSpawnerPrefab, mPlayer.transform);
            ChainLightningSpawner = obj.GetComponent<ProjectileSpawner>();
        }
        if (ChainLightningSpawner != null)
        {
            ChainLightningSpawner.ShootPosition = mPlayer.GetComponent<PlayerController>().AttackTransform;
        }

        if ((int)mPlayerStats.chainLightningLevel.GetValue() != currentSkillLevel)
        {
            UpdateSkillLevel();
        }
        AudioManager.instance.PlayOneShot(chainLightningSfx, mPlayer.transform.position);
        //Vector3 Direction =( APosition - mPlayer.position).normalized;
        ChainLightningSpawner.ShootProjectileToDirectionFromPool(aDirection, ChainLightningSpawner.ShootPosition.position);
    }

    public void UpdateSkillLevel()
    {
        if (damageOnHitInstance == null ) { return; }
        currentSkillLevel = (int)mPlayerStats.chainLightningLevel.GetValue();
        int currentDamage = DamageBase + DamageMultiplier * currentSkillLevel;
        float currentProjectileSpeed = ProjectileSpeedBase + ProjectileSpeedMultiplier * currentSkillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * currentSkillLevel;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.LIGHTNING, false, true, 0f, mPlayer.gameObject);
            Debug.LogWarning("Creating attack Info on SetSkillLevel");
        }
        else
        {
            attackInfo.damage = currentDamage;
        }
        damageOnHitInstance.SetAttackInfo(attackInfo);
        //chainHitEffectInstance.LevelUpEffect(currentSkillLevel, currentRadius);
    }
    /*
    public override void SetSkillLevel(int aNewlevel)
    {
        if(damageOnHitInstance == null || chainHitEffectInstance == null) { return; }
        skillLevel++; 
        int  currentDamage = DamageBase + DamageMultiplier * skillLevel;
        float currentProjectileSpeed = ProjectileSpeedBase + ProjectileSpeedMultiplier * skillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * skillLevel;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.LIGHTNING, false, true, 0f, mPlayer.gameObject);
            Debug.LogWarning("Creating attack Info on SetSkillLevel");
        }
        else
        {
            attackInfo.damage = currentDamage;
        }
        damageOnHitInstance.SetAttackInfo(attackInfo);
        chainHitEffectInstance.LevelUpEffect(skillLevel, currentRadius);
        //ChainLightningSpawner.SetNewProjectilePool(damageOnHitInstance, chainHitEffectInstance, currentProjectileSpeed);
    }
     */

}