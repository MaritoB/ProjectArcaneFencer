using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public PlayerInputActions playerInputActions;
    public Rigidbody mRigidbody;
    public Transform mCamera;
    [SerializeField]
    private int RotationSpeed;
    [SerializeField]
    private ProjectileSpawner projectileSpawner;
    public Animator animator;
    [SerializeField]
    PlayerInGameUI inGameUI;
    bool isDead = false;
    [SerializeField]
    public ParticleSystem ParryParticleSystem, MeleeHitPS, DashPS;
    [SerializeField]
    float ParryRadius, MeleeAttackRadius;
    [SerializeField]
    int MeleeDamage;
    [SerializeField]
    LayerMask ProjectilesLayer;
    [SerializeField]
    LayerMask EnemiesLayer;
    [SerializeField]
    float MaxStamina, CurrentStamina;
    [SerializeField]
    int DashStaminaCost, RecoverStaminaOnParry;
    [SerializeField]
    float StaminaRecoveryRate, AttackStaminaCost;


    #region
    public StateMachine PlayerStateMachine;
    [SerializeField]
    private PlayerMovementBase PlayerRunBase;
    [SerializeField]
    private PlayerMovementBase PlayerDash;
    public PlayerMovementBase PlayerRunInstance { get; set; }
    public PlayerMovementBase PlayerDashInstance { get; set; }
    public PlayerMovementState PlayerRunState { get; set; }
    public PlayerDashState PlayerDashState { get; set; }

    [SerializeField]
    private PlayerAttackSOBase PlayerAttackBase, PlayerAttackParry;
    public PlayerAttackSOBase PlayerAttackBaseInstance { get; set; }
    public PlayerAttackState PlayerAttackState { get; set; }
    #endregion
    public ProjectileSkillSOBase ProjectileSkillInstance;

    [SerializeField]
    public  ProjectileSkillSOBase SingleProjectileSkill, TripleProjectileSkill;
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }

    private void Awake()
    {
        mCamera = Camera.main.gameObject.transform;
        CurrentHealth = MaxHealth;
        CurrentStamina = MaxStamina;
        mRigidbody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        Initialize();
    }
    public void Initialize()
    {
        SingleProjectileSkill = Instantiate(SingleProjectileSkill);
        TripleProjectileSkill = Instantiate(TripleProjectileSkill);
        PlayerRunInstance = Instantiate(PlayerRunBase);
        PlayerDashInstance = Instantiate(PlayerDash);

        PlayerAttackBaseInstance = Instantiate(PlayerAttackBase);
        PlayerAttackParry = Instantiate(PlayerAttackParry);
        ProjectileSkillInstance = SingleProjectileSkill;
        PlayerDashInstance.Initialize(gameObject, this);
        PlayerRunInstance.Initialize(gameObject, this);
        PlayerAttackBaseInstance.Initialize(gameObject, this);


        PlayerStateMachine = new StateMachine();
        PlayerDashState = new PlayerDashState(PlayerStateMachine, this);
        PlayerRunState = new PlayerMovementState(PlayerStateMachine, this);
        PlayerAttackState = new PlayerAttackState(PlayerStateMachine, this);
        PlayerStateMachine.Initialize(PlayerRunState);

    }
    private void Update()
    {
        if(CurrentStamina < MaxStamina)
        {
            CurrentStamina += Time.deltaTime * StaminaRecoveryRate;
            UpdateStaminaUI();
        }
        if (PlayerStateMachine == null) return;
        PlayerStateMachine.CurrentState.FrameUpdate();

    }
    public void UpdateStaminaUI()
    {
        if(inGameUI != null)
        {
            inGameUI.UpdateCurrentStaminaUI(CurrentStamina, MaxStamina);
        }
    }
    public float GetCurrentStamina() { return CurrentStamina; }
    public void PerfomrProjectileAttackSkill()
    {
        if (PlayerStateMachine == null) return;
        ProjectileSkillInstance.UseSkill(projectileSpawner);
    }
    public void ParryProjectile()
    {
        if (ProjectileSkillInstance == null) return;
        Collider[] ProjectileColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, ParryRadius, ProjectilesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            // Shoot Forward
            // ProjectileSkillInstance.UseSkill(projectileSpawner);

            // Reflect projectile
            ProjectileSkillInstance.UseSkill(projectileSpawner, (collider.GetComponent<Rigidbody>().velocity * -1).normalized);
            if (ParryParticleSystem != null)
            {
                ParryParticleSystem.Emit(30);
            }
            collider.GetComponent<ProjectileBehaviour>().DisableProjectile();
            // Recover Stamina on Parry
            RecoverStamina(RecoverStaminaOnParry);
        }
    }

    private void RecoverStamina(int aAmount)
    {
        if(aAmount < 0) return;
        CurrentStamina += aAmount;
        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        UpdateStaminaUI();
    }

    public void SimpleMeleeAttack()
    {
        Collider[] ProjectileColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, MeleeAttackRadius, EnemiesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            IDamageable enemy = collider.GetComponent<IDamageable>();
            if (enemy != null)
            {
                MeleeHitPS.Emit(15);
                enemy.TakeDamage(MeleeDamage);
            }
        }
        ParryProjectile();
    }

    public void ShootProjectile()
    {
        //projectileSpawner.ShootProjectileForwardFromPool();
        PerfomrProjectileAttackSkill();
    }
    public void ChangeStateToRun()
    {
        PlayerStateMachine.ChangeState(PlayerRunState);
    }

    private void FixedUpdate()
    {
        if (PlayerStateMachine == null) return;
        PlayerStateMachine.CurrentState.PhysicsUpdate();
    }
    public void RotateTowardMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 Direction = new Vector3(inputVector.x, 0 , inputVector.y).normalized;
        Direction = Quaternion.Euler(0f, mCamera.transform.eulerAngles.y, 0f) * Direction;
        if (Direction == Vector3.zero) return;
        var rotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);

    }

    public void TakeDamage(int aDamageAmount)
    {
        CurrentHealth -= aDamageAmount;

        if(CurrentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
        if(inGameUI != null)
        {
            inGameUI.UpdateCurrentHealthUI(CurrentHealth,MaxHealth);

        }

    }

    public void Die()
    {
        animator.SetTrigger("Die");
        inGameUI.FadeIn();
        this.enabled = false;
    }

    public bool TryDash()
    {
        if (DashStaminaCost > CurrentStamina || (playerInputActions.Player.Movement.ReadValue<Vector2>() == Vector2.zero))
        {
            return false;

        }
        else
        {
            CurrentStamina -= DashStaminaCost;
            UpdateStaminaUI();
            return true;
        }

    }
    public bool TryAttack()
    {
        if (AttackStaminaCost > CurrentStamina)
        {
            return false;
        }
        else
        {
            CurrentStamina -= AttackStaminaCost;
            UpdateStaminaUI();
            return true;
        }

    }
}
