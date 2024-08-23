using UnityEngine;
[CreateAssetMenu(fileName = "_KnockbackOnBlock", menuName = "WeaponModifiers/KnockbackOnBlock")]
public class KnockbackOnBlock : ItemModifierSO, IItemModifier
{
    public int KnockBackForceBase, KnockBackForceMutiplier;
    int currentKnockbackForce;
    Transform playerTransform;
    public void ApplyModifier(PlayerController aPlayer)
    {
        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
        aPlayer.OnBlockPerformed -= KnockBackEnemy;
        aPlayer.OnBlockPerformed += KnockBackEnemy;
        playerTransform = aPlayer.transform;
         GetDescription();
    }
    public string GetDescription()
    {
        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
        return "Knockback enemy that hit your Magic Shield with " + currentKnockbackForce + " Force.";
    }

    public void KnockBackEnemy(Enemy enemy)
    {
        if (enemy == null)
        {
            return;
        }
        Vector3 force = (enemy.transform.position - playerTransform.position).normalized * currentKnockbackForce;
        force.y = 0;
        enemy.GetKnockBack(force);
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.OnBlockPerformed -= KnockBackEnemy;
    }
}
