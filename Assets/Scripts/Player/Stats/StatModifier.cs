using UnityEngine;

[CreateAssetMenu(fileName = "NewStatModifier", menuName = "Stats/StatModifier")]
public class StatModifier : ScriptableObject
{
    public StatType statType;
    public float value;

    public string GetDescription(int aModifierLevel)
    {
        return "Increase " + value * aModifierLevel + " to " + StatTypeExtensions.GetStatName(statType) + ".";
    }

    public void Apply(Stat stat)
    {
        stat.AddModifier(value);
        Debug.Log("Adding " + value + " to " + StatTypeExtensions.GetStatName(statType) + ".");
    }

    public void Remove(Stat stat)
    {
        stat.RemoveModifier(value);
    }
}