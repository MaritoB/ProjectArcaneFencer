
using UnityEngine;
public interface IProjectileSkillStrategy
{
    void UseSkill(ProjectileSpawner projectileSpawner);
    void UseSkill(ProjectileSpawner projectileSpawner, Vector3 aDirection);

}