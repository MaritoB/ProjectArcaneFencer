using UnityEngine;
[CreateAssetMenu(fileName = "_ReduceStaminaOnBlock", menuName = "WeaponModifiers/ReduceStaminaOnBlock")]
public class ReduceStaminaOnBlock : WeaponModifierSO
{
    public float ReduceStaminaOnBlockBase, ReduceStaminaOnBlockMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        float newValue = ReduceStaminaOnBlockBase + ReduceStaminaOnBlockMultiplier * modifierLevel;
        aPlayer.playerData.StaminaDrainPercentajeOnBlock = newValue;

    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        modifierDescription = "Use less Stamina to Block damage ("+ (ReduceStaminaOnBlockBase + ReduceStaminaOnBlockMultiplier * (modifierLevel + 1)) + "%).";
    }
}
