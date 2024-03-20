using UnityEngine;
[CreateAssetMenu(fileName = "KnockBackModifier", menuName = "WeaponModifiers/KnockBackOnThirdStrike")]
public class KnockBackModifier : WeaponModifierSO
{
    public int KnockBackForce;
    Transform player;
    public override void ApplyModifier(SwordBase sword)
    {
        sword.OnThirdMeleeHit += KnockBackEnemy;
        player = sword.transform;
    }
    public void KnockBackEnemy(Enemy enemy)
    {
        Vector3 force = (enemy.transform.position - player.position).normalized * KnockBackForce;
        force.y = 0;
        enemy.GetKnockBack(force);
    }
}
