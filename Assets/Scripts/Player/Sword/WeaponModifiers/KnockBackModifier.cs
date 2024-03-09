using UnityEngine;
[CreateAssetMenu(fileName = "KnockBackModifier", menuName = "WeaponModifiers/Knock Back")]
public class KnockBackModifier : WeaponModifierSO
{
    public int KnockBackForce;
    Transform player;
    public override void ApplyModifier(SwordBase sword)
    {
        sword.OnMeleeHit += KnockBackEnemy;
        player = sword.transform;
    }
    public void KnockBackEnemy(Enemy enemy)
    {
        Vector3 force = (enemy.transform.position - player.position).normalized * KnockBackForce;
        force.y = 0;
        enemy.GetKnockBack(force);
    }
}
