
using UnityEngine;

public class ProjectileSkillSOBase : SkillSOBase, IProjectileSkillStrategy
{
    public virtual void UseSkill(ProjectileSpawner projectileSpawner)
    {
    }

    public virtual void UseSkill(ProjectileSpawner projectileSpawner, Vector3 aDirection)
    {
    }
}
