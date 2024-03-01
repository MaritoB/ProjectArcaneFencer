using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill-Projectile-Single",menuName = "Skills/Projectile Skill/Single Projectile")]
public class SingleProjectile : ProjectileSkillSOBase
{
    public override void UseSkill(ProjectileSpawner projectileSpawner)
    {
        projectileSpawner.ShootProjectileForwardFromPool();
    }
    public override void UseSkill(ProjectileSpawner projectileSpawner, Vector3 aDirection)
    {
        projectileSpawner.ShootProjectileToDirectionFromPool(aDirection, projectileSpawner.ShootPosition.position);
    }
}
