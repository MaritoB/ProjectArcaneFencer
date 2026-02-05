using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController player;

    public event Action<IGameState> OnGameStateChanged;

    public GameStateMachine StateMachine { get; private set; }

    public InCombatState InCombatState { get; private set; }
    public TradingState TradingState { get; private set; }
    public PauseState PauseState { get; private set; }
    public SafeAreaState SafeAreaState { get; private set; }

    private void Awake()
    { 
        Instance = this; 

        StateMachine = new GameStateMachine();

        InCombatState = new InCombatState(this);
        TradingState = new TradingState(this);
        PauseState = new PauseState(this);
        SafeAreaState = new SafeAreaState(this);
    }

    private void Start()
    {
        ChangeState(SafeAreaState);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void Pause()
    {
        if(StateMachine.CurrentState is PauseState)
        { 
            GameManager.Instance.ChangeState(GameManager.Instance.SafeAreaState);
            player.EnableInputs();
        }
        else
        { 
            GameManager.Instance.ChangeState(GameManager.Instance.PauseState);
            player.DisableInputs();
            
        }
    }

    public void ChangeState(IGameState newState)
    {
        StateMachine.ChangeState(newState);
        OnGameStateChanged?.Invoke(newState);
    }
}
