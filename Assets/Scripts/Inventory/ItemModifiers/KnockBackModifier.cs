//using UnityEngine;
//[CreateAssetMenu(fileName = "_KnockBackModifier", menuName = "WeaponModifiers/KnockBackOnThirdStrike")]
//public class KnockBackModifier : ItemModifierSO, IItemModifier
//{
//    public int KnockBackForceBase, KnockBackForceMutiplier;
//    int currentKnockbackForce;
//    Transform playerTransform;
//    public void ApplyModifier(PlayerController aPlayer)
//    {
//        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
//        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit -= KnockBackEnemy;
//        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit += KnockBackEnemy;
//        playerTransform = aPlayer.transform; 
//    }

//    public string GetDescription(int aModifierLevel)
//    {  
//        return "Knockback on 3rd " + KnockBackForceBase + KnockBackForceMutiplier * aModifierLevel  + " Force.";
//    }

//    public void KnockBackEnemy(Enemy enemy)
//    {
//        if(enemy == null) {
//            return;
//        }
//        Vector3 force = (enemy.transform.position - playerTransform.position).normalized * currentKnockbackForce;
//        force.y = 0;
//        enemy.GetKnockBack(force);
//    }

//    public void RemoveModifier(PlayerController aPlayer)
//    {
//        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit -= KnockBackEnemy;
//    }
//}
