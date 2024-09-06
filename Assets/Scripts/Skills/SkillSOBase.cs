
using UnityEngine;

public class SkillSOBase : ScriptableObject, ISkillStrategy
{
    [SerializeField]
    protected Transform mPlayer;
    protected AttackInfo attackInfo;
    protected CharacterStats mPlayerStats;
    protected int currentSkillLevel; 
    [SerializeField] protected SkillEnum skill;

    public SkillEnum GetSkillEnum()
    {
        return skill;
    }
    public virtual void SetSkillLevel(int aAdditionalLevel)
    {
    }
    public virtual void StartSetUp() { }
    public void SetPlayer(Transform aTransform)
    {
        mPlayer = aTransform;
        mPlayerStats = mPlayer.gameObject.GetComponent<PlayerController>().playerStats;
    } 
    public virtual void UseSkill()
    {
    }

    public virtual void UseSkill(Vector3 APosition)
    {
    }

    public virtual void UseSkill(Vector3 APosition, int SkillLevel)
    { 
    }
}
