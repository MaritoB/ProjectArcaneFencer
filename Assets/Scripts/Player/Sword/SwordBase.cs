using System.Collections.Generic;
using UnityEngine;

public class SwordBase : MonoBehaviour
{

    public delegate void OnMeleeHitDelegate(Enemy enemy);
    public delegate void OnMeleePerformedDelegate();
    public event OnMeleeHitDelegate OnFirstMeleeHit, OnSecondMeleeHit, OnThirdMeleeHit;
    public event OnMeleePerformedDelegate OnFirstMeleePerformed, OnSecondMeleePerformed, OnThirdMeleePerformed;
    public List<WeaponModifierSO> AllWeaponModifierList;
    PlayerController playerController;
    
    [SerializeField]int baseDamage;
    //[SerializeField] float attackSpeed;

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

    void ResetBaseValues()
    {
        currentDamage = baseDamage;
    }
    public void AplyNewWeaponModifier(WeaponModifierSO aMod)
    {
        int modIndex = AllWeaponModifierList.FindIndex((x => x.modifierName == aMod.modifierName));
        if (modIndex !=  -1)
        {
            AllWeaponModifierList[modIndex].modifierLevel++;
            ApplyAllModifiers();

        }
        else
        {
            Debug.Log("WeaponModifier index not found");
        }
    }
    public void Attack(Enemy enemy, float aWeaponDamagePercentage)
    {
        enemy.TakeDamage((int)(currentDamage * aWeaponDamagePercentage));
    }
    public void CustomAttack(Enemy enemy, int Damage)
    {
        enemy.TakeDamage(Damage);
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
        playerController = GetComponent<PlayerController>();

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
