using UnityEngine;
using FMODUnity;
[CreateAssetMenu(fileName = "_EnemySoundData", menuName = "Enemy Sound Data")]
public class EnemySoundData : ScriptableObject
{
    public EventReference EnemyAttack, EnemyDeath, EnemyOnHit, EnemyWalk, EnemyBlock;
}
