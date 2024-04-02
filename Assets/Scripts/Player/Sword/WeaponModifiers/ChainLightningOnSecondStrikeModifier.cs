using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "_ChainLightningOnSecondStrikeModifier", menuName = "WeaponModifiers/ChainLightningOnSecondStrikeModifier")]
public class ChainLightningOnSecondStrikeModifier : WeaponModifierSO
{
    public GameObject ChainLightningSpawnerPrefab;
    ProjectileSpawner ChainLightningSpawner;
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;

    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        if(ChainLightningSpawner == null)
        {
            GameObject obj = Instantiate(ChainLightningSpawnerPrefab, aPlayer.transform);
            ChainLightningSpawner = obj.GetComponent<ProjectileSpawner>();
        }
        if (ChainLightningSpawner != null)
        {
            ChainLightningSpawner.ShootPosition = aPlayer.AttackTransform;
            aPlayer.sword.OnSecondMeleeHit -= TryCastChainLightning;
            aPlayer.sword.OnSecondMeleeHit += TryCastChainLightning;
        }
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        modifierDescription = modifierDescription = "your Second Strike have " + CurrentTriggerChance + "% chance to cast Chain Lightning on Hit";
    }
    public void TryCastChainLightning(Enemy enemy)
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            Vector3 Direction = (enemy.transform.position - Player.transform.position).normalized;
            ChainLightningSpawner.ShootProjectileToDirectionFromPool(Direction, ChainLightningSpawner.ShootPosition.position);
        }

    }
}
