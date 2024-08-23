using UnityEngine;

[CreateAssetMenu(fileName = "Skill-FireballSkill", menuName = "Skills/Fireball")]
public class FireballSkill : SkillSOBase
{
    [SerializeField]
    AoEDamageOnHit mAoEDamageOnHit;
    [SerializeField]
    DisableAfterHitEffect mDisableAfterHitEffect;
    AoEDamageOnHit mAoEDamageOnHitInstance = null;
    DisableAfterHitEffect mDisableAfterHitEffectInstance = null;
    [SerializeField]
    GameObject FireballProjectileSpawnerPrefab;
    ProjectileSpawner FireballProjectileSpawner;
    [SerializeField]
    FMODUnity.EventReference FireballSfx;
    [SerializeField] float RadiusBase, RadiusMultiplier;
    [SerializeField] int DamageBase, DamageMultiplier;
    [SerializeField]
    float ProjectileSpeedBase, ProjectileSpeedMultiplier;

    public override void StartSetUp()
    {
        mAoEDamageOnHitInstance = Instantiate(mAoEDamageOnHit);
        mDisableAfterHitEffectInstance = Instantiate(mDisableAfterHitEffect);
        GameObject obj = Instantiate(FireballProjectileSpawnerPrefab, mPlayer.transform);
        FireballProjectileSpawner = obj.GetComponent<ProjectileSpawner>();
        if (FireballProjectileSpawner != null)
        {
            FireballProjectileSpawner.ShootPosition = mPlayer.GetComponent<PlayerController>().AttackTransform;
            FireballProjectileSpawner.SetNewProjectilePool(mAoEDamageOnHitInstance, mDisableAfterHitEffectInstance, ProjectileSpeedBase);
        }
    }
    public override void UseSkill(Vector3 APosition)
    {
        if (FireballProjectileSpawner == null)
        {
            GameObject obj = Instantiate(FireballProjectileSpawnerPrefab, mPlayer.transform);
            FireballProjectileSpawner = obj.GetComponent<ProjectileSpawner>();
        }
        if (FireballProjectileSpawner != null)
        {
            FireballProjectileSpawner.ShootPosition = mPlayer.GetComponent<PlayerController>().AttackTransform;
        }
        AudioManager.instance.PlayOneShot(FireballSfx, APosition);
        //Vector3 Direction = (APosition - mPlayer.position).normalized;
        FireballProjectileSpawner.ShootProjectileToDirectionFromPool(APosition, FireballProjectileSpawner.ShootPosition.position);
    } 
    public override void SetSkillLevel(int aNewlevel)
    {
        if(mAoEDamageOnHitInstance == null || mDisableAfterHitEffectInstance == null) { return; }
        skillLevel = aNewlevel;
        int  currentDamage = DamageBase + DamageMultiplier * skillLevel;
        float currentProjectileSpeed = ProjectileSpeedBase + ProjectileSpeedMultiplier * skillLevel;
        float currentRadius = RadiusBase + RadiusMultiplier * skillLevel;
        mAoEDamageOnHitInstance.SetDamage(currentDamage);
        mAoEDamageOnHitInstance.SetRadius(currentRadius);
        FireballProjectileSpawner.SetNewProjectilePool(mAoEDamageOnHitInstance, mDisableAfterHitEffectInstance, currentProjectileSpeed);
    }

}