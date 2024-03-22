

using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    float attackRange = 2f;
    [SerializeField]
    int attackDamage = 10;
    [SerializeField]
    LayerMask playerLayer;
    public override void Attack()
    {
        base.Attack();
        //IsAttacking = false;
        attackPosition.localScale = Vector3.one * attackRange;
        Collider[] HitPlayers = Physics.OverlapSphere(attackPosition.position, attackRange, playerLayer);

        foreach (Collider player in HitPlayers)
        {
            player.GetComponent<IDamageable>().TakeDamage(attackDamage);
        }
    }

}

