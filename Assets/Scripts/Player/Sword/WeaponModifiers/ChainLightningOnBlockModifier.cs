using UnityEngine;
[CreateAssetMenu(fileName = "_ChainLightningOnBlockModifier", menuName = "WeaponModifiers/ChainLightningOnBlockModifier")]
public class ChainLightningOnBlockModifier : WeaponModifierSO
{
    Transform playerTransform;
    public GameObject ProjectileSpawnerPrefab;
    ProjectileSpawner projectileSpawner;
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        playerTransform = aPlayer.transform;
        if (projectileSpawner == null)
        {
            GameObject obj = Instantiate(ProjectileSpawnerPrefab, aPlayer.transform);
            projectileSpawner = obj.GetComponent<ProjectileSpawner>();
        }
        if (projectileSpawner != null)
        {
            projectileSpawner.ShootPosition = aPlayer.AttackTransform;
            aPlayer.OnBlockPerformed -= CastProjectile;
            aPlayer.OnBlockPerformed += CastProjectile;
        }
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;

        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        modifierDescription =TriggerChanceBase + TriggerChanceMultiplier * ( modifierLevel +1 ) + "% chance to cast ChainLightning on Block";
    }
    public void CastProjectile(Enemy aEnemy)
    {
        int number = Random.Range(0, 100);
        Vector3 direction = (playerTransform.position - aEnemy.transform.position).normalized;
        if (number < CurrentTriggerChance)
        {
            projectileSpawner.ShootProjectileToDirectionFromPool(direction, projectileSpawner.ShootPosition.position);
        }
    }
}
