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
    public Stat physicalResistance = new Stat(0);
    public Stat magicResistance = new Stat(0);
    public Stat fireResistance = new Stat(0);
    public Stat coldResistance = new Stat(0);
    public Stat lightningResistance = new Stat(0);
    public Stat fireballLevel = new Stat(0);
    public Stat iceNovaLevel = new Stat(0);
    public Stat chainLightningLevel = new Stat(0);
    public Stat KnockbackLevel = new Stat(0);

    private Dictionary<StatType, Stat> statMap;
    private Dictionary<DamageType, StatType> ResistanceDictionary;


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
            { StatType.StaminaDrainPercentageOnBlock, staminaDrainPercentageOnBlock },
            { StatType.PhysicalResistance,physicalResistance },
            { StatType.MagicResistance, magicResistance },
            { StatType.FireResistance, fireResistance },
            { StatType.ColdResistance,coldResistance },
            { StatType.LightningResistance,lightningResistance},
            { StatType.FireballLevel, fireballLevel },
            { StatType.IceNovaLevel, iceNovaLevel },
            { StatType.ChainLightningLevel, chainLightningLevel },
            { StatType.KnockbackLevel, KnockbackLevel }
        };
        ResistanceDictionary = new Dictionary<DamageType, StatType> {
            { DamageType.PHYSICAL, StatType.PhysicalResistance },
            { DamageType.MAGIC, StatType.MagicResistance },
            { DamageType.FIRE,  StatType.FireResistance },
            { DamageType.COLD, StatType.ColdResistance },
            { DamageType.LIGHTNING,  StatType.LightningResistance}
        };
    }

    public void ApplyModifier(StatModifier modifier)
    {
        if (statMap.TryGetValue(modifier.statType, out Stat stat))
        {
            modifier.Apply(stat); 
        }
    }
    public float GetResistance(DamageType aDamageType)
    {
        StatType aStatType;
        if (ResistanceDictionary.TryGetValue(aDamageType, out aStatType))
        {
            Stat stat;
            if (statMap.TryGetValue(aStatType, out stat)){
                return stat.GetValue();
            }
        }
        return 0f;
    }

    public void RemoveModifier(StatModifier modifier)
    {
        if (statMap.TryGetValue(modifier.statType, out Stat stat))
        {
            modifier.Remove(stat);
        }
    }
    public Dictionary<StatType, Stat> GetStats()
    {
        return statMap;
    }
}
