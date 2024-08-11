using UnityEngine;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth = new Stat(300f);
    public Stat maxStamina = new Stat(100f);
    public Stat movementSpeed = new Stat(200f);
    public Stat dashSpeed = new Stat(500f);
    public Stat dashTime = new Stat(0.2f);
    public Stat dashStaminaCost = new Stat(10f);
    public Stat recoverStaminaOnParry = new Stat(10f);
    public Stat staminaRecoveryRate = new Stat(10f);
    public Stat parryRadius = new Stat(1f);
    public Stat meleeAttackRadius = new Stat(1f);
    public Stat staminaDrainPercentageOnBlock = new Stat(0.5f);

    private Dictionary<StatType, Stat> statMap;
    private Dictionary<EquipSlot, EquipableItemData> equippedItems;

    private void Awake()
    {
        statMap = new Dictionary<StatType, Stat>
        {
            { StatType.MaxHealth, maxHealth },
            { StatType.MaxStamina, maxStamina },
            { StatType.MovementSpeed , movementSpeed },
            { StatType.DashSpeed, dashSpeed },
            { StatType.DashTime, dashTime },
            { StatType.DashStaminaCost, dashStaminaCost },
            { StatType.RecoverStaminaOnParry, recoverStaminaOnParry },
            { StatType.StaminaRecoveryRate, staminaRecoveryRate },
            { StatType.ParryRadius, parryRadius },
            { StatType.MeleeAttackRadius, meleeAttackRadius },
            { StatType.StaminaDrainPercentageOnBlock, staminaDrainPercentageOnBlock }
        };

        equippedItems = new Dictionary<EquipSlot, EquipableItemData>();
    }

    public void EquipItem(EquipableItemData item)
    {
        if (equippedItems.TryGetValue(item.equipSlot, out EquipableItemData currentlyEquippedItem))
        {
            UnequipItem(currentlyEquippedItem);
        }

        equippedItems[item.equipSlot] = item;

        foreach (var modifier in item.statModifiers)
        {
            if (statMap.TryGetValue(modifier.statType, out Stat stat))
            {
                modifier.Apply(stat);
            }
        }
    }

    public void UnequipItem(EquipableItemData item)
    {
        if (equippedItems.ContainsKey(item.equipSlot) && equippedItems[item.equipSlot] == item)
        {
            foreach (var modifier in item.statModifiers)
            {
                if (statMap.TryGetValue(modifier.statType, out Stat stat))
                {
                    modifier.Remove(stat);
                }
            }
            equippedItems.Remove(item.equipSlot);
        }
    }
}
