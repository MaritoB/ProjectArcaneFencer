
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
    StaminaDrainPercentageOnBlock
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
        { StatType.StaminaDrainPercentageOnBlock, "Stamina Drain Percentage on Block" }
    };

    // Método de extensión para obtener el nombre de un stat basado en StatType
    public static string GetStatName(this StatType statType)
    {
        return statNames.TryGetValue(statType, out var name) ? name : "Unknown Stat";
    }
}