using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public delegate void OnMeleeHitDelegate(Enemy enemy);
    public delegate void OnMeleePerformedDelegate();
    public event OnMeleeHitDelegate OnFirstMeleeHit, OnSecondMeleeHit, OnThirdMeleeHit, OnCritialHit;
    public event OnMeleePerformedDelegate OnFirstMeleePerformed, OnSecondMeleePerformed, OnThirdMeleePerformed;
    public delegate void OnParryDelegate();
    public event OnParryDelegate OnParry;
    //public List<WeaponModifierSO> AllWeaponModifierList;
    PlayerController playerController;
    public ParticleSystem ParryParticleSystem, MeleeHitPS; 
    [SerializeField] int baseDamage;
    [SerializeField] float  attackRadius;
    //[SerializeField] float attackSpeed;
    [SerializeField] int CriticalChance;
    [SerializeField] int attackStaminaCost;
    [SerializeField] int currentDamage;
    void Start()
    { 
        currentDamage = baseDamage; 
    }
    public int GetBaseDamage() { return baseDamage; }
    public int GetCurrentDamage() { return currentDamage; }
    public void ConfiguerWeapon(int aBaseDamage, float aAttackRadius, int aCriticalCHance, int aStaminaCost)
    {
        baseDamage = aBaseDamage;
        attackRadius = aAttackRadius;
        CriticalChance = aCriticalCHance;
        attackStaminaCost = aStaminaCost;
        currentDamage = baseDamage;

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
    public void ParryModifier()
    {
        OnParry?.Invoke();
    }
    public void ParryProjectile()
    {
        //if (mProjectileSkillInstance == null) return;
        Collider[] ProjectileColliders = Physics.OverlapSphere(playerController.AttackTransform.position, attackRadius, playerController.EnemyProjectilesLayer);
        foreach (Collider collider in ProjectileColliders)
        {
            if (ParryParticleSystem != null)
            {
                ParryParticleSystem.Emit(30);
            } 
            OnParry?.Invoke(); 
            playerController.PlayParryStepSounds(); 
            collider.GetComponent<ProjectileBehaviour>().DisableProjectile();
        }
    }
      
    public void Attack(Enemy enemy, float aWeaponDamagePercentage)
    {
        
        int AttackDamage = (int)(currentDamage * aWeaponDamagePercentage);
        bool isCritical = false;
        
        if (Random.Range(1, 100) < CriticalChance)
        {
            isCritical = true;
            OnCritialHit?.Invoke(enemy);
            AttackDamage *= 2;
        }
       
        enemy.TakeDamage(new AttackInfo(AttackDamage, false, isCritical, playerController.gameObject)); 
    }
    public void CustomMeleeAttack(float aWeaponDamagePercentage)
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
            }
        } 
        ParryProjectile();
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
                FirstStrikeModifiers(enemy);
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
                SecondStrikeModifiers(enemy);
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
                ThirdStrikeModifiers(enemy);
            }
        }
        ThirdStrikePerformedModifiers(); 
        ParryProjectile();
    }

    public void FirstStrikeModifiers(Enemy enemy)
    {
        OnFirstMeleeHit?.Invoke(enemy);
    }
    public void SecondStrikeModifiers(Enemy enemy)
    {
        OnSecondMeleeHit?.Invoke(enemy);
    }
    public void ThirdStrikeModifiers(Enemy enemy)
    {
        OnThirdMeleeHit?.Invoke(enemy);
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
