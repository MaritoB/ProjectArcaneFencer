using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
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
}
