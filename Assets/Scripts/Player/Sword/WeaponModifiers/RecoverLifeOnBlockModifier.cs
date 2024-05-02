using UnityEngine;
[CreateAssetMenu(fileName = "_RecoverLifeOnBlockModifier", menuName = "WeaponModifiers/RecoverLifeOnBlockModifier")]
public class RecoverLifeOnBlockModifier : WeaponModifierSO
{
    public int RecoverLifeOnBlockBase, RecoverLifeOnBlockMultiplier;
    int LifeRecovery;
    PlayerController player;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        LifeRecovery = RecoverLifeOnBlockBase + RecoverLifeOnBlockMultiplier * modifierLevel;
        player = aPlayer;
        aPlayer.OnBlockPerformed -= RecoverLifeOnBlock;
        aPlayer.OnBlockPerformed += RecoverLifeOnBlock;

        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        modifierDescription = "Recover " + (RecoverLifeOnBlockBase + RecoverLifeOnBlockMultiplier * (modifierLevel+1)) + " HP Blocking Damage";
    }
    public void RecoverLifeOnBlock(Enemy aEnemy)
    {
        player.RecoverLife(LifeRecovery);
    }
}
