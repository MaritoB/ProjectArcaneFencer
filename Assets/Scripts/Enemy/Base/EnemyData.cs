using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public int soulsAmount;
    public float attackRange;
    public int attackDamage;
    public float AttackRate;
}
