using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 30f;
    // Agrega aquí otros datos del enemigo, como velocidad, daño, etc.
}
