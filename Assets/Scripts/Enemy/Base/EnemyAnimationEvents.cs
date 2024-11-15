using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    Enemy enemy;
    MeleeWithShieldEnemy enemyWithShield;
    BossSkeletonEnemy BossSkeleton;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyWithShield = GetComponentInParent<MeleeWithShieldEnemy>();
        BossSkeleton = GetComponentInParent<BossSkeletonEnemy>();
    }

    public void Attack()
    {
        if (enemy == null) return;
        enemy.Attack();
    }
    public void SpawnRandomEnemy()
    {
        EnemySpawner.Instance.SpawnRandomEnemy();
    }
    public void DeathFinishEvent()
    {
        if (enemy == null) return;
        enemy.InformDeathToOwner();
    }
    public void PlayDeathSoundEvent()
    {
        if (enemy == null) return;
        enemy.PlayDeathSound();
    }
    public void PlayBossWarcrySoundEvent()
    {
        if (BossSkeleton == null) return;
        BossSkeleton.PlayWarcrySound();

    }
    public void PlayRiseSkeletonSoundEvent()
    {
        if (BossSkeleton == null) return;
        BossSkeleton.PlayRiseSkeletonSound();

    }
    public void PlayBossHitGroundEvent()
    {
        if (BossSkeleton == null) return;
        BossSkeleton.PlayHitGorundSound();

    }

    public void PlayBossGruntSlashEvent()
    {
        if (BossSkeleton == null) return;
        BossSkeleton.PlayGruntSlashSound();

    }
    public void FinishHitStun()
    {
        if (enemy == null) return;
        enemy.SetStateToChase();
        enemy.IsAttacking = false;
        if (enemy.IsAlive)
        {
            enemy.CanMove = true;
        }
    }
    public void StartBlocking()
    {
        if (enemyWithShield == null) return;
        enemyWithShield.IsBlocking = true;

    }
    public void PlayAttackSoundEvent()
    {
        if (enemy == null) return;
        enemy.PlayEnemyAttackSound();
    }
    public void PlayFootStepSoundEvent()
    {
        if (enemy == null) return;
        enemy.PlayEnemyFootStepSound();
    }
    public void FinishAttack()
    {
        if (enemy == null) return;
        enemy.IsAttacking = false;
        if (enemy.IsAlive)
        {
           enemy.CanMove = true;
        }
        enemy.SetStateToChase();
    }
    public void ResetCanMove()
    {
        enemy.CanMove = true;
    }
    public void SetStateToIdel()
    {
        if (enemy == null) return;
        enemy.SetStateToIdle();
    }
    public void SetStateToChase()
    {
        if (enemy == null) return;
        enemy.SetStateToChase();
    }
    public void DashForwardEvent(int aDashForce)
    {
        if (enemy == null) return;
        enemy.DashForward(aDashForce);
    }

        public void StopMovementEvent()
    {
        if (enemy == null) return;
        enemy.StopMovement();
    }
}
