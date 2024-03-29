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
        aPlayer.playerData.DashTime = ModifiedDashTimeBase;
        aPlayer.playerData.DashStaminaCost  = ModifiedDashStaminaCostbase +(int) DashCostLevelMultiplier*modifierLevel;
        aPlayer.DashChanellingPerk = isChannellingDash;
        string desc = isChannellingDash? "Your Dash is now Chanelling": "Your Dash is no longer Chanelling";
        desc +=" the Stamina cost is "+aPlayer.playerData.DashStaminaCost.ToString() + " , Speed is "+aPlayer.playerData.DashSpeed.ToString() + ", and DashTime is "+aPlayer.playerData.DashTime.ToString() + ".";
        modifierDescription = desc;
    }


}
