
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

