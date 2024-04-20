
using UnityEngine;
[CreateAssetMenu(fileName = "_IceNovaOnSecondStrike", menuName = "WeaponModifiers/IceNovaOnSecondStrike")]
public class IceNovaOnSecondStrike : WeaponModifierSO
{
    public GameObject IceNovaEffectPrefab;
    ParticleSystem IceNovaEffect;
    [SerializeField]
    FMODUnity.EventReference IceNovaSfx;

    
    public float RadiusBase, RadiusMultiplier;
    float currentRadius;


    public int DamageBase, DamageMultiplier;
    int currentDamage;

    public int TriggerChanceBase, TriggerChanceMultiplier;
    int currentTriggerChance;
    [SerializeField]
    LayerMask EnemyLayer;
    PlayerController Player;

    public override void ApplyModifier(PlayerController aPlayer)
    {
        Player = aPlayer;

        if (IceNovaEffect == null)
        {
            if (IceNovaEffectPrefab != null)
            {
                GameObject obj = Instantiate(IceNovaEffectPrefab, aPlayer.transform);
                IceNovaEffect = obj.GetComponent<ParticleSystem>();

            }
            
        }
        aPlayer.sword.OnSecondMeleePerformed -= TryCastIceNova;
        aPlayer.sword.OnSecondMeleePerformed += TryCastIceNova;
        
        currentTriggerChance = TriggerChanceBase + TriggerChanceMultiplier * modifierLevel;
        currentDamage = DamageBase + DamageMultiplier * modifierLevel;
        currentRadius = RadiusBase + RadiusMultiplier * modifierLevel;
        UpdateDescription();
    }
    public override void UpdateDescription()
    {
        base.UpdateDescription();
        modifierDescription = "your Second Strike have " + currentTriggerChance + "% chance to cast Ice Nova. " +
            "Ice Nova Damage : " + currentDamage + ", Radius : " + currentRadius;

    }
    public void TryCastIceNova()
    {
        int number = Random.Range(0, 100);
        if (number < currentTriggerChance)
        {
            AudioManager.instance.PlayOneShot(IceNovaSfx, Player.transform.position);
            IceNovaEffect.Emit(25* modifierLevel);
            IceNovaEffect.transform.position = Player.transform.position;
            Collider[] enemiesColliders = Physics.OverlapSphere(Player.transform.position, currentRadius, EnemyLayer);
            foreach (Collider enemy in enemiesColliders)
            {
                IDamageable damageableEnemy = enemy.GetComponent<IDamageable>();
                if (damageableEnemy != null)
                {
                    damageableEnemy.TakeDamage(currentDamage, null);
                }
            }
        }

    }
}
