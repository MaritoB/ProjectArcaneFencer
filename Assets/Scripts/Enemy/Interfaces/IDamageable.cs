using UnityEngine;

public interface IDamageable
{
   // void TakeDamage(int aDamageAmount, GameObject aSource);
    void TakeDamage(AttackInfo aAttackInfo);
    void Death();
    float CurrentHealth { get; set; }

}