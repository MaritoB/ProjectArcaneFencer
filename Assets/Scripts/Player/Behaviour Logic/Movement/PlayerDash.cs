using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

[CreateAssetMenu(fileName = "Dash", menuName = "Player Logic/Movement Logic/Dash ")]
public class PlayerDash  : PlayerMovementBase
{

    private float CurrentDashTime;
    Vector3 DashVelocity;


    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //player.playerInputActions.Player.Movement.Reset();

        StartDash();
    }
    private void StartDash()
    {
        if(player== null) { return; }
        player.OnDashInvoke();
       // player.animator.SetTrigger("Dash");
        player.animator.SetBool("IsDashing", true);
        player.DashPS.Emit(1);
        CurrentDashTime = player.playerStats.dashTime.GetValue();
        Vector2 aInputVector = player.playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 Direction;
        if (aInputVector == Vector2.zero)
        {
            Direction = player.transform.forward.normalized;
            DashVelocity = Direction * player.playerStats.dashSpeed.GetValue(); 
        }
        else
        {
            Vector3 MovementVector = new Vector3(aInputVector.x, 0, aInputVector.y).normalized;
            MovementVector = Quaternion.Euler(0, player.mCamera.transform.eulerAngles.y, 0) * MovementVector;
            DashVelocity = MovementVector * player.playerStats.dashSpeed.GetValue(); 
            Direction = Quaternion.Euler(0f, player.mCamera.transform.eulerAngles.y, 0f) * MovementVector;
            var rotation = Quaternion.LookRotation(Direction);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, rotation, 100);
        }
        DashVelocity.y = player.mRigidbody.velocity.y;
        player.mRigidbody.velocity = DashVelocity * Time.deltaTime;
        player.isDashing = true;
    }
    private void DashEvent(InputAction.CallbackContext context)
    {
        if (player.DashChanellingPerk)
        {
            if (context.performed && player.TryDash())
            {
                Debug.Log("performed DashEvent");
                player.PlayerStateMachine.ChangeState(player.mPlayerDashState);
            }
        }
        else
        {
            if (context.started && player.TryDash())
            {
                Debug.Log("Started DashEvent");
                player.PlayerStateMachine.ChangeState(player.mPlayerDashState);

            }
        }
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
       // player.RotateTowardMovementVector();

    }


    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        CurrentDashTime -= Time.fixedDeltaTime;
        if (CurrentDashTime < 0)
        {
            player.isDashing = false;

            if (player.DashChanellingPerk)
            {

                if (player.playerInputActions.Player.Dash.IsPressed() && player.TryDash())
                {
                    player.PlayerStateMachine.ChangeState(player.mPlayerDashState);
                    return;
                }
            }

            player.animator.SetBool("IsDashing", false);
            player.PlayerStateMachine.ChangeState(player.mPlayerRunState);
            return;
        }
        player.mRigidbody.velocity = DashVelocity * Time.fixedDeltaTime;
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
