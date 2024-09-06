using UnityEngine;

[CreateAssetMenu(fileName = "Skill-KnockbackSkill", menuName = "Skills/KnockbackSkill")]
public class KnockbackSkill : SkillSOBase
{
   
    [SerializeField] int ForceBase, ForceMultiplier;
    [SerializeField] int currentKnockbackForce;
    [SerializeField] float knockbackRadiusBase, radiusMultiplier, currentRadius;
    LayerMask enemyLayer;
    ParticleSystem hitPS; 
    [SerializeField] GameObject PSPrefab;
    PlayerController player;


    public override void StartSetUp()
    {
        player = mPlayer.GetComponent<PlayerController>();
        if (player != null)
        {
            enemyLayer = player.EnemiesLayer;
        }
        currentKnockbackForce = ForceBase + ForceMultiplier * currentSkillLevel;
        currentRadius = knockbackRadiusBase + radiusMultiplier * currentSkillLevel;
        hitPS = Instantiate(PSPrefab).GetComponent<ParticleSystem>();
    }

    public override void UseSkill(Vector3 aDirection)
    {

        if ((int)mPlayerStats.KnockbackLevel.GetValue() != currentSkillLevel)
        {
            UpdateSkillLevel();
        }

        Collider[] EnemyColliders = Physics.OverlapSphere(player.AttackTransform.position, currentRadius, enemyLayer);
        foreach (Collider collider in EnemyColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                hitPS.transform.position = enemy.transform.position;
                hitPS.Emit(10);
                Vector3 force = aDirection * currentKnockbackForce;
                force.y = 0;
                enemy.GetKnockBack(force);
            }
        }
    }

    public void UpdateSkillLevel()
    {
        currentKnockbackForce = ForceBase + ForceMultiplier * currentSkillLevel;
        currentRadius = knockbackRadiusBase + radiusMultiplier * currentSkillLevel;
    } 

}