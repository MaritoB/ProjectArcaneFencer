
using UnityEngine;
[CreateAssetMenu(fileName = "_IceNovaOnSecondStrike", menuName = "WeaponModifiers/IceNovaOnSecondStrike")]
public class IceNovaOnSecondStrike : WeaponModifierSO
{
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        Player.mSkillManager.LevelUpIceNova(modifierLevel);
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleePerformed -= TryCastIceNova;
        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleePerformed += TryCastIceNova;
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        modifierDescription = "+1 to Ice Nova & " + (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel + 1)) + "% chance to cast it on Second Attack. ";
    }
    public void TryCastIceNova()
    {
        int number = Random.Range(0, 100);
        if (number < currentTriggerChance)
        {
            Player.mSkillManager.UseIceNova();
        }

    }
}
