using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "PlayerSoundData", menuName = "Player Sound Data")]
public class PlayerSoundData : ScriptableObject
{
    public EventReference PlayerAttack1, PlayerAttack2, PlayerAttack3,
        PlayerOnHit, PlayerDeath, PlayerDash, PlayerShield, PlayerParry, PlayerStep; 
}
