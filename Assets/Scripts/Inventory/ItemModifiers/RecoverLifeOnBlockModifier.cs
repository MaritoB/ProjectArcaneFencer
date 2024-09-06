//using UnityEngine;
//[CreateAssetMenu(fileName = "_RecoverLifeOnBlockModifier", menuName = "WeaponModifiers/RecoverLifeOnBlockModifier")]
//public class RecoverLifeOnBlockModifier : ItemModifierSO, IItemModifier
//{
//    public int RecoverLifeOnBlockBase, RecoverLifeOnBlockMultiplier;
//    int LifeRecovery;
//    PlayerController player;
//    public void ApplyModifier(PlayerController aPlayer)
//    {
//        LifeRecovery = RecoverLifeOnBlockBase + RecoverLifeOnBlockMultiplier * modifierLevel;
//        player = aPlayer;
//        aPlayer.OnBlockPerformed -= RecoverLifeOnBlock;
//        aPlayer.OnBlockPerformed += RecoverLifeOnBlock;
         
//    }
//    public string GetDescription(int aModifierLevel)
//    {
//        return "Recover " + (RecoverLifeOnBlockBase + RecoverLifeOnBlockMultiplier * (aModifierLevel  )) + " HP Blocking Melee Damage";
//    }
//    public void RecoverLifeOnBlock(Enemy aEnemy)
//    {
//        player.RecoverLife(LifeRecovery);
//    }

//    public void RemoveModifier(PlayerController aPlayer)
//    {
//        aPlayer.OnBlockPerformed -= RecoverLifeOnBlock;
//    }
//}
