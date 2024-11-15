
using TMPro;
using UnityEngine;

public class PlayerInGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    [SerializeField]
    int RemainingPoints;
    [SerializeField]
    ItemModifierUI itemModifierUI1, itemModifierUI2, itemModifierUI3;
    [SerializeField]
    RectTransform MaxHealth, CurrentHealth, MaxStamina, CurrentStamina;
    [SerializeField]
    TextMeshProUGUI LevelNumberText, RemainingPointsText, HealthText;
    private Vector2 MaxHealthSize, MaxStaminaSize;
    private Vector2 CurrentHealthSize, CurrentStaminaSize;
    PlayerController playerController;
    [SerializeField] RectTransform aWeaponModPanel;
    [SerializeField] Transform mPlayerDeathCamera;
    public void SetRemainingPoints(int aNumber)
    {
        RemainingPoints = aNumber;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealthSize = new Vector2(MaxHealth.rect.width, MaxHealth.rect.height);
        MaxStaminaSize = new Vector2(MaxStamina.rect.width, MaxStamina.rect.height);
        CurrentHealthSize = new Vector2(MaxHealth.rect.width, MaxHealth.rect.height);
        CurrentStaminaSize = new Vector2(MaxStamina.rect.width, MaxStamina.rect.height);
        if (RemainingPoints > 0)
        {
            //SetupSkillUI();

        } 
    }
    public void SetPlayer(PlayerController playerController)
    {
        if (playerController == null) return;
        this.playerController = playerController;
        //SetupSkillUI();
    }
    public void PlayerDeath()
    {
        Time.timeScale = 0.5f;
        mPlayerDeathCamera.gameObject.SetActive(true);
        FadeInResetGame();

    }
    public void TakeDamageUIAnimation()
    {
        if (animator== null)
        {
            return;

        }
        animator.SetTrigger("OnTakeDamage");
    }

    /*
    public void SetupSkillUI()
    {
        if (!aWeaponModPanel.gameObject.activeSelf)
        {
            Time.timeScale = 0f;
            aWeaponModPanel.gameObject.SetActive(true);
        }
        if (playerController == null) return;
        LevelNumberText.text = "Level : " + playerController.CurrentLevel;
        RemainingPointsText.text = " Points : " + RemainingPoints;
        WeaponModifierSO mod1, mod2, mod3;
        mod1 = playerController.inventory.equipmentManager.weapon.GetRandomModifier();
        while (mod1 == null)
        {
            mod1 = playerController.inventory.equipmentManager.weapon.GetRandomModifier();
        }
        mod2 = playerController.inventory.equipmentManager.weapon.GetRandomModifier();
        while (mod1 == mod2 || mod2 == null) {
            mod2 = playerController.inventory.equipmentManager.weapon.GetRandomModifier();
        }
        mod3 = playerController.inventory.equipmentManager.weapon.GetRandomModifier();
        while (mod1 == mod3|| mod2==mod3 || mod3 == null)
        {
            mod3 = playerController.inventory.equipmentManager.weapon.GetRandomModifier();
        }
        itemModifierUI1.UpdateItemModifierUI(mod1, this);
        itemModifierUI2.UpdateItemModifierUI(mod2, this);
        itemModifierUI3.UpdateItemModifierUI(mod3, this);
        aWeaponModPanel.gameObject.SetActive(true);
    }
     */
    public void FadeInResetLevel()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("FadeInResetLevel");
    }
    public void FadeInResetGame()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("FadeInResetGame");
    }
    public void FadeInLoadNextLevel()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("FadeInLoadNextLevel");
    }
    public void FadeInTeleportNextLevel()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("FadeInTeleport");
    }
    public void TeleportPlayer()
    {
        playerController.Teleport();
    }

    public void UpdateCurrentHealthUI(float aCurrentHealth, float aMaxHealth)
    {
        if (aCurrentHealth < 0)
        {
            aCurrentHealth = 0;
        }
        if (CurrentHealth != null)
        {
            float HealthPercentage = aCurrentHealth / aMaxHealth;
            CurrentHealthSize.x = HealthPercentage * MaxHealthSize.x;
            CurrentHealth.sizeDelta = CurrentHealthSize;
            HealthText.text =(int) aCurrentHealth +"/"+ aMaxHealth;
        }
    }
    public void UpdateCurrentStaminaUI(float aCurrentStamina, float aMaxStamina)
    {
        if (aCurrentStamina < 0)
        {
            aCurrentStamina = 0;
        }
        if (CurrentStamina != null)
        {
            float staminaPercentage = aCurrentStamina / aMaxStamina;
            CurrentStaminaSize.x = staminaPercentage * MaxStaminaSize.x;
            CurrentStamina.sizeDelta = CurrentStaminaSize;
        }
    }
    public void ResetLevel()
    {
        Time.timeScale =1f;
        SceneManagerSingleton.Instance.ResetLevel();
    }
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManagerSingleton.Instance.TryLoadNextScene();
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManagerSingleton.Instance.LoadScene(0);
    }
    /*
    internal void ApplyNewWeaponModifier(WeaponModifierSO aMod)
    {
        playerController.inventory.equipmentManager.weapon.ApplyNewWeaponModifier(aMod);
        RemainingPoints--;
        if(RemainingPoints > 0) 
        {
            //SetupSkillUI();
        }
        else{
            CloseModifiersPanel();
        }
    }
     */
    void CloseModifiersPanel()
    {
        aWeaponModPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
