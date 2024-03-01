
using UnityEngine;

public interface IProjectileAfterHitEffect 
{
    void AfterHitEffect(ProjectileBehaviour projectile);
    void ResetAfterHitEffect();
}
