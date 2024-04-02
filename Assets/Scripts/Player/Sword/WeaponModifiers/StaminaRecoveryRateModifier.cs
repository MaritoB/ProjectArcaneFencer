using UnityEngine;
[CreateAssetMenu(fileName = "_StaminaRecoveryRate", menuName = "WeaponModifiers/StaminaRecoveryRate")]
public class StaminaRecoveryRateModifier : WeaponModifierSO
{
    public int StaminaBase, StaminaMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        int newStamina = StaminaBase + StaminaMultiplier * modifierLevel;
        aPlayer.playerData.StaminaRecoveryRate = newStamina;

        UpdateDescription();
    }
    public override void UpdateDescription()
    {

        modifierDescription = "StaminaBase Recovery Rate is " + (StaminaBase + StaminaMultiplier * modifierLevel) + ".";
    }

}
