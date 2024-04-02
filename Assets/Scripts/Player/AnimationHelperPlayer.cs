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
    public void CustomMeleeAttackEvent(float aWeaponDamagePercentage)
    {
        if (controller != null)
        {
            controller.CustomMeleeAttack(aWeaponDamagePercentage);
        }
    }
    public void FirstMeleeAttackEvent(float aWeaponDamagePercentage)
    {
        if (controller != null)
        {
            controller.FirstMeleeAttack(aWeaponDamagePercentage);
        }
    }
    public void SecondMeleeAttackEvent(float aWeaponDamagePercentage)
    {
        if (controller != null)
        {
            controller.SecondMeleeAttack(aWeaponDamagePercentage);
        }
    }
    public void ThirdMeleeAttackEvent(float aWeaponDamagePercentage)
    {
        if (controller != null)
        {
            controller.ThirdMeleeAttack(aWeaponDamagePercentage);
        }
    }
    public void PlayFootStepSoundEvent()
    {

        if (controller != null)
        {
            controller.PlayFootStepSounds();
        }
    }
    public void PlayAttack1SoundEvent()
    {

        if (controller != null)
        {
            controller.PlayAttack1Sounds();
        }
    }
    public void PlayAttack2SoundEvent()
    {

        if (controller != null)
        {
            controller.PlayAttack2Sounds();
        }
    }
    public void PlayAttack3SoundEvent()
    {

        if (controller != null)
        {
            controller.PlayAttack3Sounds();
        }
    }
    public void CustomDashEvent(int aDashForce)
    {
        if (controller != null)
        {
            controller.DashForward(aDashForce);
        }
    }
    public void TurnOnMagicShieldEvent()
    {
        if (controller != null)
        {
            controller.TurnOnShield();
        }
    }
}
