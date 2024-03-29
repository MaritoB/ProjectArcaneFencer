using UnityEngine;
[CreateAssetMenu(fileName = "_StaminaRecoveryRate", menuName = "WeaponModifiers/StaminaRecoveryRate")]
public class StaminaRecoveryRate : WeaponModifierSO
{
    public int StaminaBase, StaminaMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        int newStamina = StaminaBase + StaminaMultiplier * modifierLevel;
        aPlayer.playerData.StaminaRecoveryRate = newStamina;
        modifierDescription = "StaminaBase Recovery Rate is " + newStamina + ".";
    }
}
