using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable, IEnemyAttacker
{
    private Transform _playerTransform = null;
    [field: SerializeField] public float MaxHealth { get; set; } = 30;
    [field: SerializeField] public float CurrentHealth { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public StateMachine StateMachine { get; set; }
    public EnemyIdleState EnemyIdleState { get; set; }
    public EnemyChaseState EnemyChaseState { get; set; }
    public EnemyFleeState EnemyFleeState { get; set; }
    public EnemyAttackState EnemyAttackState { get; set; }
    public EnemyDieState EnemyDieState { get; set; }
    public bool IsWithinAggroDistance { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    public bool IsWithinFleeDistance { get; set; }
    public  Animator animator;
    protected Vector3 _attackDirection;
    [SerializeField] protected Transform attackPosition;
    public bool IsAttacking = false;
    public bool CanMove = true;
    

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;
    [SerializeField] private EnemyFleeSOBase EnemyFleeBase;
    [SerializeField] private EnemyDieSOBase EnemyDieBase;
    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyFleeSOBase EnemyFleeBaseInstance { get; set; }
    public EnemyDieSOBase EnemyDieBaseInstance { get; set; }

    public void GetKnockBack(Vector3 aForce)
    {
        CanMove = false;
        Rigidbody.AddForce(aForce, ForceMode.Impulse);
        animator.SetTrigger("KnockBack");
    }


    public void Die()
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
            Die();
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

    // Start is called before the first frame update
    void Awake()
    {
    }
    public void Initialize(Transform aPlayerTransform)
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        EnemyFleeBaseInstance = Instantiate(EnemyFleeBase);
        EnemyDieBaseInstance = Instantiate(EnemyDieBase);

        EnemyIdleBaseInstance.Initialize(gameObject, this, aPlayerTransform);
        EnemyChaseBaseInstance.Initialize(gameObject, this, aPlayerTransform);
        EnemyAttackBaseInstance.Initialize(gameObject, this, aPlayerTransform);
        EnemyFleeBaseInstance.Initialize(gameObject, this, aPlayerTransform);
        EnemyDieBaseInstance.Initialize(gameObject, this, aPlayerTransform);

        StateMachine = new StateMachine();
        EnemyIdleState = new EnemyIdleState(this, StateMachine);
        EnemyChaseState = new EnemyChaseState(this, StateMachine);
        EnemyAttackState = new EnemyAttackState(this, StateMachine);
        EnemyFleeState = new EnemyFleeState(this, StateMachine);
        EnemyDieState = new EnemyDieState(this, StateMachine);
        StateMachine.Initialize(EnemyIdleState);
        _playerTransform = aPlayerTransform;

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
        CurrentHealth = MaxHealth;
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

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
