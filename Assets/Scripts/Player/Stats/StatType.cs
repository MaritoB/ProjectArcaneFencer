
using System;
using System.Collections.Generic;
public enum StatType
{
    MaxHealth,
    MaxStamina,
    MovementSpeed,
    DashSpeed,
    DashTime,
    DashStaminaCost,
    RecoverStaminaOnParry,
    StaminaRecoveryRate,
    ParryRadius,
    MeleeAttackRadius,
    StaminaDrainPercentageOnBlock,
    PhysicalResistance,
    MagicResistance,
    FireResistance,
    ColdResistance,
    LightningResistance,
    FireballLevel,
    IceNovaLevel,
    ChainLightningLevel,
    KnockbackLevel
}

public static class StatTypeExtensions
{
    // Diccionario para asociar StatType con nombres
    private static readonly Dictionary<StatType, string> statNames = new Dictionary<StatType, string>
    {
        { StatType.MaxHealth, "Maximum Health" },
        { StatType.MaxStamina, "Maximum Stamina" },
        { StatType.MovementSpeed, "Movement Speed" },
        { StatType.DashSpeed, "Dash Speed" },
        { StatType.DashTime, "Dash Time" },
        { StatType.DashStaminaCost, "Dash Stamina Cost" },
        { StatType.RecoverStaminaOnParry, "Recover Stamina on Parry" },
        { StatType.StaminaRecoveryRate, "Stamina Recovery Rate" },
        { StatType.ParryRadius, "Parry Radius" },
        { StatType.MeleeAttackRadius, "Melee Attack Radius" },
        { StatType.StaminaDrainPercentageOnBlock, "Stamina Drain Percentage on Block" },
        { StatType.PhysicalResistance, "Physical Resistance" },
        { StatType.MagicResistance, "Magic Resistance" },
        { StatType.FireResistance, "Fire Resistance" },
        { StatType.ColdResistance, "Cold Resistance" },
        { StatType.LightningResistance, "Lightning Resistance" },
        { StatType.FireballLevel, "Fireball Level" },
        { StatType.IceNovaLevel, "Ice Nova Level" },
        { StatType.ChainLightningLevel, "Chain Lightning Level" },
        { StatType.KnockbackLevel, "Knockback Level" }

    };

    // Método de extensión para obtener el nombre de un stat basado en StatType
    public static string GetStatName(this StatType statType)
    {
        return statNames.TryGetValue(statType, out var name) ? name : "Unknown Stat";
    }
}