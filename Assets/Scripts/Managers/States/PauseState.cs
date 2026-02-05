using UnityEngine;

public class PauseState : IGameState
{
    private GameManager gameManager;

    public PauseState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Game Paused");
        UIManager.Instance.ActivatePauseUI();
        Time.timeScale = 0f;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        UIManager.Instance.DisablePauseUI();
        Debug.Log("Game Resumed");
    }
}