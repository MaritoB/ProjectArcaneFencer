using UnityEngine;

[CreateAssetMenu(fileName = "_ChannelingDashModifier", menuName = "WeaponModifiers/Channeling Dash")]
public class ChannelingDash : WeaponModifierSO
{
    public float ModifiedDashSpeedBase,DashSpeedLevelMultiplier, ModifiedDashTimeBase, DashTimeLevelMultiplier, DashCostLevelMultiplier;
    public int ModifiedDashStaminaCostbase;
    public bool isChannellingDash;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        aPlayer.playerData.DashSpeed = ModifiedDashSpeedBase  +  DashSpeedLevelMultiplier * modifierLevel;
        aPlayer.playerData.DashTime = ModifiedDashTimeBase + DashTimeLevelMultiplier * modifierLevel;
        aPlayer.playerData.DashStaminaCost  = ModifiedDashStaminaCostbase +(int) DashCostLevelMultiplier*modifierLevel;
        aPlayer.DashChanellingPerk = isChannellingDash;
       
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        string desc = isChannellingDash ? "Your Dash is now Chanelling" : "Your Dash is no longer Chanelling";
        desc += " the Stamina cost is " +( ModifiedDashStaminaCostbase + (int)DashCostLevelMultiplier * modifierLevel)+
            " , Speed is " +( ModifiedDashSpeedBase + DashSpeedLevelMultiplier * modifierLevel )+
            ", and DashTime is " + (ModifiedDashTimeBase + DashTimeLevelMultiplier * modifierLevel) + ".";
    }
}
