
using UnityEngine;
[CreateAssetMenu(fileName = "_IceNovaOnDashModifier", menuName = "WeaponModifiers/IceNovaOnDashModifier")]
public class IceNovaOnDashModifier : WeaponModifierSO
{ 
    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;
        aPlayer.OnDash -= TryCastIceNova;
        aPlayer.OnDash += TryCastIceNova;

        Player.mSkillManager.LevelUpFireball(modifierLevel);
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        modifierDescription = "+1 to Ice Nova & " + (TriggerChanceBase + TriggerChanceMultiplier * (modifierLevel + 1)) + "% chance to cast it on Dash. ";
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
