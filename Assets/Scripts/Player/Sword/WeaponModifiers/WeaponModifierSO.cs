using UnityEngine;

public class WeaponModifierSO : ScriptableObject
{
    public string modifierName;
    public string modifierDescription;

    public virtual void ApplyModifier(SwordBase sword)
    {

    }
    // Aqu� podr�as agregar m�s atributos seg�n tus necesidades
}
