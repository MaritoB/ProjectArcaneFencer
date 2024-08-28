
using UnityEngine;
[CreateAssetMenu(fileName = "_ChainLightningOnSecondStrikeModifier", menuName = "WeaponModifiers/ChainLightningOnSecondStrikeModifier")]
public class ChainLightningOnSecondStrikeModifier : ItemModifierSO, IItemModifier  
{ 
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance; 
    PlayerController player;

    public void ApplyModifier(PlayerController aPlayer)
    {
        RemoveModifier(aPlayer);
        player = aPlayer;
        player.inventory.equipmentManager.weapon.OnSecondMeleeHit += TryCastChainLightning;
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel; 
        GetDescription();
    }
    public string GetDescription()
    {  
        return   CurrentTriggerChance + "% chance to cast Chain Lightning on 2nd.";
    }
    public void RemoveModifier(PlayerController aPlayer)
    {
        if (player != null && aPlayer == player)
        {
            player.inventory.equipmentManager.weapon.OnSecondMeleeHit -= TryCastChainLightning;
            player = null;
        }
    }
 
public void TryCastChainLightning(Enemy enemy)
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            Vector3 Direction = (enemy.transform.position - player.transform.position).normalized;
            player.mSkillManager.UseChainLightningSkill(Direction);
        }

    }

}
