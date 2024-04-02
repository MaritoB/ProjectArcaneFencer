using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy_StateData", menuName = "Enemy State Data")]
public class EnemyStateData : ScriptableObject
{
    public EnemyIdleSOBase idleStateData;
    public EnemyChaseSOBase chaseStateData;
    public EnemyChaseSOBase chaseRunForSecondsStateData;
    public EnemyAttackSOBase attackStateData;
    public EnemyFleeSOBase fleeStateData;
    public EnemyStunSOBase knockBackStateData;
    public EnemyStunSOBase hitStunStateData;
    public EnemyDieSOBase dieStateData;
}
