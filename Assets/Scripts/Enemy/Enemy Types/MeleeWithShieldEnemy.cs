

using UnityEngine;

public class MeleeWithShieldEnemy : Enemy
{
    [SerializeField]
    LayerMask playerLayer;
    public bool IsBlocking = true;
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
            player.GetComponent<IDamageable>().
                TakeDamage(attackInfo);
        }
    }
    public override void TakeDamage(AttackInfo aAttackInfo)
    {
        if (IsBlocking)
        {
            //Play Enemy On Block
            animator.SetTrigger("Block");
            IsBlocking = false;
            PopupTextPool.Instance.ShowPopup(transform.position, "Blocked!", false, false);
            AudioManager.instance.PlayOneShot(enemySoundData.EnemyBlock, transform.position);
            return;
        }
        base.TakeDamage(aAttackInfo); 
    }
    /*
    public override void TakeDamage(int aDamageAmount, GameObject aSource)
    {
        if (IsBlocking)
        {
            //Play Enemy On Block
            damageIndicator.PopUp(0);
            animator.SetTrigger("Block");
            IsBlocking = false;
            AudioManager.instance.PlayOneShot(enemySoundData.EnemyBlock, transform.position);
            return;

        }
        if (!IsAlive || aDamageAmount < 0) return;

 
        CurrentHealth -= aDamageAmount;
        if (CurrentHealth > 0)
        {

            AudioManager.instance.PlayOneShot(enemySoundData.EnemyOnHit, transform.position);
            if (!IsUnstopable)
            {
                HitStun();
                IsAttacking = false;
            }
        }
        else
        {
            Death();

        }
    }
     */
}

