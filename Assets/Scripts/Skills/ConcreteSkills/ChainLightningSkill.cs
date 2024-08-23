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
        damageOnHitInstance = Instantiate(damageOnHit);
        chainHitEffectInstance = Instantiate(chainHitEffect);
        GameObject obj = Instantiate(ChainLightningSpawnerPrefab, mPlayer.transform);
        ChainLightningSpawner = obj.GetComponent<ProjectileSpawner>();
        if (ChainLightningSpawner != null)
        {
            ChainLightningSpawner.ShootPosition = mPlayer.GetComponent<PlayerController>().AttackTransform;
            ChainLightningSpawner.SetNewProjectilePool(damageOnHitInstance, chainHitEffectInstance, ProjectileSpeedBase);
        }



    }
    public override void UseSkill(Vector3 APosition)
    {
        Debug.Log("Using skill Chain Lightning");
        if (ChainLightningSpawner == null)
        {
            GameObject obj = Instantiate(ChainLightningSpawnerPrefab, mPlayer.transform);
            ChainLightningSpawner = obj.GetComponent<ProjectileSpawner>();
        }
        if (ChainLightningSpawner != null)
        {
            ChainLightningSpawner.ShootPosition = mPlayer.GetComponent<PlayerController>().AttackTransform;
        } 
        AudioManager.instance.PlayOneShot(chainLightningSfx, APosition);
        Vector3 Direction =( APosition - mPlayer.position).normalized;
        ChainLightningSpawner.ShootProjectileToDirectionFromPool(Direction, ChainLightningSpawner.ShootPosition.position);
    }
    public override void SetSkillLevel(int aNewlevel)
    {
        if(damageOnHitInstance == null || chainHitEffectInstance == null) { return; }
        skillLevel++; 
        int  currentDamage = DamageBase + DamageMultiplier * skillLevel;
        float currentProjectileSpeed = ProjectileSpeedBase + ProjectileSpeedMultiplier * skillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * skillLevel;
        damageOnHitInstance.SetDamage(currentDamage);
        chainHitEffectInstance.LevelUpEffect(skillLevel, currentRadius);
        ChainLightningSpawner.SetNewProjectilePool(damageOnHitInstance, chainHitEffectInstance, currentProjectileSpeed);
    }

}