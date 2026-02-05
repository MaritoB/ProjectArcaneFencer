
using SkeletonEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    //public PlayerInputManager InputManager;
    public PlayerInput playerInput;
    //public PlayerData playerData;
    public CharacterStats playerStats;
    public PlayerSoundData playerSoundData;
    public int CurrentLevel = 0;
    public PlayerInputActions playerInputActions;
    public Rigidbody mRigidbody;
    public Camera mCamera;
    [SerializeField]
    private int RotationSpeed;
    [SerializeField]
    private ProjectileSpawner projectileSpawner;
    public Animator animator;
    [SerializeField]
    PlayerInGameUI inGameUI;
    [SerializeField]
    public ParticleSystem DashPS, BloodOnHit;
    public event WeaponBase.PerformedEventDelegate OnDash;
    public event WeaponBase.HitEventDelegate OnBlockHit;
    public LayerMask EnemyProjectilesLayer, EnemiesLayer, GroundLayer;
    [SerializeField]
    float CurrentStamina;
    bool isAlive = true;
    public bool isBlocking = false;
    public bool isDashing = false;
    public bool isAttacking = false;
    public bool CanAttack = true;
    public bool DashChanellingPerk = false;
    public float rotationSpeed;
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
    public InventoryManager inventory;


    [SerializeField]
    public ProjectileSkillSOBase SingleProjectileSkill, TripleProjectileSkill;
    [field: SerializeField] public float CurrentHealth { get; set; } 
    [SerializeField] Transform MagicSphereShield;
    [SerializeField] Transform SwordTransform;
    [SerializeField] Transform SwordPS;
    public Transform AttackTransform;
    public Vector3 PositionToTeleport;
    public void LevelUP()
    {
        CurrentLevel++;
        //inGameUI.SetRemainingPoints(5);
        //inGameUI.SetupSkillUI();
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
        /*
        if(playerData == null)
        {
            Debug.Log("Null PlayerData");
            return;
        }
        playerData = Instantiate(playerData);
        CurrentHealth = playerData.MaxHealth;
        CurrentStamina = playerData.MaxStamina;
         */
        CurrentHealth = playerStats.maxHealth.GetValue();
        CurrentStamina = playerStats.maxStamina.GetValue();

        mCamera = Camera.main;
        inGameUI.SetPlayer(this);
        inventory = GetComponentInChildren<InventoryManager>();
        mRigidbody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        Initialize();
    }
    public void DisableInputs()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Close.Enable();

    }
    public void EnableInputs()
    {
        playerInputActions.Player.Enable(); 
        playerInputActions.Player.Close.Enable();

    }

    public void LoadNextLevel()
    {
        inGameUI.FadeInLoadNextLevel();
    }
    public void FadeToTeleport(Vector3 aPoint)
    {
        inGameUI.FadeInTeleportNextLevel();
        // check?
        PositionToTeleport = aPoint;
    }
    public void Teleport()
    { 
        transform.position = PositionToTeleport;
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
        if(CurrentStamina> playerStats.maxStamina.GetValue())
        {
            CurrentStamina = playerStats.maxStamina.GetValue();
        }
        UpdateStaminaUI();
    }
    private void Update()
    {
        if(CurrentStamina < playerStats.maxStamina.GetValue())
        {
            RecoverStamina(Time.deltaTime * playerStats.staminaRecoveryRate.GetValue());
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
        newScalePS.y = 0.1f + (CurrentStamina / playerStats.maxStamina.GetValue());
        SwordPS.transform.transform.localScale = newScalePS;
        if (inGameUI != null)
        {
            inGameUI.UpdateCurrentStaminaUI(CurrentStamina, playerStats.maxStamina.GetValue());
        }
    }
    public void TurnOnShield()
    {
        isBlocking = true;
        //animator.SetBool("IsBlocking", true);
        //MagicSphereShield.gameObject.SetActive(true);
        //AudioManager.instance.PlayOneShot(playerSoundData.PlayerShield, transform.position);
    }
    public void TurnOffShield()
    {
        isBlocking = false; 
        animator.SetBool("IsBlocking", false); 
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
    public void PlayParryStepSounds()
    {
        AudioManager.instance.PlayOneShot(playerSoundData.PlayerParry, transform.position);
    }
    public void RecoverLife(int aAmount)
    {
        if (aAmount < 0) return;
        CurrentHealth += aAmount;
        if (CurrentHealth > playerStats.maxHealth.GetValue())
        {
            CurrentHealth = playerStats.maxHealth.GetValue();
        }
        inGameUI.UpdateCurrentHealthUI(CurrentHealth, playerStats.maxHealth.GetValue());
    }
    public void DashForward(int aDashForce)
    {
        mRigidbody.AddForce(transform.forward * aDashForce, ForceMode.Impulse);
    }
    public void OnDashInvoke()
    {
        OnDash?.Invoke();
    } 
    
    public void FirstMeleeAttack(float aWeaponDamagePercentage)
    { 
        inventory.equipmentManager.weapon.FirstMeleeAttack(aWeaponDamagePercentage);
        CanAttack = true;
        isAttacking = false; 
    }

    public void SecondMeleeAttack(float aWeaponDamagePercentage)
    {
        inventory.equipmentManager.weapon.SecondMeleeAttack(aWeaponDamagePercentage);
        CanAttack = true;
        isAttacking = false; 
    }
    public void ThirdMeleeAttack(float aWeaponDamagePercentage)
    {
        inventory.equipmentManager.weapon.ThirdMeleeAttack(aWeaponDamagePercentage);
        CanAttack = true;
        isAttacking = false; 
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
    public void HandleMovement( float movementSpeed)
    {
        Vector2 InputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(InputVector.x, 0, InputVector.y).normalized;
        movementVector = Quaternion.Euler(0, mCamera.transform.eulerAngles.y, 0) * movementVector;
        float turnAmount = RotateAndCalculateTurnTowardMouse();
        Vector3 newVelocity = movementVector * movementSpeed * Time.deltaTime;
        newVelocity.y = mRigidbody.velocity.y;  // Mantener la componente Y actual
        mRigidbody.velocity = newVelocity;
        UpdateAnimator(turnAmount, new Vector2(InputVector.x, InputVector.y));
    }
    public void UpdateAnimator(float turnAmount, Vector2 inputVector)
    {
        // Magnitud total de la velocidad para controlar la animación de movimiento
        float velocityMagnitude = mRigidbody.velocity.magnitude;

        // Obtener la dirección de la cámara en el plano XZ
        Vector3 cameraForward = mCamera.transform.forward;
        cameraForward.y = 0; // Ignorar componente Y
        cameraForward.Normalize();

        Vector3 cameraRight = mCamera.transform.right;
        cameraRight.y = 0; // Ignorar componente Y
        cameraRight.Normalize();

        // Crear el vector de movimiento en relación con la entrada del usuario
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        // Transformar el vector de movimiento en el espacio de la cámara
        Vector3 adjustedMovementDirection = cameraForward * movementDirection.z + cameraRight * movementDirection.x;

        // Obtener el "forward" y "right" en función de la orientación del personaje
        float forwardMovement = Vector3.Dot(adjustedMovementDirection, transform.forward);
        float rightMovement = Vector3.Dot(adjustedMovementDirection, transform.right);

        // Asegurarse de que los valores sean correctos y no estén invertidos
        // Dependiendo de la rotación del jugador en relación a la cámara, puede ser necesario invertir el signo.
        forwardMovement = Mathf.Clamp(forwardMovement, -1f, 1f);
        rightMovement = Mathf.Clamp(rightMovement, -1f, 1f);

        // Actualizar las variables en el Animator
        animator.SetFloat("Velocity", velocityMagnitude);
        animator.SetFloat("ForwardMovement", forwardMovement);  // Movimiento hacia adelante o atrás
        animator.SetFloat("RightMovement", rightMovement);      // Movimiento lateral (strafe)
        animator.SetFloat("Turn", turnAmount);                  // Turno es el valor de giro calculado previamente
    }


    public float RotateAndCalculateTurnTowardMouse()
    {
        // Crear un rayo desde la posición de la cámara hacia la posición del mouse
        Ray ray = mCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Detectar el suelo para obtener la posición de destino (asegúrate de que el suelo esté en la capa GroundLayer)
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundLayer))
        {
            // Obtener la posición en el suelo donde el mouse apunta
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Mantener la misma altura del personaje

            // Dirección hacia la que queremos rotar
            Vector3 directionToLook = (targetPosition - transform.position).normalized;

            if (directionToLook != Vector3.zero)
            {
                // Calcular la rotación deseada hacia la dirección del mouse
                Quaternion targetRotation = Quaternion.LookRotation(directionToLook);

                // Aplicar suavemente la rotación con la velocidad de rotación definida
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Calcular el ángulo entre la dirección actual y la dirección hacia el mouse
                float angleToMouse = Vector3.SignedAngle(transform.forward, directionToLook, Vector3.up);

                // Normalizar el ángulo entre -1 y 1 (para el Animator)
                return Mathf.Clamp(angleToMouse / 180f, -1f, 1f); // Normaliza el ángulo entre -1 y 1
            }
        }

        return 0f; // Si no golpea nada, no hay rotación
    }


    public void TakeDamage(AttackInfo aAttackInfo)
    {
        if (aAttackInfo == null) return;
        float currentDamage = aAttackInfo.damage;
        if (currentDamage < 0) return;
        float Resistance = playerStats.GetResistance(aAttackInfo.damageType);
        if (Resistance != 0)
        {
            Resistance = Mathf.Clamp(Resistance, -75f, 75f);
            currentDamage *= (1 - Resistance / 100f);
        }

        if (aAttackInfo.Source != null)
        {
            float isFacing = Vector3.Dot((aAttackInfo.Source.transform.position - transform.position).normalized, transform.forward.normalized);
            Debug.Log(isFacing);
            if (isFacing > 0.8f)
            {
                if (isBlocking)
                {
                    if (UseStamina(currentDamage * playerStats.staminaDrainPercentageOnBlock.GetValue()))
                    {
                        if (aAttackInfo.Source != null)
                        {
                            OnBlockHit?.Invoke(aAttackInfo.Source, (aAttackInfo.Source.transform.position - transform.position).normalized);
                            animator.SetTrigger("BlockImpact");
                            //Enemy enemy = aAttackInfo.Source.GetComponent<Enemy>();
                            //if (enemy != null)
                            //{

                            //    OnBlockHit?.Invoke(enemy.gameObject, (enemy.transform.position - transform.position).normalized);
                            //}
                        }
                        AudioManager.instance.PlayOneShot(playerSoundData.PlayerShield, transform.position);
                        return;
                    }
                    else
                    {
                        TurnOffShield();
                    }
                }
            }
            else if (isFacing <0)
            {
                Debug.Log("Backhit , interrupt? critical?");
            }
        }
        CurrentHealth -= currentDamage;
        if (BloodOnHit != null)
        {
            inGameUI.TakeDamageUIAnimation();
            BloodOnHit.Emit((int)currentDamage / 3);
            AudioManager.instance.PlayOneShot(playerSoundData.PlayerOnHit, transform.position);


        }
        if (CurrentHealth <= 0 && isAlive)
        {
            isAlive = false;
            Death();
        }
        if (inGameUI != null)
        {
            inGameUI.UpdateCurrentHealthUI(CurrentHealth, playerStats.maxHealth.GetValue());
        }
    }
    public void UpdateHealthUI()
    {
        if (inGameUI != null)
        {
            inGameUI.UpdateCurrentHealthUI(CurrentHealth, playerStats.maxHealth.GetValue());
        }
    }
  /*
    public void TakeDamage(AttackInfo aAttackInfo, GameObject aSource)
    {
        if (aAttackInfo == null)
        if (aAttackInfo == null)
        {
            Debug.Log("null attack info ");
            return;
        }
        float currentDamage = aAttackInfo.damage;
        if (currentDamage < 0) return;
        float Resistance = playerStats.GetResistance(aAttackInfo.damageType);
        if (Resistance != 0)
        {
            Resistance = Mathf.Clamp(Resistance, -75f, 75f);
            currentDamage *= (1 - Resistance/100f);
        }
        if (isBlocking)
        {
            if (UseStamina(currentDamage * playerStats.staminaDrainPercentageOnBlock.GetValue()))
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
        CurrentHealth -= currentDamage;
        if(BloodOnHit != null)
        {
            inGameUI.TakeDamageUIAnimation();
            BloodOnHit.Emit((int)currentDamage / 3);
            AudioManager.instance.PlayOneShot(playerSoundData.PlayerOnHit, transform.position);


        }
        if(CurrentHealth <= 0 && isAlive)
        {
            isAlive = false;
            Death();
        }
        if(inGameUI != null)
        {
            inGameUI.UpdateCurrentHealthUI(CurrentHealth, playerStats.maxHealth.GetValue());
        }

    }
   */

    public void Death()
    {
        //inventory.equipmentManager.weapon.ResetAllModifiers();
        //mSkillManager.ResetSkillLevels();
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
        float dashStaminaCost = playerStats.dashStaminaCost.GetValue();
        if (dashStaminaCost > CurrentStamina)
        {
            return false;

        }
        else
        {
            if(dashStaminaCost > 0)
            {
                CurrentStamina -= dashStaminaCost;
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
        if(inventory.equipmentManager.weapon == null)
        {
            Debug.Log("null Weapon");
            return false;
        }

        if (inventory.equipmentManager.weapon.GetAttackStaminaCost() > CurrentStamina)
        {
            return false;
        }
        else
        {
            CurrentStamina -= inventory.equipmentManager.weapon.GetAttackStaminaCost();
            UpdateStaminaUI();
            CanAttack = false;
            return true;
        }

    }


}
