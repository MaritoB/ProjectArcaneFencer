using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void TriggerSwingAttack()
    {
        animator.SetTrigger("SwingAttack");
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable enemy = other.GetComponent<IDamageable>();
        if (enemy == null) return;
        enemy.TakeDamage(10);
    }
}
