using UnityEngine;
[CreateAssetMenu(fileName = "_KnockbackOnBlock", menuName = "WeaponModifiers/KnockbackOnBlock")]
public class KnockbackOnBlock : WeaponModifierSO
{
    public int KnockBackForceBase, KnockBackForceMutiplier;
    int currentKnockbackForce;
    Transform playerTransform;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
        aPlayer.OnBlockPerformed -= KnockBackEnemy;
        aPlayer.OnBlockPerformed += KnockBackEnemy;
        playerTransform = aPlayer.transform;
         UpdateDescription();
    }
    public override void UpdateDescription()
    {
        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
        modifierDescription = "Knockback enemy that hit your Magic Shield with " + currentKnockbackForce + " Force.";
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
}
