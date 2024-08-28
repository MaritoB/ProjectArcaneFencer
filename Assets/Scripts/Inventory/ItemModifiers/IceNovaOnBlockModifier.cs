 
using UnityEngine;
[CreateAssetMenu(fileName = "_IceNovaOnBlockModifier", menuName = "WeaponModifiers/IceNovaOnBlockModifier")]
public class IceNovaOnBlockModifier : ItemModifierSO, IItemModifier
{
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    PlayerController player;

    public void ApplyModifier(PlayerController aPlayer)
    {
        RemoveModifier(player);
        player = aPlayer;
        player.OnBlockPerformed += TryCastIceNova;
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel; 
    }
    public string GetDescription()
    {
        return  (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel)) + "% chance to Ice Nova on Block. ";
    }
    public void TryCastIceNova(Enemy aEnemy)
    {
        int number = Random.Range(0, 100); 
        if (number < currentTriggerChance)
        {
            player.mSkillManager.UseIceNova(modifierLevel);
        } 
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        if (player != null && aPlayer == player)
        {
            player.OnBlockPerformed -= TryCastIceNova;
            player = null;
        }
    }
}
