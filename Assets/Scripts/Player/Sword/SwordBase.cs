using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBase : MonoBehaviour
{

    public delegate void OnMeleeHitDelegate(Enemy enemy);
    public event OnMeleeHitDelegate OnFirstMeleeHit,OnSecondMeleeHit, OnThirdMeleeHit;
    public List<WeaponModifierSO> weaponModifierList;
    
    [SerializeField]int baseDamage;

    //[SerializeField] float attackSpeed;

    [SerializeField] int attackStaminaCost;

    [SerializeField] int currentDamage;
    public int GetBaseDamage() { return baseDamage; }
    public int GetCurrentDamage() { return currentDamage; }
    public void AddDamage(int aDamage)
    {
        if (aDamage < 0)
        {
            return;
        }
        currentDamage += aDamage;
    } 
    void ResetBaseValues()
    {
        currentDamage = baseDamage;
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
    public void ApplyAllModifiers()
    {
        ResetBaseValues();
        foreach(WeaponModifierSO modifier in weaponModifierList)
        {
            modifier.ApplyModifier(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ApplyAllModifiers();
    }
    internal float GetAttackStaminaCost()
    {
        return attackStaminaCost;
    }
}
