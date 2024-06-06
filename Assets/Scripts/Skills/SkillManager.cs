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
    public void UseIceNova()
    {
        mIceNovaSkill.UseSkill(transform.position);
    }
    public void LevelUpFireball(int aLevel)
    {
        mFireballSkill.LevelUpSkill(aLevel);
    }
    public void ResetSkillLevels()
    {
        mIceNovaSkill.ResetSkillLevel();
        mChainLightningSkill.ResetSkillLevel();
        mFireballSkill.ResetSkillLevel();
    }
    internal void LevelUpIceNova(int aLevel)
    {
        mIceNovaSkill.LevelUpSkill(aLevel);
    }
    internal void LevelUpChainLightning(int aLevel)
    {
        mChainLightningSkill.LevelUpSkill(aLevel);
    }

    internal void UseChainLightningSkill(Vector3 direction)
    {
        mChainLightningSkill.UseSkill(direction);
    }
}
