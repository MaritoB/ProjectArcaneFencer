using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Melee Parry Projectile", menuName = "Player Logic/Attack Logic/Melee Parry Projectile")]
public class PlayerAttackMeleeParryProjectile : PlayerAttackSOBase
{
    public float AttackDashForce;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.animator.SetTrigger("Attack1");
        player.animator.SetFloat("Velocity", 0f);
        player.mRigidbody.velocity = Vector3.zero;
        player.Attacking = true;
        //player.mRigidbody.AddForce(player.transform.forward *  AttackDashForce, ForceMode.Impulse);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        player.RotateTowardMovementVector();
        //player.mRigidbody.velocity = Vector3.zero;

        if (player.playerInputActions.Player.Attack.WasPressedThisFrame() && player.TryAttack())
        {
            player.PlayerStateMachine.ChangeState(player.PlayerAttackState);
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



