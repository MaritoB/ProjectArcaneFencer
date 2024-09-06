//using UnityEngine;
//[CreateAssetMenu(fileName = "_KnockbackOnBlock", menuName = "WeaponModifiers/KnockbackOnBlock")]
//public class KnockbackOnBlock : ItemModifierSO, IItemModifier
//{
//    public int KnockBackForceBase, KnockBackForceMutiplier;
//    int currentKnockbackForce;
//    Transform playerTransform;
//    public void ApplyModifier(PlayerController aPlayer)
//    {
//        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
//        aPlayer.OnBlockPerformed -= KnockBackEnemy;
//        aPlayer.OnBlockPerformed += KnockBackEnemy;
//        playerTransform = aPlayer.transform; 
//    }
//    public string GetDescription(int aModifierLevel)
//    { 
//        return "Knockback on Block " + KnockBackForceBase + KnockBackForceMutiplier * aModifierLevel + " Force.";
//    }

//    public void KnockBackEnemy(Enemy enemy)
//    {
//        if (enemy == null)
//        {
//            return;
//        }
//        Vector3 force = (enemy.transform.position - playerTransform.position).normalized * currentKnockbackForce;
//        force.y = 0;
//        enemy.GetKnockBack(force);
//    }

//    public void RemoveModifier(PlayerController aPlayer)
//    {
//        aPlayer.OnBlockPerformed -= KnockBackEnemy;
//    }
//}
