
using UnityEngine;
[CreateAssetMenu(fileName = "_IceNovaOnSecondStrike", menuName = "WeaponModifiers/IceNovaOnSecondStrike")]
public class IceNovaOnSecondStrike : ItemModifierSO, IItemModifier
{
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    PlayerController player;

    public void ApplyModifier(PlayerController aPlayer)
    {
        RemoveModifier(player);
        player = aPlayer;
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleePerformed += TryCastIceNova;
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        GetDescription();
    }
    public string GetDescription()
    {
        return "+1 to Ice Nova & " + (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel + 1)) + "% chance to cast it on Second Attack. ";
    }
    public void TryCastIceNova()
    {
        if (player == null) return;
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
            player.inventory.equipmentManager.weapon.OnSecondMeleePerformed -= TryCastIceNova;
            player = null;
        }
    }
}

