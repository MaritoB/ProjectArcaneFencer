using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_RecoverStaminaOnThirdStrike", menuName = "WeaponModifiers/RecoverStaminaOnThirdStrike")]
public class RecoverStaminaOnThirdStrike : WeaponModifierSO
{
    public int RecoverStaminaOnParryBase, RecoverStaminaOnParryMultiplier;
    int StaminaRecovery;
    PlayerController player;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        StaminaRecovery = RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.sword.OnThirdMeleeHit -= RecoverStaminaOnParry;
        aPlayer.sword.OnThirdMeleeHit += RecoverStaminaOnParry;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        modifierDescription = "Recover " + (RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier *(modifierLevel+1)) + " Stamina  on Third strike";
    }
    public void RecoverStaminaOnParry(Enemy enemy)
    {
        player.RecoverStamina(StaminaRecovery);

    }
}

