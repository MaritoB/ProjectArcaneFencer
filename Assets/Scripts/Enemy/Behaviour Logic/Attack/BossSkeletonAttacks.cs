
using UnityEngine;


[CreateAssetMenu(fileName = "BossSkeletonAttacks", menuName = "Enemy Logic/Attack Logic/BossSkeletonAttacks ")]
public class BossSkeletonAttacks : EnemyAttackSOBase
{
    [SerializeField] float MaxTimeToAttack;
    [SerializeField] int MaxAttacksTypes;
    [SerializeField] float StrafeSpeed;
    Vector3 strafeDirection;

    float TimeToAttack;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        TimeToAttack = Random.Range(0, MaxTimeToAttack);
        strafeDirection = Vector3.zero;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.IsUnstopable = true;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        enemy.AimPlayerPosition();
       // enemy.MoveEnemy(Vector3.zero);
        if (enemy.IsAttacking) return;
        TimeToAttack -= Time.deltaTime;
        //se hacerca

        if (!enemy.IsWithinStrikingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.EnemyChaseState);
            enemy.animator.SetBool("StrafeForward", false);
            enemy.animator.SetBool("StrafeBackward", false);
            return;
        }
        //strafe
        if(strafeDirection == Vector3.zero)
        {

            if(Random.Range(0, 1) == 0)
            {
                strafeDirection = transform.right *-1;

                enemy.animator.SetBool("StrafeForward", true);
            }
            else
            {
                strafeDirection = transform.right;
                enemy.animator.SetBool("StrafeBackward", true);
            }
        }
        enemy.MoveEnemy(strafeDirection * StrafeSpeed);
        if(TimeToAttack < 0)
        {
            if (CanAttack())
            {
                enemy.IsAttacking = true;
                _currentAttackTime = enemy.GetAttackRate();
                enemy.animator.SetInteger("AttackNumber", Random.Range(0, MaxAttacksTypes));
                enemy.animator.SetTrigger("Attack");
                enemy.animator.SetBool("StrafeForward", false);
                enemy.animator.SetBool("StrafeBackward", false);
                strafeDirection = Vector3.zero;
            }
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

}

