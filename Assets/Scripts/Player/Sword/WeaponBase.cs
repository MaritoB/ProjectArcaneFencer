using System.Collections.Generic;
using UnityEngine;
public enum HitEventTypes
{
    OnFirstMeleeHit,
    OnSecondMeleeHit,
    OnThirdMeleeHit,
    OnCriticalHit,
    OnBlockHit,
    OnParryHit
}

// Enum para eventos que no requieren parámetros adicionales
public enum PerformedEventTypes
{
    OnFirstMeleePerformed,
    OnSecondMeleePerformed,
    OnThirdMeleePerformed,
    OnDash
}

public class WeaponBase : MonoBehaviour
{
    public delegate void HitEventDelegate(GameObject hitObject, Vector3 aDirection);

    // Delegados para eventos que no necesitan parámetros adicionales
    public delegate void PerformedEventDelegate();

    // Eventos que necesitan un Enemy
    public event HitEventDelegate OnFirstMeleeHit;
    public event HitEventDelegate OnSecondMeleeHit;
    public event HitEventDelegate OnThirdMeleeHit;
    public event HitEventDelegate OnCriticalHit;
    public event HitEventDelegate OnParryHit;

    // Eventos que no necesitan parámetros adicionales
    public event PerformedEventDelegate OnFirstMeleePerformed;
    public event PerformedEventDelegate OnSecondMeleePerformed;
    public event PerformedEventDelegate OnThirdMeleePerformed;
    //public List<WeaponModifierSO> AllWeaponModifierList;
    PlayerController playerController;
    public ParticleSystem ParryParticleSystem, MeleeHitPS; 
    [SerializeField] int baseDamage;
    [SerializeField] float  attackRadius;
    //[SerializeField] float attackSpeed;
    [SerializeField] int CriticalChance;
    [SerializeField] int attackStaminaCost;
    [SerializeField] int currentDamage;

    AttackInfo attackInfo;
    private void Start()
    {
        currentDamage = baseDamage;
    }
    public void SubscribeToEvent(HitEventTypes eventType, HitEventDelegate handler)
    {
        if (handler == null)
        {
            Debug.LogWarning("Handler is null.");
            return;
        }

        switch (eventType)
        {
            case HitEventTypes.OnFirstMeleeHit:
                OnFirstMeleeHit += handler;
                break;

            case HitEventTypes.OnSecondMeleeHit:
                OnSecondMeleeHit += handler;
                break;

            case HitEventTypes.OnThirdMeleeHit:
                OnThirdMeleeHit += handler;
                break;

            case HitEventTypes.OnCriticalHit:
                OnCriticalHit += handler;
                break;

            case HitEventTypes.OnBlockHit:
                playerController.OnBlockHit += handler;
                break;

            case HitEventTypes.OnParryHit:
                OnParryHit += handler;
                break;

            default:
                Debug.LogWarning($"Unknown HitEventType: {eventType}");
                break;
        }
    }

    // Métodos para desuscribirse de eventos que necesitan un Enemy
    public void UnsubscribeFromEvent(HitEventTypes eventType, HitEventDelegate handler)
    {
        if (handler == null)
        {
            Debug.LogWarning("Handler is null.");
            return;
        }

        switch (eventType)
        {
            case HitEventTypes.OnFirstMeleeHit:
                OnFirstMeleeHit -= handler;
                break;

            case HitEventTypes.OnSecondMeleeHit:
                OnSecondMeleeHit -= handler;
                break;

            case HitEventTypes.OnThirdMeleeHit:
                OnThirdMeleeHit -= handler;
                break;

            case HitEventTypes.OnCriticalHit:
                OnCriticalHit -= handler;
                break;

            case HitEventTypes.OnBlockHit:
                playerController.OnBlockHit -= handler;
                break;

            case HitEventTypes.OnParryHit:
                OnParryHit -= handler;
                break;

            default:
                Debug.LogWarning($"Unknown HitEventType: {eventType}");
                break;
        }
    }

