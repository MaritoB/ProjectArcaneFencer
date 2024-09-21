using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBlock", menuName = "Player Logic/Block Logic/Block")]
public class PlayerBlock : PlayerBlockSOBase
{
    public float idleBlockingStaminaCost;
    float blockingMovementSpeed;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        player.animator.SetBool("IsBlocking", true) ;
        player.isBlocking = true;
        blockingMovementSpeed = player.playerStats.movementSpeed.GetValue();
        blockingMovementSpeed *=  0.6f;  
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        player.animator.SetBool("IsBlocking", false);
        player.isBlocking = false;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        player.RotateAndCalculateTurnTowardMouse();
        if (!player.UseStamina(idleBlockingStaminaCost * Time.deltaTime)|| player.playerInputActions.Player.Block.WasReleasedThisFrame())
        {
            player.ChangeStateToRun();
        }
 
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        player.HandleMovement(blockingMovementSpeed);
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



