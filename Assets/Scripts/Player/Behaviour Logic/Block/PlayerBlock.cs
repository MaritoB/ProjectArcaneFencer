using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBlock", menuName = "Player Logic/Block Logic/Block")]
public class PlayerBlock : PlayerBlockSOBase
{
    public float idleBlockingStaminaCost;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.animator.SetTrigger("BlockStart");
        player.animator.SetFloat("Velocity", 0f);
        player.mRigidbody.velocity = Vector3.zero;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        player.isBlocking = false;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        player.RotateTowardMovementVector();
        player.mRigidbody.velocity = Vector3.zero;
        if (!player.UseStamina(idleBlockingStaminaCost * Time.deltaTime))
        {
            player.TurnOffShield();
        }
        if (player.playerInputActions.Player.Block.WasReleasedThisFrame())
        {
            player.TurnOffShield();
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



