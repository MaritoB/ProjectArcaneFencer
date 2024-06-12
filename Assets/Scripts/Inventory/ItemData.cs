
using UnityEngine;
public enum ItemType
{
    Weapon,
    Consumable,
    CraftingMaterial,
    Gold
}

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class ItemData : ScriptableObject
{
    public string id;
    public ItemType itemType;
    public string displayName;
    public Sprite icon;
    public int goldPrice;

}