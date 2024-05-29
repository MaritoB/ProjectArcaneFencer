

public interface ISkillStrategy
{
    void UseSkill();
    void ResetSkillLevel();
    void LevelUpSkill(int aAdditionalLevel);
    void UseSkill(UnityEngine.Vector3 APosition);
}