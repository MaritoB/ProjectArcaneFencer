using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverLifeOnBlockModifier", menuName = "WeaponModifiers/RecoverLifeOnBlockModifier")]
public class RecoverLifeOnBlockModifier : ItemModifierSO, IItemModifier
{
    public int RecoverLifeOnBlockBase, RecoverLifeOnBlockMultiplier;
    int LifeRecovery;
    PlayerController player;
    public void ApplyModifier(PlayerController aPlayer)
    {
        LifeRecovery = RecoverLifeOnBlockBase + RecoverLifeOnBlockMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.OnBlockPerformed -= RecoverLifeOnBlock;
        aPlayer.OnBlockPerformed += RecoverLifeOnBlock;

        GetDescription();
    }
    public string GetDescription()
    {
        return "Recover " + (RecoverLifeOnBlockBase + RecoverLifeOnBlockMultiplier * (modifierLevel+1)) + " HP Blocking Melee Damage";
    }
    public void RecoverLifeOnBlock(Enemy aEnemy)
    {
        player.RecoverLife(LifeRecovery);
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.OnBlockPerformed -= RecoverLifeOnBlock;
    }
}
