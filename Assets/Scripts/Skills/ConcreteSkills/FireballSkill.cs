using UnityEngine;

[CreateAssetMenu(fileName = "Skill-FireballSkill", menuName = "Skills/Fireball")]
public class FireballSkill : SkillSOBase
{
    [SerializeField]
    AoEDamageOnHit mAoEDamageOnHit;
    [SerializeField]
    DisableAfterHitEffect mDisableAfterHitEffect;
    AoEDamageOnHit mAoEDamageOnHitInstance = null;
    //DisableAfterHitEffect mDisableAfterHitEffectInstance = null;
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
        attackInfo = new AttackInfo(DamageBase, DamageType.FIRE, false, false, 0f, mPlayer.gameObject);
        mAoEDamageOnHitInstance.SetAttackInfo(attackInfo);
        mAoEDamageOnHitInstance.SetRadius(RadiusBase);
        GameObject obj = Instantiate(FireballProjectileSpawnerPrefab, mPlayer.transform);
        FireballProjectileSpawner = obj.GetComponent<ProjectileSpawner>();
        if (FireballProjectileSpawner != null)
        {
            FireballProjectileSpawner.ShootPosition = mPlayer.GetComponent<PlayerController>().AttackTransform;
            FireballProjectileSpawner.SetupProjectilePool(mAoEDamageOnHitInstance, mDisableAfterHitEffect);
            //FireballProjectileSpawner.SetNewProjectilePool(mAoEDamageOnHitInstance, mDisableAfterHitEffectInstance, ProjectileSpeedBase);
        }
    }
    public override void UseSkill(Vector3 aDirection)
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
        if ((int)mPlayerStats.fireballLevel.GetValue() !=  currentSkillLevel)
        {
            UpdateSkillLevel();
        }
        AudioManager.instance.PlayOneShot(FireballSfx, mPlayer.transform.position);
        //Vector3 Direction = (APosition - mPlayer.position).normalized;
        FireballProjectileSpawner.ShootProjectileToDirectionFromPool(aDirection, FireballProjectileSpawner.ShootPosition.position);
    } 
    public void UpdateSkillLevel()
    {
        if (mAoEDamageOnHitInstance == null ) { return; }
        currentSkillLevel =(int) mPlayerStats.fireballLevel.GetValue();
        int currentDamage = DamageBase + DamageMultiplier * currentSkillLevel;
        float currentProjectileSpeed = ProjectileSpeedBase + ProjectileSpeedMultiplier * currentSkillLevel;
        float currentRadius = RadiusBase + RadiusMultiplier * currentSkillLevel;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.FIRE, false, true, 0f, mPlayer.gameObject);
        }
        else
        {
            attackInfo.damage = currentDamage;
        }

        mAoEDamageOnHitInstance.SetAttackInfo(attackInfo);
        mAoEDamageOnHitInstance.SetRadius(currentRadius);
    } 

}