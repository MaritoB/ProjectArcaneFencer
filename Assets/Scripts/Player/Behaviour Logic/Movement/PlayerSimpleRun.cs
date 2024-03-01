using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple-Run", menuName = "Player Logic/Movement Logic/Simple Run ")]
public class PlayerSimpleRun : PlayerMovementBase
{

    [SerializeField]
    private int MovementSpeed;

    private void HandleMovement(Vector2 aInputVector)
    {
        Vector3 MovementVector = new Vector3(aInputVector.x, 0, aInputVector.y).normalized;
        MovementVector = Quaternion.Euler(0, player.mCamera.eulerAngles.y, 0) * MovementVector;
        player.RotateTowardMovementVector();
        Vector3 newVelocity = MovementVector * MovementSpeed * Time.deltaTime;
        newVelocity.y = player.mRigidbody.velocity.y;
        player.mRigidbody.velocity = newVelocity;
        player.animator.SetFloat("Velocity", player.mRigidbody.velocity.magnitude);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (player.playerInputActions.Player.Dash.WasPressedThisFrame() && player.TryDash())
        {
            player.PlayerStateMachine.ChangeState(player.PlayerDashState);
        }
        if (player.playerInputActions.Player.Attack.WasPressedThisFrame() && player.TryAttack())
        {
            player.PlayerStateMachine.ChangeState(player.PlayerAttackState);
        }
    }


    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        HandleMovement(player.playerInputActions.Player.Movement.ReadValue<Vector2>());
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
