using UnityEngine;

public class WeaponModifierSO : ScriptableObject
{
    public string modifierName;
    public string modifierDescription;

    public virtual void ApplyModifier(SwordBase sword)
    {

    }
    // Aquí podrías agregar más atributos según tus necesidades
}
