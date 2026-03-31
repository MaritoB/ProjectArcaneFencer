using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] private ItemResourceLoader resourceLoader;
    [SerializeField] private int minBaseDamage = 10;
    [SerializeField] private int maxBaseDamage = 20;
    [SerializeField] private float minPower = 0.3f;
    [SerializeField] private float maxPower = 1f;
    [SerializeField] private float minAttackSpeed = -0.5f;
    [SerializeField] private float maxAttackSpeed = 1.5f;
    [SerializeField] private float minRadius = 0.5f;
    [SerializeField] private float maxRadius = 1.5f;
    [SerializeField] private int minCriticalChance = 0;
    [SerializeField] private int maxCriticalChance = 30;
    [SerializeField] private int minAttackStaminaCost = 30;
    [SerializeField] private int maxAttackStaminaCost = 50;
    [SerializeField] private int maxModifiers = 6;
    [SerializeField] private int GoldModifier = 17;
    private int GoldPrice = 0;

    [SerializeField]  Material FireEffect, ColdEffect, LightningEffect, MagicEffect, PhysicEffect;

    public EquipableItemData CreateWeaponItemData()
    {
        if (resourceLoader == null)
        {
            Debug.LogError("ItemResourceLoader is not assigned.");
            return null;
        }

        // Create EquipableItemData instance
        EquipableItemData itemData = ScriptableObject.CreateInstance<EquipableItemData>();
        if (itemData == null)
        {
            Debug.LogError("Failed to create EquipableItemData instance.");
            return null;
        }

        // Configure weapon prefab and modifiers
        GameObject weaponPrefab = resourceLoader.GetRandomWeaponPrefab();
        if (weaponPrefab == null)
        {
            Debug.LogError("No weapon prefab found.");
            return null;
        }
        ModifierType aType = (ModifierType)Random.Range(0, 6);
        itemData.name = "new Sword";
        itemData.isStackable = false;
        itemData.id = "newSword";
        itemData.quantity = 1;
        itemData.EquipableItemPrefab = Instantiate(weaponPrefab);
        WeaponBase weapon = itemData.EquipableItemPrefab.GetComponent<WeaponBase>(); 
        if( weapon != null)
        {
             
            switch (aType)
            {
                case ModifierType.Fire:
                    weapon.ChangeEffectMaterial( FireEffect);
                    itemData.displayName = "Flame";
                    break;
                case ModifierType.Cold:
                    weapon.ChangeEffectMaterial(ColdEffect);
                    itemData.displayName = "Ice";
                    break;
                case ModifierType.Lightning:
                    weapon.ChangeEffectMaterial(LightningEffect);
                    itemData.displayName = "Light";
                    break;
                case ModifierType.Physic:
                    weapon.ChangeEffectMaterial(PhysicEffect);
                    itemData.displayName = "Brutal";
                    break;
                case ModifierType.Magic:
                    weapon.ChangeEffectMaterial(MagicEffect);
                    itemData.displayName = "Astral ";
                    break;
                default:
                    weapon.ChangeEffectMaterial(null) ;
                    break;
            }

        }
        // Set random modifiers
        int nModifiers = Random.Range(1, maxModifiers);
        List<ItemModifierSO> randomModifiers = resourceLoader.GetRandomModifiers(nModifiers,aType);
        itemData.goldPrice = nModifiers * GoldModifier;
        itemData.itemModifiers = CreateItemModifierLevels(randomModifiers);

        // Example values
        itemData.equipSlot = EquipmentSocket.Weapon; // Change as necessary

        return itemData;
    }

    private List<ItemModifierLevel> CreateItemModifierLevels(List<ItemModifierSO> modifiers)
    {
        List<ItemModifierLevel> modifierLevels = new List<ItemModifierLevel>();

        foreach (ItemModifierSO modifier in modifiers)
        {
            ItemModifierLevel modifierLevel = new ItemModifierLevel
            {
                ItemModifier = modifier,
                level = Random.Range(1, 6) // Random level between 1 and 5
            };
            modifierLevels.Add(modifierLevel);
        }

        return modifierLevels;
    }

    public EquipableItemData CreateWeapon()
    {
        EquipableItemData itemData = CreateWeaponItemData();
        if (itemData == null)
        {
            return null;
        }
        ConfigureWeaponBase(itemData.EquipableItemPrefab.GetComponent<WeaponBase>());

        return itemData;
    }

    private void ConfigureWeaponBase(WeaponBase weaponBase)
    { /*
        int baseDmg = Random.Range(minBaseDamage, maxBaseDamage);
        float percentage = baseDmg / maxBaseDamage;
        float attackSpeed = Random.Range(minAttackSpeed, maxAttackSpeed);
        attackSpeed /= percentage;
        float radius = Random.Range(minRadius, maxRadius);
        radius *= percentage;
        int staminaCost = Random.Range(minAttackStaminaCost, maxAttackStaminaCost);
        int Crit = Random.Range(minCriticalChance, maxCriticalChance); 
        staminaCost = (int)(staminaCost * percentage);
        weaponBase.ConfiguerWeapon(baseDmg, radius, attackSpeed, (int)Crit, staminaCost) ;
       */
        float powerBudget = Random.Range(minPower, maxPower);

        // Distribución de poder
        float dmgWeight = Random.Range(0.3f, 0.5f);
        float speedWeight = Random.Range(0.2f, 0.4f);
        float radiusWeight = Mathf.Clamp01(1f - dmgWeight - speedWeight);

        // Aplicar presupuesto
        float damage01 = powerBudget * dmgWeight;
        float speed01 = powerBudget * speedWeight;
        float radius01 = powerBudget * radiusWeight;

        // Convertir a valores reales
        int damage = Mathf.RoundToInt(
            Mathf.Lerp(minBaseDamage, maxBaseDamage, damage01)
        );

        float attackSpeed = Mathf.Lerp(
            maxAttackSpeed, // inverso!
            minAttackSpeed,
            speed01
        );

        float radius = Mathf.Lerp(minRadius, maxRadius, radius01);

        int staminaCost = Mathf.RoundToInt(
            Mathf.Lerp(minAttackStaminaCost, maxAttackStaminaCost, dmgWeight)
        );

        int critChance = Random.Range(minCriticalChance, maxCriticalChance);

        weaponBase.ConfiguerWeapon(damage, radius, attackSpeed, critChance, staminaCost);
    }

}
