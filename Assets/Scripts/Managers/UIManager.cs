using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] public GameObject combatUI;
    [SerializeField] public GameObject pauseUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private PlayerInGameUI PlayerInGameUI;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 

    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }
    public void OpenInvetory()
    {
        inventoryUI.SetActive(true);
    }
    public void CloseInventory()
    { 
         inventoryUI.SetActive(false);  
    }
    public void ActivatePauseUI()
    {
        pauseUI.SetActive(true);
    }
    public void DisablePauseUI()
    {
        pauseUI.SetActive(false);
    }

    private void HandleGameStateChanged(IGameState state)
    {
        //combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        skillTreeUI.SetActive(false); 
       if (state is TradingState)
            inventoryUI.SetActive(true); 
    }

    public void HideAll()
    {
        
        if( !inventoryUI.activeSelf && !skillTreeUI.activeSelf)
        {
            GameManager.Instance.Pause();
        }
         
        //combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        skillTreeUI.SetActive(false);
    }

}
