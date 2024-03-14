using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy_StateData", menuName = "Enemy State Data")]
public class EnemyStateData : ScriptableObject
{
    public EnemyIdleSOBase idleStateData;
    public EnemyChaseSOBase chaseStateData;
    public EnemyAttackSOBase attackStateData;
    public EnemyFleeSOBase fleeStateData;
    public EnemyKnockBackSOBase knockBackStateData;
    public EnemyDieSOBase dieStateData;
}
