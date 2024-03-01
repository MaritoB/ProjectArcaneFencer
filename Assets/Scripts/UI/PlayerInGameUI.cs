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
    RectTransform MaxHealth, CurrentHealth, MaxStamina, CurrentStamina;
    private Vector2 MaxHealthSize, MaxStaminaSize;
    private Vector2 CurrentHealthSize, CurrentStaminaSize;
    private void Start()
    {
        animator = GetComponent<Animator>();
        MaxHealthSize = new Vector2(MaxHealth.rect.width, MaxHealth.rect.height);
        MaxStaminaSize = new Vector2(MaxStamina.rect.width, MaxStamina.rect.height);
        CurrentHealthSize = new Vector2(MaxHealth.rect.width, MaxHealth.rect.height);
        CurrentStaminaSize = new Vector2(MaxStamina.rect.width, MaxStamina.rect.height);
    }
    public void FadeIn()
    {
        if(animator == null)
        {
            return;
        }
        animator.SetTrigger("FadeIn");
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
        SceneManager.LoadScene(0);
    }
}
