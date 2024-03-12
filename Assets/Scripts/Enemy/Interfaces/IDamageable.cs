public interface IDamageable
{
    void TakeDamage(int aDamageAmount);
    void Death();
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

}