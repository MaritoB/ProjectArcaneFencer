using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void Attack()
    {
        if (enemy == null) return;
        enemy.Attack();
    }
    public void FinishAttack()
    {
        if (enemy == null) return;
        enemy.IsAttacking = false;
        enemy.CanMove = true;
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
