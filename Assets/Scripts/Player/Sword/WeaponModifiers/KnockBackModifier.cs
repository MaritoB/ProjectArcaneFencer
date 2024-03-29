using UnityEngine;
[CreateAssetMenu(fileName = "_KnockBackModifier", menuName = "WeaponModifiers/KnockBackOnThirdStrike")]
public class KnockBackModifier : WeaponModifierSO
{
    public int KnockBackForce;
    Transform playerTransform;
    public override void ApplyModifier(PlayerController aPlayer)
    {
        aPlayer.sword.OnThirdMeleeHit += KnockBackEnemy;
        playerTransform = aPlayer.transform;
    }
    public void KnockBackEnemy(Enemy enemy)
    {
        Vector3 force = (enemy.transform.position - playerTransform.position).normalized * KnockBackForce;
        force.y = 0;
        enemy.GetKnockBack(force);
    }
}
