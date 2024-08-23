using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverLifeOnDashModifier", menuName = "WeaponModifiers/RecoverLifeOnDashModifier")]
public class RecoverLifeOnDashModifier : ItemModifierSO, IItemModifier
{
    public int RecoverLifeBase, RecoverLifeMultiplier;
    int LifeRecovery;
    PlayerController player;
    public void ApplyModifier(PlayerController aPlayer)
    {
        LifeRecovery = RecoverLifeBase + RecoverLifeMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.OnDash -= RecoverLifeOnParry;
        aPlayer.OnDash += RecoverLifeOnParry;

        GetDescription();
    }
    public string GetDescription()
    {
        return "Recover " + (RecoverLifeBase + RecoverLifeMultiplier * (modifierLevel+1)) + " HP on Dash";
    }
    public void RecoverLifeOnParry()
    {
        player.RecoverLife(LifeRecovery);
        
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.OnDash -= RecoverLifeOnParry;
    }
}
