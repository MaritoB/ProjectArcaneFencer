using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverStaminaOnParryModifier", menuName = "WeaponModifiers/RecoverStaminaOnParryModifier")]
public class RecoverLifeOnParryModifierRecoverStaminaOnParryModifier : WeaponModifierSO
{
    public int RecoverStaminaOnParryBase, RecoverStaminaOnParryMultiplier;
    int StaminaRecovery;
    PlayerController player;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        StaminaRecovery = RecoverStaminaOnParryBase + RecoverStaminaOnParryMultiplier * modifierLevel;
        modifierDescription = "Recover " + StaminaRecovery + " Stamina  Parrying projectiles";
        player = aPlayer;
        aPlayer.OnParry -= RecoverStaminaOnParry;
        aPlayer.OnParry += RecoverStaminaOnParry;
        ;

    }
    public void RecoverStaminaOnParry()
    {
        Debug.Log("Stamina on Parry");
        player.RecoverStamina(StaminaRecovery);
        
    }
}
