
using UnityEngine;
[CreateAssetMenu(fileName = "AfterHitEffect-Pierce", menuName = "Projectile /After Hit Effects/Pierce")]
public class PierceAfterHitEffect : AfterHitEffectSOBase
{
    public int PierceNumber;
    int currentPierceNumber;

    public override void AfterHitEffect(ProjectileBehaviour projectile)
    {
        currentPierceNumber--;
        if (currentPierceNumber < 0)
        {
            projectile.DisableProjectile();
        }
    }
    public override void ResetAfterHitEffect()
    {
        currentPierceNumber = PierceNumber;
    }
}
