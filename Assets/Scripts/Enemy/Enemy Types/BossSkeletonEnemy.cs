

using UnityEngine;

public class BossSkeletonEnemy : Enemy
{
    [SerializeField]
    LayerMask playerLayer;
    public bool IsBlocking = true;

    public override void Attack()
    {
        base.Attack();
        if (!IsAttacking || CurrentHealth <1) return;

        IsAttacking = false;
        attackPosition.localScale = Vector3.one * currentAttackRange;
        Collider[] HitPlayers = Physics.OverlapSphere(attackPosition.position, currentAttackRange, playerLayer);

        foreach (Collider player in HitPlayers)
        {
            player.GetComponent<IDamageable>().TakeDamage(currentAttackDamage, this.gameObject);
        }
    }

    public override void TakeDamage(int aDamageAmount, GameObject aSource)
    {
        if (!IsAlive || aDamageAmount < 0) return;
        if (IsBlocking)
        {
            //Play Enemy On Block
            animator.SetTrigger("Block");
            IsBlocking = false;
            AudioManager.instance.PlayOneShot(enemySoundData.EnemyBlock, transform.position);
            return;

        }
        CurrentHealth -= aDamageAmount;
        if (CurrentHealth > 0)
        {
            AudioManager.instance.PlayOneShot(enemySoundData.EnemyOnHit, transform.position);
        }
        else
        {
            Death();
            if (owner != null)
            {
                owner.InformEnemyDeath();
            }
        }
    }
}

