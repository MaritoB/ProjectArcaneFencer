
using UnityEngine;

public class BossMageSkeletonEnemy : Enemy
{
    [SerializeField]
    LayerMask playerLayer;
    public bool IsBlocking = true;

    [SerializeField] BossSkeletonSoundData BossSoundData;

    [SerializeField]
    ParticleSystem DustPs;
    [SerializeField]
    private ProjectileSpawner projectileSpawner;
    
    public override void Attack()
    {
        base.Attack();
        if (!IsAttacking || CurrentHealth <1) return;
        IsAttacking = false;
        if (projectileSpawner != null)
        {
            projectileSpawner.ShootProjectileForwardFromPool();
        }
        /*
        DustPs.transform.position = attackPosition.position;
        DustPs.Emit(30);
        IsAttacking = false;
        attackPosition.localScale = Vector3.one * currentAttackRange;
        Collider[] HitPlayers = Physics.OverlapSphere(attackPosition.position, currentAttackRange, playerLayer);

        foreach (Collider player in HitPlayers)
        {
            player.GetComponent<IDamageable>().TakeDamage(currentAttackDamage, this.gameObject);
        }
         */
    }

    public override void TakeDamage(int aDamageAmount, GameObject aSource)
    {
        if (!IsAlive || aDamageAmount < 0) return;
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
