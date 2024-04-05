using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int aDamageAmount, GameObject aSource);
    void Death();
    float CurrentHealth { get; set; }

}