using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "_ProjectilesOnThirdStrikePerformedModifier", menuName = "WeaponModifiers/ProjectilesOnThirdStrikePerformedModifier")]
public class ProjectilesOnThirdStrikePerformedModifier : WeaponModifierSO
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
            aPlayer.sword.OnThirdMeleePerformed -= TryCastTripleProjectiles;
            aPlayer.sword.OnThirdMeleePerformed += TryCastTripleProjectiles;
        }
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
       
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        modifierDescription = "your third Strike have " + CurrentTriggerChance + "% chance to cast 3 Projectiles";
    }
    public void TryCastTripleProjectiles()
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            Transform shootPosition = projectileSpawner.ShootPosition;
            projectileSpawner.ShootProjectileToDirectionFromPool((Quaternion.Euler(0, 15, 0) * shootPosition.forward), shootPosition.position + Vector3.forward * 0.4f);
            projectileSpawner.ShootProjectileToDirectionFromPool(projectileSpawner.ShootPosition.forward, projectileSpawner.ShootPosition.position);
            projectileSpawner.ShootProjectileToDirectionFromPool((Quaternion.Euler(0, -15, 0) * projectileSpawner.ShootPosition.forward), projectileSpawner.ShootPosition.position - Vector3.forward * 0.4f);
        }
    }

}
