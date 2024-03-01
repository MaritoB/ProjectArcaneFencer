
using UnityEngine;

[CreateAssetMenu(fileName = "Skill-Projectile-Triple",menuName = "Skills/Projectile Skill/Triple Projectile")]
public class TripleProjectile : ProjectileSkillSOBase
{
    public override void UseSkill(ProjectileSpawner projectileSpawner)
    {
        Transform shootPosition = projectileSpawner.ShootPosition;
        projectileSpawner.ShootProjectileToDirectionFromPool((Quaternion.Euler(0, 15 , 0) * shootPosition.forward), shootPosition.position + Vector3.forward * 0.4f);
        projectileSpawner.ShootProjectileToDirectionFromPool(projectileSpawner.ShootPosition.forward, projectileSpawner.ShootPosition.position);
        projectileSpawner.ShootProjectileToDirectionFromPool((Quaternion.Euler(0, -15, 0) * projectileSpawner.ShootPosition.forward), projectileSpawner.ShootPosition.position - Vector3.forward * 0.4f);
    }
}
