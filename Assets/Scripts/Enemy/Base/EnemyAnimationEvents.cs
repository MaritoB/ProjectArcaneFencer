using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    Enemy enemy;
    MeleeWithShieldEnemy enemyWithShield;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyWithShield = GetComponentInParent<MeleeWithShieldEnemy>();
    }

    public void Attack()
    {
        if (enemy == null) return;
        enemy.Attack();
    }
    public void SpawnRandomEnemy()
    {
        if (enemy == null) return;
        EnemySpawner.Instance.SpawnRandomEnemy();
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
