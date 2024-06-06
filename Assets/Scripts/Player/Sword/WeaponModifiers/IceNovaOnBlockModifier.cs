
using FMOD;
using UnityEngine;
[CreateAssetMenu(fileName = "_IceNovaOnBlockModifier", menuName = "WeaponModifiers/IceNovaOnBlockModifier")]
public class IceNovaOnBlockModifier : WeaponModifierSO
{
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        aPlayer.OnBlockPerformed -= TryCastIceNova;
        aPlayer.OnBlockPerformed += TryCastIceNova;

        Player.mSkillManager.LevelUpIceNova(modifierLevel);
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();

        modifierDescription = "+1 to Ice Nova & " + (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel + 1)) + "% chance to cast it when blocking melee Damage. ";
    }
    public void TryCastIceNova(Enemy aEnemy)
    {
        int number = Random.Range(0, 100);
        if (number < currentTriggerChance)
        {
            Player.mSkillManager.UseIceNova();
        }

    }
}
