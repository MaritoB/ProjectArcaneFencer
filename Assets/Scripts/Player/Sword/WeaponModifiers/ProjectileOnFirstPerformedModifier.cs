using UnityEngine;
[CreateAssetMenu(fileName = "_ProjectileOnFirstAttackPerformedModifier", menuName = "WeaponModifiers/ProjectileOnFirstAttackPerformedModifier")]
public class ProjectileOnFirstPerformedModifier : WeaponModifierSO
{
    public GameObject ProjectileSpawnerPrefab;
    ProjectileSpawner projectileSpawner;
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;

    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        if (projectileSpawner == null)
        {
            GameObject obj = Instantiate(ProjectileSpawnerPrefab, aPlayer.transform);
            projectileSpawner = obj.GetComponent<ProjectileSpawner>();
        }
        if (projectileSpawner != null)
        {
            projectileSpawner.ShootPosition = aPlayer.AttackTransform;
            aPlayer.sword.OnFirstMeleePerformed -= CastProjectile;
            aPlayer.sword.OnFirstMeleePerformed += CastProjectile;
        }
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
       
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        modifierDescription = "Your First Attack has " + (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel + 1)) + "% chance to cast  this Projectile Skill";
    }
    public void CastProjectile()
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            projectileSpawner.ShootProjectileToDirectionFromPool(projectileSpawner.ShootPosition.forward, projectileSpawner.ShootPosition.position);
        }
    }

}
