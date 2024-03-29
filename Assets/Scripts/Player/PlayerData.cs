using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public float MaxHealth = 300;
    public float MaxStamina = 100;
    public int MovementSpeed = 200;
    public float DashSpeed = 1000;
    public float DashTime = 0.15f;
    public int DashStaminaCost, RecoverStaminaOnParry;
    public float StaminaRecoveryRate;
    public float ParryRadius, MeleeAttackRadius, StaminaDrainPercentajeOnBlock;

}
