
//using UnityEngine;
//[CreateAssetMenu(fileName = "_IceNovaOnSecondStrike", menuName = "WeaponModifiers/IceNovaOnSecondStrike")]
//public class IceNovaOnSecondStrike : ItemModifierSO, IItemModifier
//{
//    public int TriggerChanceBase, TriggerChanceMultiplier;
//    int currentTriggerChance;
//    PlayerController player;

//    public void ApplyModifier(PlayerController aPlayer)
//    {
//        RemoveModifier(player);
//        player = aPlayer;
//        aPlayer.inventory.equipmentManager.weapon.OnSecondMeleePerformed += TryCastIceNova;
//        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel; 
//    }
//    public string GetDescription(int aModifierLevel)
//    {
//        return   (TriggerChanceBase + TriggerChanceMultiplier * (aModifierLevel)) + "% chance to cast Ice Nova on 2nd. ";
//    }
//    public void TryCastIceNova()
//    {
//        if (player == null) return;
//        int number = Random.Range(0, 100);
//        if (number < currentTriggerChance)
//        {
//            player.mSkillManager.UseIceNova();
//        }

//    }

//    public void RemoveModifier(PlayerController aPlayer)
//    {
//        if (player != null && aPlayer == player)
//        {
//            player.inventory.equipmentManager.weapon.OnSecondMeleePerformed -= TryCastIceNova;
//            player = null;
//        }
//    }
//}

