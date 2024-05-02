using UnityEngine;

[CreateAssetMenu(fileName = "_FasterDashModifier", menuName = "WeaponModifiers/FasterDashModifier")]
public class FasterDashModifier : WeaponModifierSO
{
    public float ModifiedDashSpeedBase, DashSpeedLevelMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        aPlayer.playerData.DashSpeed = ModifiedDashSpeedBase  +  DashSpeedLevelMultiplier * modifierLevel;
       
        //UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        modifierDescription = "Increase your Dash Speed by " + DashSpeedLevelMultiplier;
    }
}
