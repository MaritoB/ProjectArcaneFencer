using UnityEngine;

public class AttackInfo
{
    public int damage {  get;  set; }
    public bool isMagic { get;  set; }
    public bool isCritical { get;  set; }
    public GameObject Source { get;  set; }
    public AttackInfo(int damage, bool isMagic, bool isCritical, GameObject source)
    {
        this.damage = damage;
        this.isMagic = isMagic;
        this.isCritical = isCritical;
        Source = source;
    }
}