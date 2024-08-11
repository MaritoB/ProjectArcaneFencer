using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public int goldPrice;
    public int quantity;
    public bool isStackable;
    public Color colorMultiplier;
}