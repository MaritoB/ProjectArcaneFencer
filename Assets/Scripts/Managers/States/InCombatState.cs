using UnityEngine;

public class InCombatState : IGameState
{
    private GameManager gameManager;

    public InCombatState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Enter Combat State");
        Time.timeScale = 1f;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        Debug.Log("Exit Combat State");
    }
}