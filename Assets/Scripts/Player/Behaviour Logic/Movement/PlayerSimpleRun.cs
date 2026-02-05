using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Simple-Run", menuName = "Player Logic/Movement Logic/Simple Run ")]
public class PlayerSimpleRun : PlayerMovementBase
{
    float movementSpeed;
    //private void HandleMovement(Vector2 aInputVector)
    //{ 
    //    Vector3 movementVector = new Vector3(aInputVector.x, 0, aInputVector.y).normalized;
    //    movementVector = Quaternion.Euler(0, player.mCamera.transform.eulerAngles.y, 0) * movementVector; 
    //    float turnAmount = player.RotateAndCalculateTurnTowardMouse(); 
    //    Vector3 newVelocity = movementVector * movementSpeed * Time.deltaTime;
    //    newVelocity.y = player.mRigidbody.velocity.y;  // Mantener la componente Y actual
    //    player.mRigidbody.velocity = newVelocity; 
    //    player.UpdateAnimator(turnAmount, new  Vector2(aInputVector.x,   aInputVector.y));
    //}

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
        player.playerInputActions.Player.Close.performed += CloseUI;

        movementSpeed = player.playerStats.movementSpeed.GetValue();

    }

    private void CloseUI(InputAction.CallbackContext context)
    {
        UIManager.Instance.HideAll();
        player.inventory.CloseInventory();
    }

    public void InputCleanUp()
    {
        player.playerInputActions.Player.Dash.performed -= DashEvent;
        player.playerInputActions.Player.Dash.started -= DashEvent;
        player.playerInputActions.Player.Attack.started -= AttackEvent;
        player.playerInputActions.Player.Block.performed -= BlockEvent;
        player.playerInputActions.Player.Inventory.performed -= InventoryEvent;
        player.playerInputActions.Player.Close.performed -= CloseUI;
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
        player.inventory.ToggleInventory();
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
        player.HandleMovement(movementSpeed);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
