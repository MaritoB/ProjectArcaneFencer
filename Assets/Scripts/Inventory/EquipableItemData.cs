using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemModifierLevel
{
    public ItemModifierSO ItemModifier;
    public int level;
}

[CreateAssetMenu(menuName = "Item/EquipableItemData")]
public class EquipableItemData : ItemData
{
    public List<ItemModifierLevel> itemModifiers;
    public EquipmentSocket equipSlot;
    public GameObject EquipableItemPrefab;

    private List<ItemModifierSO> activeModifiers = new List<ItemModifierSO>();

    public void Unequip(PlayerController aPlayer)
    {
        foreach (var mod in activeModifiers)
        {
            if (mod is IItemModifier interfaceMod)
            {
                interfaceMod.RemoveModifier(aPlayer);
            }
        }
        activeModifiers.Clear();
    }

    internal void Equip(PlayerController aPlayer)
    {
        foreach (ItemModifierLevel modifier in itemModifiers)
        {
            ItemModifierSO newMod = Instantiate(modifier.ItemModifier);
            newMod.modifierLevel = modifier.level;
            activeModifiers.Add(newMod);

            if (newMod is IItemModifier modifierInterface)
            {
                modifierInterface.ApplyModifier(aPlayer);
            }
        }
    }
}
