using UnityEngine;
[CreateAssetMenu(fileName = "_KnockBackModifier", menuName = "WeaponModifiers/KnockBackOnThirdStrike")]
public class KnockBackModifier : ItemModifierSO, IItemModifier
{
    public int KnockBackForceBase, KnockBackForceMutiplier;
    int currentKnockbackForce;
    Transform playerTransform;
    public void ApplyModifier(PlayerController aPlayer)
    {
        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit -= KnockBackEnemy;
        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit += KnockBackEnemy;
        playerTransform = aPlayer.transform;
        GetDescription();
    }

    public string GetDescription()
    { 
        currentKnockbackForce = KnockBackForceBase + KnockBackForceMutiplier * modifierLevel;
        return "Your Third Attack Knockback enemys with " + currentKnockbackForce + " Force.";
    }

    public void KnockBackEnemy(Enemy enemy)
    {
        if(enemy == null) {
            return;
        }
        Vector3 force = (enemy.transform.position - playerTransform.position).normalized * currentKnockbackForce;
        force.y = 0;
        enemy.GetKnockBack(force);
    }

    public void RemoveModifier(PlayerController aPlayer)
    {
        aPlayer.inventory.equipmentManager.weapon.OnThirdMeleeHit -= KnockBackEnemy;
    }
}
