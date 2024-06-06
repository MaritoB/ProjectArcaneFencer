
using SkeletonEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    //public PlayerInputManager InputManager;
    public  PlayerInput playerInput;
    public PlayerData playerData;
    public PlayerSoundData playerSoundData;
    public int CurrentLevel = 0;
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
    [SerializeField]
    public ParticleSystem ParryParticleSystem, MeleeHitPS, DashPS, BloodOnHit;
    public delegate void OnParryDelegate();
    public event OnParryDelegate OnParry;
    public delegate void OnDashDelegate();
    public event OnDashDelegate OnDash;
    public delegate void OnBlockPerformedDelegate(Enemy enemy);
    public event OnBlockPerformedDelegate OnBlockPerformed;
    [SerializeField]
    LayerMask ProjectilesLayer;
    [SerializeField]
    LayerMask EnemiesLayer;
    [SerializeField]
    float CurrentStamina;
    bool isAlive = true;
    public bool isBlocking = false;
    public bool isDashing = false;
    public bool isAttacking = false;
    public bool CanAttack = true;
    public bool DashChanellingPerk = false;
    #region
    public StateMachine PlayerStateMachine;
    [SerializeField]
    private PlayerMovementBase mPlayerRunBase;
    [SerializeField]
    private PlayerMovementBase mPlayerDash;

    public PlayerMovementState mPlayerRunState { get; set; }
    public PlayerDashState mPlayerDashState { get; set; }
    public PlayerAttackState mPlayerAttackState { get; set; }
    public PlayerBlockState mPlayerBlockState { get; set; }

    [SerializeField]
    private PlayerAttackSOBase PlayerAttackBase, PlayerAttackParry;
    [SerializeField]
    private PlayerBlockSOBase PlayerBlock;

    public PlayerMovementBase mPlayerRunInstance { get; set; }
    public PlayerMovementBase mPlayerDashInstance { get; set; }
    public PlayerAttackSOBase mPlayerAttackBaseInstance { get; set; }
    public PlayerBlockSOBase mPlayerBlockInstance { get; set; }
    #endregion
    public ProjectileSkillSOBase mProjectileSkillInstance;
    public SkillManager mSkillManager;


    [SerializeField]
    public  ProjectileSkillSOBase SingleProjectileSkill, TripleProjectileSkill;
    [field: SerializeField] public float CurrentHealth { get; set; }
    [SerializeField] public  SwordBase sword;
    [SerializeField] Transform MagicSphereShield;
    [SerializeField] Transform SwordTransform;
    [SerializeField] Transform SwordPS;
    public Transform AttackTransform;
    public void LevelUP()
    {
        CurrentLevel++;
        inGameUI.SetRemainingPoints(5);
        inGameUI.SetupSkillUI();
    }
    public void HideSword()
    {
        SwordTransform.gameObject.SetActive(false);
    }
    public void ShowSword()
    {
        SwordTransform.gameObject.SetActive(true);
    }
    private void Awake()
    {
        if(playerData == null)
        {
            Debug.Log("Null PlayerData");
            return;
        }
        playerData = Instantiate(playerData);
        mCamera = Camera.main.gameObject.transform;
        inGameUI.SetPlayer(this);
        CurrentHealth = playerData.MaxHealth;
        CurrentStamina = playerData.MaxStamina;
        mRigidbody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        Initialize();
    }

    public void LoadNextLevel()
    {
        inGameUI.FadeInLoadNextLevel();
    }
    public void Initialize()
    {
        SingleProjectileSkill = Instantiate(SingleProjectileSkill);
        TripleProjectileSkill = Instantiate(TripleProjectileSkill);
        mPlayerRunInstance = Instantiate(mPlayerRunBase);
        mPlayerDashInstance = Instantiate(mPlayerDash);
        mPlayerBlockInstance = Instantiate(PlayerBlock);

        mPlayerAttackBaseInstance = Instantiate(PlayerAttackBase);
        PlayerAttackParry = Instantiate(PlayerAttackParry);
        mProjectileSkillInstance = SingleProjectileSkill;
        mPlayerDashInstance.Initialize(gameObject, this);
        mPlayerRunInstance.Initialize(gameObject, this);
        mPlayerAttackBaseInstance.Initialize(gameObject, this);
        mPlayerBlockInstance.Initialize(gameObject, this);


        PlayerStateMachine = new StateMachine();
        mPlayerDashState = new PlayerDashState(PlayerStateMachine, this);
        mPlayerRunState = new PlayerMovementState(PlayerStateMachine, this);
        mPlayerAttackState = new PlayerAttackState(PlayerStateMachine, this);
        mPlayerBlockState = new PlayerBlockState(PlayerStateMachine, this);
        PlayerStateMachine.Initialize(mPlayerRunState);

    }
    public void RecoverStamina(float aAmount)
    {
        if(aAmount < 0)
        {
            return;
        }
        CurrentStamina += aAmount;
        if(CurrentStamina> playerData.MaxStamina)
        {
            CurrentStamina = playerData.MaxStamina;
        }
        UpdateStaminaUI();
    }
    private void Update()
    {
        if(CurrentStamina < playerData.MaxStamina)
        {
            RecoverStamina(Time.deltaTime * playerData.StaminaRecoveryRate);
        }
        if (PlayerStateMachine == null) return;
        PlayerStateMachine.CurrentState.FrameUpdate();

    }
    public bool UseStamina(float aStaminaCost)
    {
        if (aStaminaCost > CurrentStamina || CurrentStamina < 1 )
        {
            return false;
        }

        CurrentStamina -= aStaminaCost;
        UpdateStaminaUI();
        return true;
    }
    public void UpdateStaminaUI()
    {
        Vector3 newScalePS = SwordPS.transform.transform.localScale;
        newScalePS.y = 0.1f + (CurrentStamina / playerData.MaxStamina);
        SwordPS.transform.transform.localScale = newScalePS;
        if (inGameUI != null)
        {
            inGameUI.UpdateCurrentStaminaUI(CurrentStamina, playerData.MaxStamina);
        }
    }
    public void TurnOnShield()
    {
        isBlocking = true;
        //MagicSphereShield.gameObject.SetActive(true);
        AudioManager.instance.PlayOneShot(playerSoundData.PlayerShield, transform.position);
    }
    public void TurnOffShield()
    {
        isBlocking = false;
        animator.SetTrigger("BlockFinish");
    }
    public float GetCurrentStamina() { return CurrentStamina; }
    public void PerfomrProjectileAttackSkill()
    {
        mProjectileSkillInstance.UseSkill(projectileSpawner);
    }
    public void PerfomrProjectileAttackSkill(Vector3 aDirection)
    {
        mProjectileSkillInstance.UseSkill(projectileSpawner, aDirection);
    }
    public void ParryProjectile()
    {
        if (mProjectileSkillInstance == null) return;
        Collider[] ProjectileColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, playerData.ParryRadius, ProjectilesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            // Shoot Forward
            // ProjectileSkillInstance.UseSkill(projectileSpawner);

            // Reflect projectile
            mProjectileSkillInstance.UseSkill(projectileSpawner, (collider.GetComponent<Rigidbody>().velocity * -1).normalized);
            if (ParryParticleSystem != null)
            {
                ParryParticleSystem.Emit(30);
            }
            // Recover Stamina on Parry
            OnParry?.Invoke();

            AudioManager.instance.PlayOneShot(playerSoundData.PlayerParry, transform.position);
            //RecoverStamina(playerData.RecoverStaminaOnParry);
            collider.GetComponent<ProjectileBehaviour>().DisableProjectile();
        }
    }
    public void PlayAttack1Sounds()
    {
        AudioManager.instance.PlayOneShot(playerSoundData.PlayerAttack1, transform.position);
    }
    public void PlayAttack2Sounds()
    {
        AudioManager.instance.PlayOneShot(playerSoundData.PlayerAttack2, transform.position);
    }
    public void PlayAttack3Sounds()
    {
        AudioManager.instance.PlayOneShot(playerSoundData.PlayerAttack3, transform.position);
    }
    public void PlayFootStepSounds()
    {
        AudioManager.instance.PlayOneShot(playerSoundData.PlayerStep, transform.position);
    }
    public void RecoverLife(int aAmount)
    {
        if (aAmount < 0) return;
        CurrentHealth += aAmount;
        if (CurrentHealth > playerData.MaxHealth)
        {
            CurrentHealth = playerData.MaxHealth;
        }
        inGameUI.UpdateCurrentHealthUI(CurrentHealth, playerData.MaxHealth);
    }
    public void DashForward(int aDashForce)
    {
        mRigidbody.AddForce(transform.forward * aDashForce, ForceMode.Impulse);
    }
    public void OnDashInvoke()
    {
        OnDash?.Invoke();
    }
    public void SimpleMeleeAttack()
    {
        if(sword == null)
        {
            return;
        }

        Collider[] ProjectileColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, playerData.MeleeAttackRadius, EnemiesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                MeleeHitPS.Emit(15);
                //enemy.TakeDamage(sword.GetCurrentDamage());
                sword.Attack(enemy,1);
            }
        }
        CanAttack = true;
        isAttacking = false;
        ParryProjectile();
    }
    public void CustomMeleeAttack(float  aWeaponDamagePercentage)
    {
        if (sword == null)
        {
            return;
        }
        Collider[] ProjectileColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, playerData.MeleeAttackRadius, EnemiesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                MeleeHitPS.Emit((int)(15 * aWeaponDamagePercentage));
                //enemy.TakeDamage(sword.GetCurrentDamage());
                sword.Attack(enemy, aWeaponDamagePercentage);
            }
        }
        CanAttack = true;
        isAttacking = false;
        ParryProjectile();
    }
    public void FirstMeleeAttack(float aWeaponDamagePercentage)
    {
        if (sword == null)
        {
            return;

        }
        Collider[] EnemyColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, playerData.MeleeAttackRadius, EnemiesLayer);
        foreach (Collider collider in EnemyColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                MeleeHitPS.Emit((int)(15 * aWeaponDamagePercentage));
                //enemy.TakeDamage(sword.GetCurrentDamage());
                sword.Attack(enemy, aWeaponDamagePercentage);
                sword.FirstStrikeModifiers(enemy);
            }
        }
        sword.FirstStrikePerformedModifiers();
        CanAttack = true;
        isAttacking = false;
        ParryProjectile();
    }
    public void ParryModifier()
    {
        OnParry?.Invoke();
    }
    public void SecondMeleeAttack(float aWeaponDamagePercentage)
    {
        if (sword == null)
        {
            return;

        }
        Collider[] ProjectileColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, playerData.MeleeAttackRadius, EnemiesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                MeleeHitPS.Emit((int)(15 * aWeaponDamagePercentage));
                //enemy.TakeDamage(sword.GetCurrentDamage());
                sword.Attack(enemy, aWeaponDamagePercentage);
                sword.SecondStrikeModifiers(enemy);
            }
        }

        sword.SecondStrikePerformedModifiers();
        CanAttack = true;
        isAttacking = false;
        ParryProjectile();
    }
    public void ThirdMeleeAttack(float aWeaponDamagePercentage)
    {
        if (sword == null)
        {
            return;

        }
        Collider[] ProjectileColliders = Physics.OverlapSphere(projectileSpawner.ShootPosition.position, playerData.MeleeAttackRadius, EnemiesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                MeleeHitPS.Emit((int)(15 * aWeaponDamagePercentage));
                //enemy.TakeDamage(sword.GetCurrentDamage());
                sword.Attack(enemy, aWeaponDamagePercentage);
                sword.ThirdStrikeModifiers(enemy);
            }
        }
        sword.ThirdStrikePerformedModifiers();
        CanAttack = true;
        isAttacking = false;
        ParryProjectile();
    }

    public void ShootProjectile()
    {
        //projectileSpawner.ShootProjectileForwardFromPool();
        PerfomrProjectileAttackSkill();
    }
    public void ChangeStateToRun()
    {
        if (isAttacking)
        {
            return;
        }
        PlayerStateMachine.ChangeState(mPlayerRunState);
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
    public void TakeDamage(AttackInfo aAttackInfo)
    {
        if (aAttackInfo == null) return;

        if (isBlocking)
        {
            if (UseStamina(aAttackInfo.damage * playerData.StaminaDrainPercentajeOnBlock))
            {
                if (aAttackInfo.Source != null)
                {
                    Enemy enemy = aAttackInfo.Source.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        OnBlockPerformed?.Invoke(enemy);
                    }
                }
                AudioManager.instance.PlayOneShot(playerSoundData.PlayerShield, transform.position);
                return;
            }
            else
            {
                TurnOffShield();
            }
        }
        CurrentHealth -= aAttackInfo.damage;
        if (BloodOnHit != null)
        {
            inGameUI.TakeDamageUIAnimation();
            BloodOnHit.Emit(aAttackInfo.damage / 3);
            AudioManager.instance.PlayOneShot(playerSoundData.PlayerOnHit, transform.position);


        }
        if (CurrentHealth <= 0 && isAlive)
        {
            isAlive = false;
            Death();
        }
        if (inGameUI != null)
        {
            inGameUI.UpdateCurrentHealthUI(CurrentHealth, playerData.MaxHealth);
        }
    }

    public void TakeDamage(int aDamageAmount, GameObject aSource)
    {
        if(aDamageAmount< 0) return;

        if (isBlocking)
        {
            if (UseStamina(aDamageAmount * playerData.StaminaDrainPercentajeOnBlock))
            {
                if (aSource != null)
                {
                    Enemy enemy = aSource.GetComponent<Enemy>();
                    if(enemy != null)
                    {
                        OnBlockPerformed?.Invoke(enemy);
                    }
                }
                AudioManager.instance.PlayOneShot(playerSoundData.PlayerShield, transform.position);
                return;
            }
            else
            {
                TurnOffShield();
            }
        }
        CurrentHealth -= aDamageAmount;
        if(BloodOnHit != null)
        {
            inGameUI.TakeDamageUIAnimation();
            BloodOnHit.Emit(aDamageAmount/3);
            AudioManager.instance.PlayOneShot(playerSoundData.PlayerOnHit, transform.position);


        }
        if(CurrentHealth <= 0 && isAlive)
        {
            isAlive = false;
            Death();
        }
        if(inGameUI != null)
        {
            inGameUI.UpdateCurrentHealthUI(CurrentHealth, playerData.MaxHealth);
        }

    }

    public void Death()
    {
        sword.ResetAllModifiers();
        mSkillManager.ResetSkillLevels();
        animator.SetTrigger("Death");
        AudioManager.instance.TurnOffMusic();
        AudioManager.instance.PlayOneShot(playerSoundData.PlayerDeath, transform.position);
        inGameUI.PlayerDeath();
        this.enabled = false;
    }

    public bool TryDash()
    {
        if (isDashing)
        {
            return false;
        }
        if (playerData.DashStaminaCost > CurrentStamina)
        {
            return false;

        }
        else
        {
            if(playerData.DashStaminaCost > 0)
            {
                CurrentStamina -= playerData.DashStaminaCost;
                UpdateStaminaUI();
            }
            AudioManager.instance.PlayOneShot(playerSoundData.PlayerDash, transform.position);
            return true;
        }

    }
    public bool TryAttack()
    {
        if (!CanAttack)
        {
            return false;
        }
        if(sword == null)
        {
            return false;
        }

        if (sword.GetAttackStaminaCost() > CurrentStamina)
        {
            return false;
        }
        else
        {
            CurrentStamina -= sword.GetAttackStaminaCost();
            UpdateStaminaUI();
            CanAttack = false;
            return true;
        }

    }


}
