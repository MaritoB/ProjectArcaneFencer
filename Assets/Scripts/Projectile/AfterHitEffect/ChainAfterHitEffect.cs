
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
    [SerializeField]int ChainNumber;
    [SerializeField]float ChainRadius;
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
            Vector3 NewDirection = nearestTarget.targetCollider.transform.position;
            NewDirection.y = projectile.transform.position.y;
            projectile.ChangeDirection(NewDirection);
        }
        else
        {
            projectile.DisableProjectile();
        }
    }
    public void LevelUpEffect(int chainNumber, float chainRadius)
    {
        ChainNumber  = chainNumber;
        ChainRadius = chainRadius;
    }

    void FillTargetsDictionary(ProjectileBehaviour projectile)
    {
        Collider[] hitColliders = Physics.OverlapSphere(projectile.transform.position, ChainRadius, projectile.TargetLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if ((hitCollider.transform.position - projectile.transform.position).magnitude<0.5f) continue;
            if (!targetsDictionary.ContainsKey(hitCollider))
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if(enemy != null && enemy.IsAlive)
                {
                    targetsDictionary.Add(hitCollider, new ChainTarget { targetCollider = hitCollider, used = false });

                }
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
