using System.Collections.Generic;
using UnityEngine;
public class Stat
{
    private float baseValue;
    private List<float> modifiers = new List<float>();
    private bool isDirty = true; 
    private float cachedValue; 

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
        isDirty = true;
    } 

    public void AddModifier(float value)
    {
        modifiers.Add(value);
        isDirty = true; 
    }

    public void RemoveModifier(float value)
    {
        modifiers.Remove(value); 
        isDirty = true; 
    }

    public float GetValue()
    {
        if (isDirty)
        {
            RecalculateValue();
        }
        return cachedValue;
    }

    private void RecalculateValue()
    {
        float total = baseValue;
        foreach (var modifier in modifiers)
        {
            total += modifier;
        }
        cachedValue = total;
        isDirty = false; 
    }

    public float BaseValue
    {
        get { return baseValue; }
        set
        {
            if (baseValue != value)
            {
                baseValue = value;
                isDirty = true; 
            }
        }
    }
}
