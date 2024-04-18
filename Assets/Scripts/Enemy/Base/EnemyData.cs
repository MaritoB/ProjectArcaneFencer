using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealthBase;
    public int soulsAmountBase;
    public float attackRangeBase;
    public int attackDamageBase;
    public float AttackRateBase;

    public float maxHealthMultiplier;
    public int soulsAmountMultiplier;
    public float attackRangeMultiplier;
    public int attackDamageMultiplier;
    public float attackRateMultiplier; 
}
