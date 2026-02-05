using UnityEngine;

public class TradingState : IGameState
{
    private GameManager gameManager;

    public TradingState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Enter Trading State");
        Time.timeScale = 0f;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        Debug.Log("Exit Trading State");
    }
}