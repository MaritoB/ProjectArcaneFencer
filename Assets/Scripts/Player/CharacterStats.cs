using UnityEngine;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth = new Stat(300f);
    public Stat maxStamina = new Stat(100f);
    public Stat movementSpeed = new Stat(200f);
    public Stat dashSpeed = new Stat(500f);
    public Stat dashTime = new Stat(0.3f);
    public Stat dashStaminaCost = new Stat(10f);
    public Stat recoverStaminaOnParry = new Stat(10f);
    public Stat staminaRecoveryRate = new Stat(10f); 
    public Stat staminaDrainPercentageOnBlock = new Stat(0.5f);

    private Dictionary<StatType, Stat> statMap;

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
            { StatType.StaminaDrainPercentageOnBlock, staminaDrainPercentageOnBlock }
        };
    }

    public void ApplyModifier(StatModifier modifier)
    {
        if (statMap.TryGetValue(modifier.statType, out Stat stat))
        {
            modifier.Apply(stat); 
        }
    }

    public void RemoveModifier(StatModifier modifier)
    {
        if (statMap.TryGetValue(modifier.statType, out Stat stat))
        {
            modifier.Remove(stat);
        }
    }
}
