using UnityEngine;

[CreateAssetMenu(fileName = "_FasterDashModifier", menuName = "WeaponModifiers/FasterDashModifier")]
public class FasterDashModifier : WeaponModifierSO
{
    public float ModifiedDashSpeedBase, DashSpeedLevelMultiplier;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        aPlayer.playerStats.dashSpeed.AddModifier(DashSpeedLevelMultiplier * modifierLevel);  
       
        //UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        modifierDescription = "Increase your Dash Speed by " + DashSpeedLevelMultiplier;
    }
}
