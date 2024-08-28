using UnityEngine;

[CreateAssetMenu(fileName = "_ChainLightningOnBlockModifier", menuName = "WeaponModifiers/ChainLightningOnBlockModifier")]
public class ChainLightningOnBlockModifier : ItemModifierSO, IItemModifier
{
    PlayerController player; 
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;
    public void ApplyModifier(PlayerController aPlayer)
    {
        player = aPlayer;
        RemoveModifier(player);
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        player.OnBlockPerformed += CastProjectile; 
    }
    public string GetDescription()
    {
       return (TriggerChanceBase + TriggerChanceMultiplier * ( modifierLevel +1 )) + "% chance to cast Chain Lightning on Block.";
    }
 
    public void CastProjectile(Enemy aEnemy)
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            Vector3 direction = (player.transform.position - aEnemy.transform.position).normalized;
            player.mSkillManager.UseChainLightningSkill(direction);
        }
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        if (aPlayer != null)
        {
            aPlayer.OnBlockPerformed -= CastProjectile;
            aPlayer = null;
        }
    }
}
