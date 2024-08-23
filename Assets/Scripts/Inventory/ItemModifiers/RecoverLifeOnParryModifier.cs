using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverLifeOnParryModifier", menuName = "WeaponModifiers/RecoverLifeOnParryModifier")]
public class RecoverLifeOnParryModifier : ItemModifierSO, IItemModifier
{
    public int RecoverLifeOnParryBase, RecoverLifeOnParryMultiplier;
    int LifeRecovery;
    PlayerController player;
    public void ApplyModifier(PlayerController aPlayer)
    {
        LifeRecovery = RecoverLifeOnParryBase + RecoverLifeOnParryMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.inventory.equipmentManager.weapon.OnParry -= RecoverLifeOnParry;
        aPlayer.inventory.equipmentManager.weapon.OnParry += RecoverLifeOnParry;

        GetDescription();
    }
    public string GetDescription()
    {
        return "Recover " + (RecoverLifeOnParryBase + RecoverLifeOnParryMultiplier * (modifierLevel+1)) + " HP Parrying projectiles";
    }
    public void RecoverLifeOnParry()
    {
        player.RecoverLife(LifeRecovery);
        
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.inventory.equipmentManager.weapon.OnParry -= RecoverLifeOnParry;
    }
}
