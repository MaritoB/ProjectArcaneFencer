using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemModifierUI: MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ItemNameText, ItemDescriptionText, ItemLevelText;
    [SerializeField]
    Image ItemImage;
    WeaponModifierSO modifierSO;
    PlayerInGameUI playerInGameUI;
    public void UpdateItemModifierUI(WeaponModifierSO aModifier, PlayerInGameUI aPlayerInGameUI)
    {
        if (aModifier == null) return;
        ItemNameText.text = aModifier.modifierName;
        ItemDescriptionText.text = aModifier.modifierDescription;
        ItemLevelText.text = aModifier.modifierLevel.ToString();
        ItemImage.sprite = aModifier.modifierSprite;
        modifierSO = aModifier;
        playerInGameUI = aPlayerInGameUI;

    }
    public void OnCickApplyModifier( )
    {
        if ( modifierSO == null  || playerInGameUI == null) return;
        playerInGameUI.AplyNewWeaponModifier(modifierSO);
    }

}