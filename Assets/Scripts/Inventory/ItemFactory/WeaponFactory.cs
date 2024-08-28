using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    [SerializeField] private ItemResourceLoader resourceLoader;
    [SerializeField] private int minBaseDamage = 10;
    [SerializeField] private int maxBaseDamage = 20;
    [SerializeField] private float minRadius = 0.5f;
    [SerializeField] private float maxRadius = 1.5f;
    [SerializeField] private int minCriticalChance = 0;
    [SerializeField] private int maxCriticalChance = 30;
    [SerializeField] private int minAttackStaminaCost = 5;
    [SerializeField] private int maxAttackStaminaCost = 20;
    [SerializeField] private int maxModifiers = 4;

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
        itemData.name = "new Sword";
        itemData.isStackable = false;
        itemData.id = "newSword";
        itemData.quantity = 1; 
        itemData.EquipableItemPrefab = weaponPrefab;

        // Set random modifiers
        List<ItemModifierSO> randomModifiers = resourceLoader.GetRandomModifiers(Random.Range(1, maxModifiers));
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
    {
        weaponBase.ConfiguerWeapon(Random.Range(minBaseDamage, maxBaseDamage + 1),
            1,
            Random.Range(minCriticalChance, maxCriticalChance + 1),
            Random.Range(minAttackStaminaCost, maxAttackStaminaCost + 1));
    }
}
