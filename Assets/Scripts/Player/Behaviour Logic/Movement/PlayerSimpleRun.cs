using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Simple-Run", menuName = "Player Logic/Movement Logic/Simple Run ")]
public class PlayerSimpleRun : PlayerMovementBase
{
    private void HandleMovement(Vector2 aInputVector)
    {
        Vector3 MovementVector = new Vector3(aInputVector.x, 0, aInputVector.y).normalized;
        MovementVector = Quaternion.Euler(0, player.mCamera.eulerAngles.y, 0) * MovementVector;
        player.RotateTowardMovementVector();
        Vector3 newVelocity = MovementVector * player.playerData.MovementSpeed * Time.deltaTime;
        newVelocity.y = player.mRigidbody.velocity.y;
        player.mRigidbody.velocity = newVelocity;
        player.animator.SetFloat("Velocity", player.mRigidbody.velocity.magnitude);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        InputSetup();

    }
    public void InputSetup()
    {
        player.playerInputActions.Player.Dash.performed += DashEvent;
        player.playerInputActions.Player.Dash.started += DashEvent;
        player.playerInputActions.Player.Attack.started += AttackEvent;
        player.playerInputActions.Player.Block.performed += BlockEvent;
        player.playerInputActions.Player.Inventory.performed += InventoryEvent;

    }
    public void InputCleanUp()
    {
        player.playerInputActions.Player.Dash.performed -= DashEvent;
        player.playerInputActions.Player.Dash.started -= DashEvent;
        player.playerInputActions.Player.Attack.started -= AttackEvent;
        player.playerInputActions.Player.Block.performed -= BlockEvent;
        player.playerInputActions.Player.Inventory.performed -= InventoryEvent;
    }
    private void BlockEvent(InputAction.CallbackContext context)
    {
        if (!player.isBlocking)
        {
            player.PlayerStateMachine.ChangeState(player.mPlayerBlockState);
        }
    }
    private void InventoryEvent(InputAction.CallbackContext context)
    {
        player.inventory.ToggleInventoryOnToEquip();
    }

    private void AttackEvent(InputAction.CallbackContext context)
    {
        if(player.TryAttack())
        {
            player.PlayerStateMachine.ChangeState(player.mPlayerAttackState);
        }
    }

    private void DashEvent(InputAction.CallbackContext context)
    {
        if (player.DashChanellingPerk)
        {
            if (context.performed && player.TryDash())
            {
                Debug.Log("SimpleRun Performed");
                player.PlayerStateMachine.ChangeState(player.mPlayerDashState);
            }
        }
        else
        {
            if (context.started && player.TryDash())
            {
                Debug.Log("SimpleRun Started");
                player.PlayerStateMachine.ChangeState(player.mPlayerDashState);
                
            }
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        InputCleanUp();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        if (player.playerInputActions.Player.Block.IsPressed())
        {
            if (!player.isBlocking)
            {
                player.PlayerStateMachine.ChangeState(player.mPlayerBlockState);
            }
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
