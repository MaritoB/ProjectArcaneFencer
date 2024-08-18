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
    public List<WeaponModifierSO> AllWeaponModifierList;
    PlayerController playerController;
    public ParticleSystem ParryParticleSystem, MeleeHitPS; 
    [SerializeField] int baseDamage;
    [SerializeField] float  attackRadius;
    //[SerializeField] float attackSpeed;
    [SerializeField] int CriticalChance;
    [SerializeField] int attackStaminaCost;
    [SerializeField] int currentDamage;
    
    public int GetBaseDamage() { return baseDamage; }
    public int GetCurrentDamage() { return currentDamage; }
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
        
            //mProjectileSkillInstance.UseSkill(projectileSpawner, (collider.GetComponent<Rigidbody>().velocity * -1).normalized);
            if (ParryParticleSystem != null)
            {
                ParryParticleSystem.Emit(30);
            }
            // Recover Stamina on Parry
            OnParry?.Invoke();

            playerController.PlayParryStepSounds();
            //RecoverStamina(playerData.RecoverStaminaOnParry);
            collider.GetComponent<ProjectileBehaviour>().DisableProjectile();
        }
    }

    void ResetBaseValues()
    {
        currentDamage = baseDamage;
        CriticalChance = 0;
    }
    public void ApplyNewWeaponModifier(WeaponModifierSO aMod)
    {
        int modIndex = AllWeaponModifierList.FindIndex((x => x.modifierName == aMod.modifierName));
        if (modIndex !=  -1)
        {
            AllWeaponModifierList[modIndex].modifierLevel++;
           // ApplyAllModifiers();
            AllWeaponModifierList[modIndex].ApplyModifier(playerController);

        }
        else
        {
            Debug.Log("WeaponModifier index not found");
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
        //enemy.TakeDamage(AttackDamage, playerController.gameObject);
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
                //enemy.TakeDamage(sword.GetCurrentDamage());
                Attack(enemy, aWeaponDamagePercentage);
            }
        } 
        ParryProjectile();
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
                //enemy.TakeDamage(sword.GetCurrentDamage());
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
                //enemy.TakeDamage(sword.GetCurrentDamage());
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
                //enemy.TakeDamage(sword.GetCurrentDamage());
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
    public void ApplyAllModifiers()
    {
        ResetBaseValues();
        foreach(WeaponModifierSO modifier in AllWeaponModifierList)
        {
            if (modifier.modifierLevel < 1) continue;
            modifier.ApplyModifier(playerController);
            Debug.Log(modifier.modifierName + $" {modifier.modifierLevel}");
        }
    }

    public WeaponModifierSO GetRandomModifier()
    {
        if (AllWeaponModifierList.Count == 0) return null;
        int index = UnityEngine.Random.Range(0, AllWeaponModifierList.Count);
        return AllWeaponModifierList[index];
    }
    void Start()
    {
        /*
        for (int i = 0; i < AllWeaponModifierList.Count; ++i)
        {
            AllWeaponModifierList[i] = Instantiate(AllWeaponModifierList[i]);

        }
         */
        currentDamage = baseDamage;
        //playerController = GetComponent<PlayerController>();
        //ApplyAllModifiers();
    }
    public void ResetAllModifiers()
    {
        currentDamage = baseDamage;
        for (int i = 0; i < AllWeaponModifierList.Count; ++i)
        {
            AllWeaponModifierList[i].modifierLevel = 0;

        }
    }
    internal float GetAttackStaminaCost()
    {
        return attackStaminaCost;
    }

}
