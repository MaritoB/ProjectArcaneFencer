public interface IDamageable
{
    void TakeDamage(int aDamageAmount);
    void Death();
    float CurrentHealth { get; set; }

}