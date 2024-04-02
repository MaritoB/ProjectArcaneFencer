using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverLifeOnParryModifier", menuName = "WeaponModifiers/RecoverLifeOnParryModifier")]
public class RecoverLifeOnParryModifier : WeaponModifierSO
{
    public int RecoverLifeOnParryBase, RecoverLifeOnParryMultiplier;
    int LifeRecovery;
    PlayerController player;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        LifeRecovery = RecoverLifeOnParryBase + RecoverLifeOnParryMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.OnParry -= RecoverLifeOnParry;
        aPlayer.OnParry += RecoverLifeOnParry;

        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        LifeRecovery = RecoverLifeOnParryBase + RecoverLifeOnParryMultiplier * modifierLevel;
        modifierDescription = "Recover " + LifeRecovery + " Life  Parrying projectiles";
    }
    public void RecoverLifeOnParry()
    {
        Debug.Log("Heal on Parry");
        player.Heal(LifeRecovery);
        player.UpdateStaminaUI();
        
    }
}
