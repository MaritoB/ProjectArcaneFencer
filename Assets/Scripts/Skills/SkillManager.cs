using System;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] SkillSOBase mIceNovaSkill;
    [SerializeField] SkillSOBase mChainLightningSkill;
    [SerializeField] SkillSOBase mFireballSkill;

    private void Start()
    {
        mIceNovaSkill = Instantiate(mIceNovaSkill);
        mChainLightningSkill = Instantiate(mChainLightningSkill);
        mFireballSkill = Instantiate(mFireballSkill);
        mFireballSkill.SetPlayer(transform);
        mIceNovaSkill.SetPlayer(transform);
        mChainLightningSkill.SetPlayer(transform);
        mChainLightningSkill.StartSetUp();
        mFireballSkill.StartSetUp();
    }

    public void ResetSkillLevels()
    {
        mIceNovaSkill.ResetSkillLevel();
        mChainLightningSkill.ResetSkillLevel();
        mFireballSkill.ResetSkillLevel();
    }
    public void UseIceNova(int aSkillLevel)
    {
        mIceNovaSkill.UseSkill(transform.position, aSkillLevel);
    }
    public void UseFireball(Vector3 aDirection)
    {
        mFireballSkill.UseSkill(aDirection);
    }
    internal void UseChainLightningSkill(Vector3 direction)
    {
        mChainLightningSkill.UseSkill(direction);
    }
    public void SetLevelFireball(int aLevel)
    {
        mFireballSkill.SetSkillLevel(aLevel);
    }
 /*
    internal void LevelUpIceNova(int aLevel)
    {
        mIceNovaSkill.LevelUpSkill(aLevel);
    }
    internal void LevelUpChainLightning(int aLevel)
    {
        mChainLightningSkill.LevelUpSkill(aLevel);
    }
  

  */
}
