using UnityEngine;

[CreateAssetMenu(fileName = "_DashFreedomModifier", menuName = "WeaponModifiers/DashFreedomModifier")]
public class DashFreedomModifier : ItemModifierSO, IItemModifier
{
    public float  ModifiedDashTimeBase, DashTimeLevelMultiplier, DashCostLevelMultiplier;
    public int ModifiedDashStaminaCostbase;
    public void ApplyModifier(PlayerController aPlayer)
    {
        aPlayer.playerStats.dashTime.AddModifier(DashTimeLevelMultiplier * modifierLevel);
        aPlayer.playerStats.dashStaminaCost.AddModifier(DashCostLevelMultiplier * modifierLevel);
        //aPlayer.playerData.DashTime = ModifiedDashTimeBase + DashTimeLevelMultiplier * modifierLevel;
        //aPlayer.playerData.DashStaminaCost  = ModifiedDashStaminaCostbase +(int) DashCostLevelMultiplier*modifierLevel;
        aPlayer.DashChanellingPerk = true;
       
        //UpdateDescription();
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.DashChanellingPerk = false;
        aPlayer.playerStats.dashStaminaCost.RemoveModifier(DashCostLevelMultiplier * modifierLevel);
        aPlayer.playerStats.dashTime.RemoveModifier(DashTimeLevelMultiplier * modifierLevel);
    }

    public string GetDescription()
    {

        return "Hold Dash to keep dashing. Decrease Dash Stamina cost by "+ DashCostLevelMultiplier + " & Dash Duration by " + DashTimeLevelMultiplier;
    }
}