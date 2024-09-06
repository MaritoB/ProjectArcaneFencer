using UnityEngine;

public class AttackInfo
{
    public DamageType damageType { get; set; }
    public int damage {  get;  set; }
    public bool isCritical { get;  set; }
    public bool canInterrupt { get; set; }
    public float ailmentChance { get; set; }
    public GameObject Source { get;  set; }
    public AttackInfo(int damage, DamageType aType, bool isCritical, bool canInterrupt, float ailmentChance, GameObject source)
    {
        this.damage = damage;
        this.damageType = aType;
        this.isCritical = isCritical;
        this.canInterrupt = canInterrupt;
        this.ailmentChance = ailmentChance;
        Source = source;
    }
}