    // Métodos para suscribirse a eventos sin parámetros adicionales
    public void SubscribeToEvent(PerformedEventTypes eventType, PerformedEventDelegate handler)
    {
        if (handler == null)
        {
            Debug.LogWarning("Handler is null.");
            return;
        }

        switch (eventType)
        {
            case PerformedEventTypes.OnFirstMeleePerformed:
                OnFirstMeleePerformed += handler;
                break;

            case PerformedEventTypes.OnSecondMeleePerformed:
                OnSecondMeleePerformed += handler;
                break;

            case PerformedEventTypes.OnThirdMeleePerformed:
                OnThirdMeleePerformed += handler;
                break;

            case PerformedEventTypes.OnDash:
                playerController.OnDash += handler;
                break;

            default:
                Debug.LogWarning($"Unknown PerformedEventType: {eventType}");
                break;
        }
    }

    // Métodos para desuscribirse de eventos sin parámetros adicionales
    public void UnsubscribeFromEvent(PerformedEventTypes eventType, PerformedEventDelegate handler)
    {
        if (handler == null)
        {
            Debug.LogWarning("Handler is null.");
            return;
        }

        switch (eventType)
        {
            case PerformedEventTypes.OnFirstMeleePerformed:
                OnFirstMeleePerformed -= handler;
                break;

            case PerformedEventTypes.OnSecondMeleePerformed:
                OnSecondMeleePerformed -= handler;
                break;

            case PerformedEventTypes.OnThirdMeleePerformed:
                OnThirdMeleePerformed -= handler;
                break;

            case PerformedEventTypes.OnDash:
                playerController.OnDash -= handler;
                break;

            default:
                Debug.LogWarning($"Unknown PerformedEventType: {eventType}");
                break;
        }
    }
    public int GetBaseDamage() { return baseDamage; }
    public int GetCurrentDamage() { return currentDamage; }
    public void ConfiguerWeapon(int aBaseDamage, float aAttackRadius, int aCriticalChance, int aStaminaCost)
    {

        baseDamage = aBaseDamage;
        attackRadius = aAttackRadius;
        CriticalChance = aCriticalChance;
        attackStaminaCost = aStaminaCost;
        currentDamage = baseDamage;
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(currentDamage, DamageType.PHYSICAL, false, false, 0f, this.gameObject) ;
        }
        else
        {
            attackInfo.damage = currentDamage;
        }

    }
    public void SetDamage(int aDamage)
    {
        if (aDamage < 0)
        {
            return;
        }
        currentDamage = aDamage;
    }
    public void SetPlayerController(PlayerController aPlayer)
    {
        if (aPlayer == null) return;
        playerController = aPlayer; 
    }
    public void SetCriticalChance(int aCriticalChance)
    {
        if (aCriticalChance < 0)
        {
            return;
        }
        CriticalChance = aCriticalChance;
    }
    public void ParryModifier(GameObject hitObject, Vector3 aDirection)
    {
        OnParryHit?.Invoke(hitObject, aDirection);
    }
    public void ParryProjectile()
    {
        //if (mProjectileSkillInstance == null) return;
        Collider[] ProjectileColliders = Physics.OverlapSphere(playerController.AttackTransform.position, attackRadius, playerController.EnemyProjectilesLayer);
        foreach (Collider collider in ProjectileColliders)
        {

            ProjectileBehaviour projectile = collider.GetComponent<ProjectileBehaviour>();
            if (ParryParticleSystem != null)
            {
                ParryParticleSystem.Emit(30);
            }
            OnParryHit?.Invoke(collider.gameObject, projectile.GetDirection()*-1) ; 
            playerController.PlayParryStepSounds(); 
            projectile.DisableProjectile(); 
        }
    }

    public void Attack(Enemy enemy, float aWeaponDamagePercentage)
    { 
        int AttackDamage = (int)(currentDamage * aWeaponDamagePercentage);
        bool isCritical = false;

        if (Random.Range(1, 100) < CriticalChance)
        {
            isCritical = true;
            OnCriticalHit?.Invoke(enemy.gameObject, (enemy.transform.position - playerController.transform.position).normalized);
            AttackDamage *= 2;
        }
        if (attackInfo == null)
        {
            attackInfo = new AttackInfo(AttackDamage, DamageType.PHYSICAL, isCritical, isCritical, 0f, playerController.gameObject);
        }
        else
        {
            attackInfo.damage = AttackDamage;
            attackInfo.isCritical = isCritical;
        }
        enemy.TakeDamage(attackInfo);
    }
    internal void SetAttackStaminaCost(int aStaminaCost)
    {
        if (aStaminaCost < 0)
        {
            return;
        }
        attackStaminaCost = aStaminaCost;
    }

    public void FirstMeleeAttack(float aWeaponDamagePercentage)
    { 
        Collider[] EnemyColliders = Physics.OverlapSphere(playerController.AttackTransform.position,  attackRadius , playerController.EnemiesLayer);
        foreach (Collider collider in EnemyColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (MeleeHitPS != null)
                {
                    MeleeHitPS.Emit((int)(15 * aWeaponDamagePercentage));
                } 
                Attack(enemy, aWeaponDamagePercentage);
                FirstStrikeModifiers(enemy.gameObject, (enemy.transform.position - playerController.transform.position).normalized);
            }
        }
        FirstStrikePerformedModifiers(); 
        ParryProjectile();
    }
 
    public void SecondMeleeAttack(float aWeaponDamagePercentage)
    {
        Collider[] EnemyColliders = Physics.OverlapSphere(playerController.AttackTransform.position, attackRadius, playerController.EnemiesLayer);
        foreach (Collider collider in EnemyColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (MeleeHitPS != null)
                {
                    MeleeHitPS.Emit((int)(15 * aWeaponDamagePercentage));
                } 
                Attack(enemy, aWeaponDamagePercentage);
                SecondStrikeModifiers(enemy.gameObject, (enemy.transform.position - playerController.transform.position).normalized);
            }
        }

        SecondStrikePerformedModifiers(); 
        ParryProjectile();
    }
    public void ThirdMeleeAttack(float aWeaponDamagePercentage)
    {
        Collider[] EnemyColliders = Physics.OverlapSphere(playerController.AttackTransform.position, attackRadius, playerController.EnemiesLayer);
        foreach (Collider collider in EnemyColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (MeleeHitPS != null)
                {
                    MeleeHitPS.Emit((int)(15 * aWeaponDamagePercentage));
                } 
                Attack(enemy, aWeaponDamagePercentage);
                ThirdStrikeModifiers(enemy.gameObject, (enemy.transform.position - playerController.transform.position).normalized);
            }
        }
        ThirdStrikePerformedModifiers(); 
        ParryProjectile();
    }

    public void FirstStrikeModifiers(GameObject hitObject, Vector3 aDirection)
    {
        OnFirstMeleeHit?.Invoke(hitObject, aDirection);
    }
    public void SecondStrikeModifiers(GameObject hitObject, Vector3 aDirection)
    {
        OnSecondMeleeHit?.Invoke(hitObject, aDirection);
    }
    public void ThirdStrikeModifiers(GameObject hitObject, Vector3 aDirection)
    {
        OnThirdMeleeHit?.Invoke(hitObject, aDirection);
    }
    public void FirstStrikePerformedModifiers()
    {
        OnFirstMeleePerformed?.Invoke();
    }
    public void SecondStrikePerformedModifiers()
    {
        OnSecondMeleePerformed?.Invoke();
    }
    public void ThirdStrikePerformedModifiers()
    {
        OnThirdMeleePerformed?.Invoke();
    } 
    internal float GetAttackStaminaCost()
    {
        return attackStaminaCost;
    }

}
