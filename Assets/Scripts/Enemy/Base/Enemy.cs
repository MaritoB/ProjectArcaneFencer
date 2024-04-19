using UnityEngine;
using FMODUnity;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable, IEnemyAttacker
{
    public EnemyData enemyData;
    public EnemySoundData enemySoundData;
    public EnemyStateData enemyStateData;
    public LayerMask wallLayer;
    private Transform _playerTransform = null;

    
    [SerializeField] protected int currentLevel = 0;
    [SerializeField] protected float currentAttackRange;
    [SerializeField] protected float currentAttackRate;
    [SerializeField] protected  int currentAttackDamage;
    public float GetAttackRate() { return currentAttackRate; }
    [field: SerializeField] public float CurrentHealth { get; set; }
    public Rigidbody mRigidbody { get; set; }
    public bool IsWithinAggroDistance { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    public bool IsWithinFleeDistance { get; set; }
    public Animator animator;
    protected Vector3 _attackDirection;
    [SerializeField] protected Transform attackPosition;
    public bool IsAttacking = false;
    public bool CanMove = true;
    public bool IsAlive = true;
    public bool IsUnstopable = false;
    public bool IsStunned = false;
    public StateMachine StateMachine { get; set; }

    public EnemyIdleState EnemyIdleState { get; set; }
    public EnemyChaseState EnemyChaseState { get; set; }
    public EnemyChaseState EnemyChaseRunForSecondsState { get; set; }
    public EnemyFleeState EnemyFleeState { get; set; }
    public EnemyAttackState EnemyAttackState { get; set; }
    public EnemStunState EnemyStunState { get; set; }
    public EnemyDieState EnemyDieState { get; set; }

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    public EnemyFleeSOBase EnemyFleeBaseInstance { get; set; }
    public EnemyStunSOBase EnemyStunBaseInstance { get; set; }
    public EnemyDieSOBase EnemyDieBaseInstance { get; set; }
    public EnemyStunSOBase EnemyKnockbackInstance { get; set; }
    public EnemyStunSOBase EnemyHitStunInstance { get; set; }

    public IOwner owner = null;
    public void SetOwner(IOwner aOwner)
    {
        owner = aOwner;
    }
    public void InformDeathToOwner()
    {
        owner.InformEnemyDeath();
    }

    public void GetKnockBack(Vector3 aForce)
    {
        mRigidbody.AddForce(aForce, ForceMode.Impulse);
        if (CurrentHealth >0)
        {
            if (IsStunned ||  StateMachine == null || EnemyKnockbackInstance == null) { return; }
            IsStunned = true;
            EnemyStunBaseInstance = EnemyKnockbackInstance;
            StateMachine.ChangeState(EnemyStunState);
        }
        else
        {
            Death();
            return;
        }
    
    }
    public void ResetStats()
    {
        CurrentHealth = enemyData.maxHealthBase + enemyData.maxHealthMultiplier * currentLevel;
        //mRigidbody.
        mRigidbody.velocity = Vector3.zero;
        mRigidbody.useGravity = true;
        GetComponent<CapsuleCollider>().enabled = true;
        animator.SetTrigger("Revive");
        IsAttacking = false;
        IsAlive = true;
        SetStateToChase();
    }
    public void DashForward(int aDashForce)
    {
        if(!IsAlive) { return; }
        IsUnstopable = true;
        mRigidbody.AddForce(transform.forward* aDashForce, ForceMode.Impulse);
        CanMove = false;
    }
    public void StopMovement()
    {
        if (!IsAlive) { return; }
        mRigidbody.velocity = Vector3.zero;
        animator.SetFloat("Velocity", 0f);
    }

    public void SetStateToIdle()
    {
        if (!IsAlive)
        {
            StateMachine.ChangeState(EnemyDieState);
            return;
        }
        if (IsAlive || StateMachine == null || EnemyIdleState == null) { return; }
        CanMove = true;
        StateMachine.ChangeState(EnemyIdleState);
    }

    public void SetStateToChase()
    {
        if (CurrentHealth <=0)
        {
            Death();
            return;
        }
        if (StateMachine == null || EnemyChaseState == null) { return; }
        CanMove = true;
        StateMachine.ChangeState(EnemyChaseState);
    }
    public void Death()
    {
        IsAlive = false;
        AudioManager.instance.PlayOneShot(enemySoundData.EnemyDeath, transform.position);
        //SceneManagerSingleton.Instance.AddSouls(_enemyData.soulsAmount);
        if (StateMachine == null || EnemyDieState == null) { return; }
        StateMachine.ChangeState(EnemyDieState);

    }
    public void PlayEnemyAttackSound()
    {
        AudioManager.instance.PlayOneShot(enemySoundData.EnemyAttack, transform.position);
    }
    public void PlayEnemyFootStepSound()
    {
        AudioManager.instance.PlayOneShot(enemySoundData.EnemyWalk, transform.position);
    }

    public void MoveEnemy(Vector3 aVelocity)
    {
        if (!IsAlive) return;
        if (!CanMove) return;
        Vector3 movementVelocity = new Vector3(aVelocity.x, mRigidbody.velocity.y, aVelocity.z);
        mRigidbody.velocity = movementVelocity;
        animator.SetFloat("Velocity", mRigidbody.velocity.magnitude);
    }
    public void HitStun()
    {
        EnemyStunBaseInstance = EnemyHitStunInstance;
        if (StateMachine != null && !IsStunned )
        {
            StateMachine.ChangeState(EnemyStunState);
        }

    }
    public virtual void TakeDamage(int aDamageAmount, GameObject aSource)
    {
        if (!IsAlive || aDamageAmount<0) return;
        CurrentHealth -= aDamageAmount;
        if (CurrentHealth > 0)
        {

            AudioManager.instance.PlayOneShot(enemySoundData.EnemyOnHit, transform.position);
            if (!IsUnstopable) 
            {
                HitStun();
                IsAttacking = false;
            }
        }
        else
        {
            Death();
            if (owner != null)
            {
                owner.InformEnemyDeath();
            }
        }
    }
    public void AimPlayerPosition()
    {
        if (_playerTransform == null || !IsAlive) { return; }
        _attackDirection = (_playerTransform.position - transform.position).normalized;
        Quaternion LookAtRotation = Quaternion.LookRotation(_attackDirection);
        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = LookAtRotationOnly_Y;
    }
    public void AimDirection(Vector3 direction)
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
        EnemyHitStunInstance.SetPlayerTarget(aPlayer);
        EnemyKnockbackInstance.SetPlayerTarget(aPlayer);
        EnemyStunBaseInstance.SetPlayerTarget(aPlayer);
        EnemyDieBaseInstance.SetPlayerTarget(aPlayer);
    }

    private void Update()
    {
        if (!IsAlive || _playerTransform == null || StateMachine == null) return;
        StateMachine.CurrentState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        if (!IsAlive || _playerTransform == null || StateMachine == null) return;
        StateMachine.CurrentState.PhysicsUpdate();
    }
    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        GetComponent<CapsuleCollider>().enabled = true;
        CurrentHealth = enemyData.maxHealthBase + enemyData.maxHealthMultiplier * currentLevel;
        //StateMachine Initialize
        EnemyIdleBaseInstance = Instantiate(enemyStateData.idleStateData);
        EnemyChaseBaseInstance = Instantiate(enemyStateData.chaseStateData);
        EnemyAttackBaseInstance = Instantiate(enemyStateData.attackStateData);
        EnemyFleeBaseInstance = Instantiate(enemyStateData.fleeStateData);
        EnemyDieBaseInstance = Instantiate(enemyStateData.dieStateData);
        EnemyHitStunInstance = Instantiate(enemyStateData.hitStunStateData);
        EnemyKnockbackInstance = Instantiate(enemyStateData.knockBackStateData);
        EnemyStunBaseInstance = EnemyHitStunInstance;

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        EnemyFleeBaseInstance.Initialize(gameObject, this);
        EnemyStunBaseInstance.Initialize(gameObject, this);
        EnemyDieBaseInstance.Initialize(gameObject, this);
        EnemyHitStunInstance.Initialize(gameObject, this);
        EnemyKnockbackInstance.Initialize(gameObject, this);

        StateMachine = new StateMachine();
        EnemyIdleState = new EnemyIdleState(this, StateMachine);
        EnemyChaseState = new EnemyChaseState(this, StateMachine);
        EnemyChaseRunForSecondsState = new EnemyChaseState(this, StateMachine);
        EnemyAttackState = new EnemyAttackState(this, StateMachine);
        EnemyFleeState = new EnemyFleeState(this, StateMachine);
        EnemyStunState = new EnemStunState(this, StateMachine);
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
        IsUnstopable = false;
    }

    internal void SetLevel(int aCurrentLevel)
    {
        currentLevel = aCurrentLevel;
        CurrentHealth = enemyData.maxHealthBase + enemyData.maxHealthMultiplier * currentLevel;
        currentAttackDamage = enemyData.attackDamageBase + enemyData.attackDamageMultiplier * currentLevel;
        currentAttackRange = enemyData.attackRangeBase + enemyData.attackRangeMultiplier * currentLevel;
       
    }

    internal Vector3 GetDirectionToPlayer()
    {
        return _attackDirection = (_playerTransform.position - transform.position).normalized;
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

}
