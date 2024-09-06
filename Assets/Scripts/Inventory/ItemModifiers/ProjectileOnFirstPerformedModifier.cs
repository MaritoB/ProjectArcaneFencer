using UnityEngine;
[CreateAssetMenu(fileName = "_ProjectileOnFirstAttackPerformedModifier", menuName = "WeaponModifiers/ProjectileOnFirstAttackPerformedModifier")]
public class ProjectileOnFirstPerformedModifier : ItemModifierSO, IItemModifier
{
    public GameObject ProjectileSpawnerPrefab;
    ProjectileSpawner projectileSpawner;
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;

    PlayerController Player;

    public void ApplyModifier(PlayerController aPlayer)
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
            aPlayer.inventory.equipmentManager.weapon.OnFirstMeleePerformed -= CastProjectile;
            aPlayer.inventory.equipmentManager.weapon.OnFirstMeleePerformed += CastProjectile;
        }
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        
    }
    public string GetDescription(int aModifierLevel)
    {
        return "Your First Attack has " + (TriggerChanceBase + TriggerChanceMultiplier * (aModifierLevel  )) + "% chance to cast  this Projectile Skill";
    }
    public void CastProjectile()
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            projectileSpawner.ShootProjectileToDirectionFromPool(projectileSpawner.ShootPosition.forward, projectileSpawner.ShootPosition.position);
        }
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.inventory.equipmentManager.weapon.OnFirstMeleePerformed -= CastProjectile;
    }
}
