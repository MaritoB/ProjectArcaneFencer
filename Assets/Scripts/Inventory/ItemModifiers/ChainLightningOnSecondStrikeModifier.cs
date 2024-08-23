
using UnityEngine;
[CreateAssetMenu(fileName = "_ChainLightningOnSecondStrikeModifier", menuName = "WeaponModifiers/ChainLightningOnSecondStrikeModifier")]
public class ChainLightningOnSecondStrikeModifier : ItemModifierSO, IItemModifier  
{ 
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int CurrentTriggerChance;

    PlayerController mPlayer;

    public void ApplyModifier(PlayerController aPlayer)
    {
        mPlayer = aPlayer;
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleeHit -= TryCastChainLightning;
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleeHit += TryCastChainLightning;
        CurrentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel; 
        GetDescription();
    }
    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleeHit -= TryCastChainLightning;
    }
    public string GetDescription()
    { 

        return "+1 to Chain Lightning & " + CurrentTriggerChance + "% chance to cast it hitting enemies with your Second Attack.";
    }
    public void TryCastChainLightning(Enemy enemy)
    {
        int number = Random.Range(0, 100);
        if (number < CurrentTriggerChance)
        {
            Vector3 Direction = (enemy.transform.position - mPlayer.transform.position).normalized;
            mPlayer.mSkillManager.UseChainLightningSkill(Direction);
        }

    }

}
