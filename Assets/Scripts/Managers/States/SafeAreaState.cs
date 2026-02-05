using UnityEngine;

public class SafeAreaState : IGameState
{
    private GameManager gameManager;

    public SafeAreaState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Enter Safe Area");
        Time.timeScale = 1f;
    }

    public void Update()
    {
    }

    public void Exit()
    {
        Debug.Log("Exit Safe Area");
    }
}