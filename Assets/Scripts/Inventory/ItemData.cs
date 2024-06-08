
using UnityEngine;
public enum ItemSlotType
{
    SWORD
}

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class ItemData : ScriptableObject
{
    public string id;
    public ItemSlotType itemType;
    public string displayName;
    public Sprite icon;
    public int goldPrice;

}