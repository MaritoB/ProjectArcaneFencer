

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
        attackPosition.localScale = Vector3.one * currentAttackRange;
        Collider[] HitPlayers = Physics.OverlapSphere(attackPosition.position, currentAttackRange, playerLayer);

        foreach (Collider player in HitPlayers)
        {
            player.GetComponent<IDamageable>().TakeDamage(currentAttackDamage, this.gameObject);
        }
    }

}

