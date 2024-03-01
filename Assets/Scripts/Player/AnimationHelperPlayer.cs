using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelperPlayer : MonoBehaviour
{
    [SerializeField]
    PlayerController controller;
    public void ShootProjectileEvent()
    {
        if (controller != null)
        {
            controller.ShootProjectile();
        }
    }
    public void SimpleMeleeAttackEvent()
    {
        if (controller != null)
        {
            controller.SimpleMeleeAttack();
        }
    }

    public void ChangeToRunStateEvent()
    {
        if (controller != null)
        {
            controller.ChangeStateToRun();
        }
    }
}
