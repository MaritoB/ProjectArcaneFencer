using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Player Logic/Movement Logic/Dash ")]
public class PlayerDash  : PlayerMovementBase
{

    [SerializeField]
    private int DashSpeed;
    [SerializeField]
    float DashTime, CurrentDashTime;
    Vector3 DashVelocity;


    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        StartDash();
    }

    private void StartDash()
    {
        player.animator.SetTrigger("Dash");
        player.DashPS.Emit(1);
        CurrentDashTime = DashTime;
        Vector2 aInputVector = player.playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 MovementVector = new Vector3(aInputVector.x, 0, aInputVector.y).normalized;
        MovementVector = Quaternion.Euler(0, player.mCamera.eulerAngles.y, 0) * MovementVector;
        DashVelocity = MovementVector * DashSpeed;
        DashVelocity.y = player.mRigidbody.velocity.y;
        player.mRigidbody.velocity = DashVelocity * Time.deltaTime;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
       // player.RotateTowardMovementVector();
        CurrentDashTime -= Time.deltaTime;
        if(CurrentDashTime < 0)
        {
            player.PlayerStateMachine.ChangeState(player.PlayerRunState);
        }
    }


    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        player.mRigidbody.velocity = DashVelocity * Time.fixedDeltaTime;
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
