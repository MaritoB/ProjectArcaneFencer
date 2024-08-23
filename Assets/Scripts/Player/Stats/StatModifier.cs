using UnityEngine;

[CreateAssetMenu(fileName = "NewStatModifier", menuName = "Stats/StatModifier")]
public class StatModifier : ScriptableObject
{
    public StatType statType;
    public float value;

    public string GetDescription()
    {
        return "Increase " + value + " to " + StatTypeExtensions.GetStatName(statType) + ".";
    }

    public void Apply(Stat stat)
    {
        stat.AddModifier(value);
    }

    public void Remove(Stat stat)
    {
        stat.RemoveModifier(value);
    }
}