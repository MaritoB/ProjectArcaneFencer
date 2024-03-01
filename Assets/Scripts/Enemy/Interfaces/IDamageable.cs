public interface IDamageable
{
    void TakeDamage(int aDamageAmount);
    void Die();
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

}