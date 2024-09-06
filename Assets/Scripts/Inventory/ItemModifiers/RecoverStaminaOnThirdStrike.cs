//using UnityEngine;

//[CreateAssetMenu(fileName = "_RecoverStaminaOnThirdStrike", menuName = "WeaponModifiers/RecoverStaminaOnThirdStrike")]
//public class RecoverStaminaOnThirdStrike : ItemModifierSO, IItemModifier
//{
//    public int RecoverStaminaOnParryBase, RecoverStaminaOnParryMultiplier;
//    int StaminaRecovery;
//    PlayerController player;
//    public void ApplyModifier(PlayerController aPlayer)
//    {
//        StaminaRecovery = RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier * modifierLevel;
//        player = aPlayer;
//        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit -= RecoverStaminaOnParry;
//        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit += RecoverStaminaOnParry; 
//    }
//    public string GetDescription(int aModifierLevel)
//    {
//        return "Recover " + (RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier *(aModifierLevel  )) + " Stamina per enemy hitted with your Third Attack";
//    }
//    public void RecoverStaminaOnParry(Enemy enemy)
//    {
//        player.RecoverStamina(StaminaRecovery);

//    }

//    public void RemoveModifier(PlayerController aPlayer)
//    {
//        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit -= RecoverStaminaOnParry;
//    }
//}

