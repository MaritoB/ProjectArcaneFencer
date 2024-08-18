using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverStaminaOnParryModifier", menuName = "WeaponModifiers/RecoverStaminaOnParryModifier")]
public class RecoverStaminaOnParryModifier : WeaponModifierSO
{
    public int RecoverStaminaOnParryBase, RecoverStaminaOnParryMultiplier;
    int StaminaRecovery;
    PlayerController player;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        StaminaRecovery = RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.inventory.equipmentManager.weapon.OnParry -= RecoverStaminaOnParry;
        aPlayer.inventory.equipmentManager.weapon.OnParry += RecoverStaminaOnParry;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        modifierDescription = "Recover " + (RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier * (modifierLevel+1)) + " Stamina  Parrying projectiles";
    }
    public void RecoverStaminaOnParry()
    {
        player.RecoverStamina(StaminaRecovery);
        
    }
}
