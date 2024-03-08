using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBase : MonoBehaviour
{

    public delegate void OnMeleeHitDelegate(Enemy enemy);
    public event OnMeleeHitDelegate OnMeleeHit;
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
    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(currentDamage);
        OnMeleeHit?.Invoke(enemy);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    internal float GetAttackStaminaCost()
    {
        return attackStaminaCost;
    }
}
