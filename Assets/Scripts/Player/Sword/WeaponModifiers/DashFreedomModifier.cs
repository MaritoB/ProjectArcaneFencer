using UnityEngine;

[CreateAssetMenu(fileName = "_DashFreedomModifier", menuName = "WeaponModifiers/DashFreedomModifier")]
public class DashFreedomModifier : WeaponModifierSO
{
    public float  ModifiedDashTimeBase, DashTimeLevelMultiplier, DashCostLevelMultiplier;
    public int ModifiedDashStaminaCostbase;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        aPlayer.playerData.DashTime = ModifiedDashTimeBase + DashTimeLevelMultiplier * modifierLevel;
        aPlayer.playerData.DashStaminaCost  = ModifiedDashStaminaCostbase +(int) DashCostLevelMultiplier*modifierLevel;
        aPlayer.DashChanellingPerk = true;
       
        //UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription(); 
        modifierDescription = "Hold Dash to keep dashing. Decrease Dash Stamina cost by "+ DashCostLevelMultiplier + " & Dash Duration by " + DashTimeLevelMultiplier;
    }
}
