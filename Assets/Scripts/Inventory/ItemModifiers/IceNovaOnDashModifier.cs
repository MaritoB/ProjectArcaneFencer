//using UnityEngine;

//[CreateAssetMenu(fileName = "_IceNovaOnDashModifier", menuName = "WeaponModifiers/IceNovaOnDashModifier")]
//public class IceNovaOnDashModifier : ItemModifierSO, IItemModifier
//{
//    public int TriggerChanceBase, TriggerChanceMultiplier;
//    private int currentTriggerChance;
//    private PlayerController player;

//    public void ApplyModifier(PlayerController aPlayer)
//    {
//        RemoveModifier(player);
//        player = aPlayer;
//        player.OnDash += TryCastIceNova;
//        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel; 
//    }

//    public string GetDescription(int aModifierLevel)
//    {
//        return  (TriggerChanceBase + TriggerChanceMultiplier * (aModifierLevel )) + "  chance to cast Ice Nova on Dash.";
//    }

//    private void TryCastIceNova()
//    {
//        if (player == null) return;

//        if (Random.Range(0, 100) < currentTriggerChance)
//        {
//            player.mSkillManager.UseIceNova();
//        }
//    }

//    public void RemoveModifier(PlayerController aPlayer)
//    {
//        if (player != null && aPlayer == player)
//        {
//            aPlayer.OnDash -= TryCastIceNova;
//            player = null;
//        }
//    }
//}
