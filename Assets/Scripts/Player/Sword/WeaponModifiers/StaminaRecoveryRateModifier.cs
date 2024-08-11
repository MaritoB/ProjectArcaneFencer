using UnityEngine;
[CreateAssetMenu(fileName = "_StaminaRecoveryRate", menuName = "WeaponModifiers/StaminaRecoveryRate")]
public class StaminaRecoveryRateModifier : WeaponModifierSO
{
    public int StaminaBase, StaminaMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    { 
        aPlayer.playerStats.staminaRecoveryRate.AddModifier(StaminaMultiplier * modifierLevel);

        UpdateDescription();
    }
    public override void UpdateDescription()
    {

        modifierDescription = "Increase Stamina Recovery Rate by " + StaminaMultiplier +". ( "+ (StaminaBase + StaminaMultiplier * (modifierLevel + 1)) + " )";
    }

}
