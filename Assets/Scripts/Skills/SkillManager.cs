using System.Collections.Generic;
using UnityEngine;

public enum SkillEnum
{
    IceNova,
    ChainLightning,
    Fireball,
    Knockback
}

public class SkillManager : MonoBehaviour
{
    Dictionary<SkillEnum, SkillSOBase> SkillDictionary;
    [SerializeField] SkillSOBase[] SkillArray;

    private void Start()
    {
        SkillDictionary = new Dictionary<SkillEnum, SkillSOBase>();
        foreach (SkillSOBase skill in SkillArray)
        {
            SkillSOBase newSkill = Instantiate(skill);
            newSkill.SetPlayer(transform);
            newSkill.StartSetUp();
            SkillDictionary.Add(newSkill.GetSkillEnum(), newSkill);
        }
    }

    public void UseSkill(SkillEnum skillEnum, Vector3 direction)
    {
        if (SkillDictionary.TryGetValue(skillEnum, out SkillSOBase skill))
        {
            skill.UseSkill(direction);
        }
        else
        {
            Debug.LogWarning($"Skill {skillEnum} not found in the dictionary!");
        }
    }

    public void SetLevel(SkillEnum skillEnum, int level)
    {
        if (SkillDictionary.TryGetValue(skillEnum, out SkillSOBase skill))
        {
            skill.SetSkillLevel(level);
        }
        else
        {
            Debug.LogWarning($"Skill {skillEnum} not found in the dictionary!");
        }
    }
}
