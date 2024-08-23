
using UnityEngine;

public class SkillSOBase : ScriptableObject, ISkillStrategy
{
    [SerializeField]
    protected int skillLevel;
    [SerializeField]
    protected Transform mPlayer;

    public virtual void SetSkillLevel(int aAdditionalLevel)
    {
    }
    public virtual void StartSetUp() { }
    public void SetPlayer(Transform aTransform)
    {
        mPlayer = aTransform;
    }

    public void ResetSkillLevel()
    {
        skillLevel = 0; 
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
