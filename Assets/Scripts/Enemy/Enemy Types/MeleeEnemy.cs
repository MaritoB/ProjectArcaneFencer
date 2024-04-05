

using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    LayerMask playerLayer;
    public override void Attack()
    {
        base.Attack();
        if (!IsAttacking || CurrentHealth <1) return;

        IsAttacking = false;
        IsUnstopable = false;
        attackPosition.localScale = Vector3.one * enemyData.attackRange;
        Collider[] HitPlayers = Physics.OverlapSphere(attackPosition.position, enemyData.attackRange, playerLayer);

        foreach (Collider player in HitPlayers)
        {
            player.GetComponent<IDamageable>().TakeDamage(enemyData.attackDamage, this.gameObject);
        }
    }

}

