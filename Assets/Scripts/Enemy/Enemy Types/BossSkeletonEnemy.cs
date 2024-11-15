
using UnityEngine;

public class BossSkeletonEnemy : Enemy
{
    [SerializeField]
    LayerMask playerLayer;
    public bool IsBlocking = true;

    [SerializeField] BossSkeletonSoundData BossSoundData;

    [SerializeField]
    ParticleSystem DustPs;
    public override void Attack()
    {
        base.Attack();
        if (!IsAttacking || CurrentHealth <1) return;
        DustPs.transform.position = attackPosition.position;
        DustPs.Emit(30);
        IsAttacking = false;
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
        if (!IsAlive || aDamageAmount < 0) return;
        if (IsBlocking)
        {
            //Play Enemy On Block
            animator.SetTrigger("Block");
            IsBlocking = false;
            AudioManager.instance.PlayOneShot(enemySoundData.EnemyBlock, transform.position);
            return;

        }
        if (damageIndicator != null)
        {
            damageIndicator.PopUp(aDamageAmount);
        }
        CurrentHealth -= aDamageAmount;
        if (CurrentHealth > 0)
        {
            AudioManager.instance.PlayOneShot(enemySoundData.EnemyOnHit, transform.position);
        }
        else
        {
            Death();

        }
    }
     */

    internal void PlayWarcrySound()
    {
        if(BossSoundData == null)
        {
            BossSoundData = (BossSkeletonSoundData)enemySoundData;
        }
        if (BossSoundData == null)
        { 
            return; 
        }
        AudioManager.instance.PlayOneShot(BossSoundData.BossWarcry, transform.position);
    }
    internal void PlayRiseSkeletonSound()
    {
        if (BossSoundData == null)
        {
            BossSoundData = (BossSkeletonSoundData)enemySoundData;
        }
        if (BossSoundData == null)
        {
            return;
        }
        AudioManager.instance.PlayOneShot(BossSoundData.RiseSkeleton, transform.position);
    }
    internal void PlayHitGorundSound()
    {
        if (BossSoundData == null)
        {
            BossSoundData = (BossSkeletonSoundData)enemySoundData;
        }
        if (BossSoundData == null)
        {
            return;
        }
        AudioManager.instance.PlayOneShot(BossSoundData.BossHeavyGroundHit, transform.position);
    }

    internal void PlayGruntSlashSound()
    {
        if (BossSoundData == null)
        {
            BossSoundData = (BossSkeletonSoundData)enemySoundData;
        }
        if (BossSoundData == null)
        {
            return;
        }
        AudioManager.instance.PlayOneShot(BossSoundData.EnemyAttack, transform.position);

    }
}

