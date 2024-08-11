using UnityEngine;

[CreateAssetMenu(menuName = "Item/ConsumableItemData")]
public class ConsumableItemData : ItemData
{
    public float effectDuration; // Duración del efecto en segundos
    public string effectDescription; // Descripción del efecto
}