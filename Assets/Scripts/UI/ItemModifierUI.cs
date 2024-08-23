using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemModifierUI: MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ItemNameText, ItemDescriptionText, ItemLevelText;
    [SerializeField]
    Image ItemImage;
    ItemModifierSO modifierSO;
    PlayerInGameUI playerInGameUI;
    public void UpdateItemModifierUI(ItemModifierSO aModifier, PlayerInGameUI aPlayerInGameUI)
    {
        if (aModifier == null) return;
        ItemNameText.text = aModifier.modifierName;
        if(aModifier is IItemModifier modifierInterface)
        {
            modifierInterface.GetDescription();
        } 
        ItemDescriptionText.text = aModifier.modifierDescription;
        ItemLevelText.text = aModifier.modifierLevel.ToString();
        ItemImage.sprite = aModifier.modifierSprite;
        modifierSO = aModifier;
        playerInGameUI = aPlayerInGameUI;

    }
    public void OnCickApplyModifier( )
    {
        if ( modifierSO == null  || playerInGameUI == null) return;
       // playerInGameUI.ApplyNewWeaponModifier(modifierSO);
    }

}