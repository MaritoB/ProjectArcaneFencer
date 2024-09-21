using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Attack-Melee Parry Projectile", menuName = "Player Logic/Attack Logic/Melee Parry Projectile")]
public class PlayerAttackMeleeParryProjectile : PlayerAttackSOBase
{
    public float AttackDashForce;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.animator.SetTrigger("Attack");
        player.animator.SetFloat("Velocity", 0f);
        player.mRigidbody.velocity = Vector3.zero;
        player.isAttacking = true;
        player.playerInputActions.Player.Attack.started += AttackEvent;

        //player.mRigidbody.AddForce(player.transform.forward *  AttackDashForce, ForceMode.Impulse);
    }

    private void AttackEvent(InputAction.CallbackContext context)
    {
        if(player.TryAttack())
        {
            player.PlayerStateMachine.ChangeState(player.mPlayerAttackState);
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        player.playerInputActions.Player.Attack.started -= AttackEvent;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        player.RotateAndCalculateTurnTowardMouse();
        //aaplayer.RotateTowardMovementVector();
        //player.mRigidbody.velocity = Vector3.zero;

        if (player.playerInputActions.Player.Attack.WasPressedThisFrame() && player.TryAttack())
        {
            player.PlayerStateMachine.ChangeState(player.mPlayerAttackState);
        }

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }
    public override void Initialize(GameObject aGameObject, PlayerController aPlayer)
    {
        base.Initialize(aGameObject, aPlayer);
    }
    public override void ResetValues()
    {
        base.ResetValues();
    }

}



