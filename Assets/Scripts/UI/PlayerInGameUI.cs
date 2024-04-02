using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    [SerializeField]
    ItemModifierUI itemModifierUI1, itemModifierUI2, itemModifierUI3;
    [SerializeField]
    RectTransform MaxHealth, CurrentHealth, MaxStamina, CurrentStamina;
    private Vector2 MaxHealthSize, MaxStaminaSize;
    private Vector2 CurrentHealthSize, CurrentStaminaSize;
    PlayerController playerController;
    [SerializeField] RectTransform aWeaponModPanel;
    private void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealthSize = new Vector2(MaxHealth.rect.width, MaxHealth.rect.height);
        MaxStaminaSize = new Vector2(MaxStamina.rect.width, MaxStamina.rect.height);
        CurrentHealthSize = new Vector2(MaxHealth.rect.width, MaxHealth.rect.height);
        CurrentStaminaSize = new Vector2(MaxStamina.rect.width, MaxStamina.rect.height);
        
    }
    public void SetPlayer(PlayerController playerController)
    {
        if (playerController == null) return;
        this.playerController = playerController;
        SetupSkillUI();
    }
    public void SelectModifier(WeaponModifierSO aModifier)
    {
    }

    public void SetupSkillUI()
    {
        if (playerController == null) return;
        WeaponModifierSO mod1, mod2, mod3;
        mod1 = playerController.sword.GetRandomModifier();
        while (mod1 == null)
        {
            mod1 = playerController.sword.GetRandomModifier();
        }
        mod2 = playerController.sword.GetRandomModifier();
        while (mod1 == mod2 || mod2 == null) {
            mod2 = playerController.sword.GetRandomModifier();
        }
        mod3 = playerController.sword.GetRandomModifier();
        while (mod1 == mod3|| mod2==mod3 || mod3 == null)
        {
            mod3 = playerController.sword.GetRandomModifier();
        }
        itemModifierUI1.UpdateItemModifierUI(mod1, this);
        itemModifierUI2.UpdateItemModifierUI(mod2, this);
        itemModifierUI3.UpdateItemModifierUI(mod3, this);
    }
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
        SceneManagerSingleton.Instance.ResetLevel();
    }
    public void LoadNextLevel()
    {
        SceneManagerSingleton.Instance.TryLoadNextScene();
    }
    public void ResetGame()
    {
        SceneManagerSingleton.Instance.LoadScene(0);
    }

    internal void AplyNewWeaponModifier(WeaponModifierSO aMod)
    {
        playerController.sword.AplyNewWeaponModifier(aMod);
        aWeaponModPanel.gameObject.SetActive(false);
    }
}
