using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverLifeOnDashModifier", menuName = "WeaponModifiers/RecoverLifeOnDashModifier")]
public class RecoverLifeOnDashModifier : WeaponModifierSO
{
    public int RecoverLifeBase, RecoverLifeMultiplier;
    int LifeRecovery;
    PlayerController player;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        LifeRecovery = RecoverLifeBase + RecoverLifeMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.OnDash -= RecoverLifeOnParry;
        aPlayer.OnDash += RecoverLifeOnParry;

        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        LifeRecovery = RecoverLifeBase + RecoverLifeMultiplier * modifierLevel;
        modifierDescription = "Recover " + LifeRecovery + " HP on Dash";
    }
    public void RecoverLifeOnParry()
    {
        player.RecoverLife(LifeRecovery);
        
    }
}
