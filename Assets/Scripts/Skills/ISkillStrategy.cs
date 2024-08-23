

public interface ISkillStrategy
{
    void UseSkill();
    void ResetSkillLevel();
    void SetSkillLevel(int aSkillLevel);
    void UseSkill(UnityEngine.Vector3 APosition);
    void UseSkill(UnityEngine.Vector3 APosition, int SkillLevel);
}