using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverStaminaOnParryModifier", menuName = "WeaponModifiers/RecoverStaminaOnParryModifier")]
public class RecoverStaminaOnParryModifier : ItemModifierSO, IItemModifier
{
    public int RecoverStaminaOnParryBase, RecoverStaminaOnParryMultiplier;
    int StaminaRecovery;
    PlayerController player;
    public void ApplyModifier(PlayerController aPlayer)
    {
        StaminaRecovery = RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.inventory.equipmentManager.weapon.OnParry -= RecoverStaminaOnParry;
        aPlayer.inventory.equipmentManager.weapon.OnParry += RecoverStaminaOnParry;
        GetDescription();
    }
    public string GetDescription()
    {
        return "Recover " + (RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier * (modifierLevel+1)) + " Stamina  Parrying projectiles";
    }
    public void RecoverStaminaOnParry()
    {
        player.RecoverStamina(StaminaRecovery);
        
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.inventory.equipmentManager.weapon.OnParry -= RecoverStaminaOnParry;
    }
}
