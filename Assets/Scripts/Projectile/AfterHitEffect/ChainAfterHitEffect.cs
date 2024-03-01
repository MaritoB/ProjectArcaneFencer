
using System.Collections.Generic;
using UnityEngine;
public class ChainTarget
{
    public Collider targetCollider;
    public bool used;
}
[CreateAssetMenu(fileName = "AfterHitEffect-Chain", menuName = "Projectile /After Hit Effects/Chain")]
public class ChainAfterHitEffect : AfterHitEffectSOBase
{
    public int ChainNumber;
    public float ChainRadius;
    int currentChainNumber;
    ChainTarget nearestTarget;
    public Dictionary<Collider, ChainTarget> targetsDictionary = new Dictionary<Collider, ChainTarget>();
    public override void AfterHitEffect(ProjectileBehaviour projectile)
    {
        currentChainNumber--;

        if (currentChainNumber < 0)
        {
            projectile.DisableProjectile();
            return;
        }
        FillTargetsDictionary(projectile);
        GetNearestTarget(projectile.transform.position);
        if (nearestTarget != null)
        {
            projectile.ChangeDirection(nearestTarget.targetCollider.transform.position);
        }
        else
        {
            projectile.DisableProjectile();
        }
    }

    void FillTargetsDictionary(ProjectileBehaviour projectile)
    {
        Collider[] hitColliders = Physics.OverlapSphere(projectile.transform.position, ChainRadius, projectile.TargetLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (!targetsDictionary.ContainsKey(hitCollider))
            {
                targetsDictionary.Add(hitCollider, new ChainTarget { targetCollider = hitCollider, used = false });
            }
        }
    }

    void GetNearestTarget(Vector3 currentPosition)
    {
        nearestTarget = null;
        float minDistance = float.MaxValue;

        foreach (var target in targetsDictionary.Values)
        {
            if (!target.used)
            {
                float distance = Vector3.Distance(currentPosition, target.targetCollider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = target;
                }
            }
        }
        if (nearestTarget != null)
        {
            nearestTarget.used = true;
        }
    }

    public override void ResetAfterHitEffect()
    {
        currentChainNumber = ChainNumber;
        targetsDictionary.Clear();
    }
}
