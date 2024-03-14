using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable, IEnemyAttacker
{
    [SerializeField] protected EnemyData _enemyData;
    [SerializeField] protected EnemyStateData _enemyStateData;

    private Transform _playerTransform = null;
    [field: SerializeField] public float CurrentHealth { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public bool IsWithinAggroDistance { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    public bool IsWithinFleeDistance { get; set; }
    public  Animator animator;
    protected Vector3 _attackDirection;
    [SerializeField] protected Transform attackPosition;
    public bool IsAttacking = false;
    public bool CanMove = true;

    public StateMachine StateMachine { get; set; }

    public EnemyIdleState EnemyIdleState { get; set; }
    public EnemyChaseState EnemyChaseState { get; set; }
    public EnemyFleeState EnemyFleeState { get; set; }
    public EnemyAttackState EnemyAttackState { get; set; }
    public EnemyKnockBackState EnemyKnockBackState { get; set; }
    public EnemyDieState EnemyDieState { get; set; }

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyFleeSOBase EnemyFleeBaseInstance { get; set; }
    public EnemyKnockBackSOBase EnemyKnockBackBaseInstance { get; set; }
    public EnemyDieSOBase EnemyDieBaseInstance { get; set; }

    public void GetKnockBack(Vector3 aForce)
    {
        if (CurrentHealth > 0)
        {
            if (StateMachine == null || EnemyKnockBackState == null) { return; }
            StateMachine.ChangeState(EnemyKnockBackState);
            CanMove = false;
            Rigidbody.AddForce(aForce, ForceMode.Impulse);

        }
    }

    public void SetStateToIdle()
    {
        CanMove = true;
        if (StateMachine == null || EnemyIdleState == null) { return; }
        StateMachine.ChangeState(EnemyIdleState);
    }
    public void SetStateToChase()
    {
        CanMove = true;
        if (StateMachine == null || EnemyIdleState == null) { return; }
        StateMachine.ChangeState(EnemyChaseState);
    }
    public void Death()
    {
        //gameObject.SetActive(false);
        if(StateMachine == null || EnemyDieState == null) { return; }
        StateMachine.ChangeState(EnemyDieState);

    }

    public void MoveEnemy(Vector3 aVelocity)
    {
        if (!CanMove) return;
        Vector3 movementVelocity = new Vector3(aVelocity.x, 0, aVelocity.z);
        Rigidbody.velocity = movementVelocity;
        animator.SetFloat("Velocity", Rigidbody.velocity.magnitude);
    }

    public void TakeDamage(int aDamageAmount)
    {
        CurrentHealth -= aDamageAmount;

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }
    public void AimPlayerPosition()
    {
        if (_playerTransform == null) { return; }
        _attackDirection = (_playerTransform.position - transform.position).normalized;
        Quaternion LookAtRotation = Quaternion.LookRotation(_attackDirection);
        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = LookAtRotationOnly_Y;
    }
    public void AimDirection( Vector3 direction)
    {
        Quaternion LookAtRotation = Quaternion.LookRotation(direction);
        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = LookAtRotationOnly_Y;
    }
    public void SetPlayerTarget(Transform aPlayer)
    {
        _playerTransform = aPlayer;
        EnemyIdleBaseInstance.SetPlayerTarget(aPlayer);
        EnemyChaseBaseInstance.SetPlayerTarget(aPlayer);
        EnemyAttackBaseInstance.SetPlayerTarget(aPlayer);
        EnemyFleeBaseInstance.SetPlayerTarget(aPlayer);
        EnemyKnockBackBaseInstance.SetPlayerTarget(aPlayer);
        EnemyDieBaseInstance.SetPlayerTarget(aPlayer);
    }

    private void Update()
    {
        if (_playerTransform == null & StateMachine == null) return;
        StateMachine.CurrentState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        if (_playerTransform == null & StateMachine == null) return;
        StateMachine.CurrentState.PhysicsUpdate();
    }
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); CurrentHealth = _enemyData.maxHealth;
        CurrentHealth = _enemyData.maxHealth;
        //StateMachine Initialize
        EnemyIdleBaseInstance = Instantiate(_enemyStateData.idleStateData);
        EnemyChaseBaseInstance = Instantiate(_enemyStateData.chaseStateData);
        EnemyAttackBaseInstance = Instantiate(_enemyStateData.attackStateData);
        EnemyFleeBaseInstance = Instantiate(_enemyStateData.fleeStateData);
        EnemyKnockBackBaseInstance = Instantiate(_enemyStateData.knockBackStateData);
        EnemyDieBaseInstance = Instantiate(_enemyStateData.dieStateData);

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        EnemyFleeBaseInstance.Initialize(gameObject, this);
        EnemyKnockBackBaseInstance.Initialize(gameObject, this);
        EnemyDieBaseInstance.Initialize(gameObject, this);

        StateMachine = new StateMachine();
        EnemyIdleState = new EnemyIdleState(this, StateMachine);
        EnemyChaseState = new EnemyChaseState(this, StateMachine);
        EnemyAttackState = new EnemyAttackState(this, StateMachine);
        EnemyFleeState = new EnemyFleeState(this, StateMachine);
        EnemyKnockBackState = new EnemyKnockBackState(this, StateMachine);
        EnemyDieState = new EnemyDieState(this, StateMachine);
        StateMachine.Initialize(EnemyIdleState);

    }
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }

    public void SetAggroStatus(bool isAggroed)
    {
        IsWithinAggroDistance = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isStrikingDistance)
    {
        IsWithinStrikingDistance = isStrikingDistance;
    }
    public void SetWithinFleeDistanceBool(bool aIsWithinFleeDistance)
    {
        IsWithinFleeDistance = aIsWithinFleeDistance;
    }

    internal void SetPlayerToNull()
    {
        _playerTransform = null;
    }

    public virtual void Attack()
    {
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

}
