using UnityEngine;

[CreateAssetMenu(fileName = "Skill-FireNova", menuName = "Skills/FireNova")]
public class FireNovaSkill : SkillSOBase
{
    [SerializeField]
    LayerMask EnemyLayer;
    [SerializeField]
    GameObject FireNovaEffectPrefab;
    ParticleSystem FireNovaEffect;
    Animator FireNovaAnimator;
    [SerializeField]
    FMODUnity.EventReference FireNovaSfx;
    [SerializeField] float RadiusBase, RadiusMultiplier; 
    float currentRadius;

    [SerializeField] int DamageBase, DamageMultiplier;
    int currentDamage;

    public override void UseSkill(Vector3 aDirection)
    {
        if (FireNovaEffect == null)
        {
            if (FireNovaEffectPrefab != null)
            {
                GameObject obj = Instantiate(FireNovaEffectPrefab, null);
                FireNovaEffect = obj.GetComponent<ParticleSystem>();
                FireNovaAnimator = obj.GetComponent<Animator>();
            }
        }

        if ((int)mPlayerStats.fireNovaLevel.GetValue() != currentSkillLevel)
        {
            UpdateSkillLevel();
        }
        FireNovaEffect.transform.position = mPlayer.transform.position;
        FireNovaAnimator.SetTrigger("Cast");
        Debug.Log("Using Fire Nova lvl " + currentSkillLevel);
        //AudioManager.instance.PlayOneShot(FireNovaSfx, mPlayer.transform.position);
        FireNovaEffect.Emit(25 * currentSkillLevel);
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
        Debug.Log("Using Fire Nova");

        currentDamage = DamageBase + DamageMultiplier * aSkillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * aSkillLevel;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.FIRE, false, true, 0f, mPlayer.gameObject);
        }
        else
        {
            attackInfo.damage = currentDamage;
        }
        FireNovaEffectPrefab.transform.localScale = Vector3.one * currentRadius; 

        if (FireNovaEffect == null)
        {
            if (FireNovaEffectPrefab != null)
            {
                GameObject obj = Instantiate(FireNovaEffectPrefab, null);
                FireNovaEffect = obj.GetComponent<ParticleSystem>();
            }
        }
        FireNovaEffect.transform.position = APosition;
        AudioManager.instance.PlayOneShot(FireNovaSfx, APosition);
        FireNovaEffect.Emit(10 * aSkillLevel);
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
        currentSkillLevel = (int)mPlayerStats.fireNovaLevel.GetValue();
        currentDamage = DamageBase + DamageMultiplier * currentSkillLevel;
        currentRadius = RadiusBase + RadiusMultiplier * currentSkillLevel;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.FIRE, false, true, 0f, mPlayer.gameObject);
        }
        else
        {
            attackInfo.damage = currentDamage;
        } 
        FireNovaEffectPrefab.transform.localScale = Vector3.one * currentRadius;
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

        FireNovaEffectPrefab.transform.localScale = Vector3.one * currentRadius; 
    }
     */

}