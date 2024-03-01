
using UnityEngine;
[CreateAssetMenu(fileName = "AfterHitEffect-Disable", menuName = "Projectile /After Hit Effects/Disable")]
public class DisableAfterHitEffect : AfterHitEffectSOBase
{
    public override void AfterHitEffect(ProjectileBehaviour projectile)
    {
        projectile.DisableProjectile();
    }
}
