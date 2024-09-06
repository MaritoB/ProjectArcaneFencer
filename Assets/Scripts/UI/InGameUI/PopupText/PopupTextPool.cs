using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public struct DamageTypeColor
{
    public DamageType type;
    public Color color;
}
public class PopupTextPool : MonoBehaviour
{
    public DamageTypeColor[] typeColorArray; 
    System.Collections.Generic.Dictionary<DamageType, Color> ColorDictionary;
    public static PopupTextPool Instance;
    [SerializeField] int fontSize, criticalFontSize;

    Quaternion cameraRotation;
    public RectTransform popupTextPrefab;
    PopupText mPopupText;

    private List<PopupText> TextPool = new List<PopupText>();

    private void Start()
    {
        mPopupText = popupTextPrefab.GetComponent<PopupText>();
        ColorDictionary = new Dictionary<DamageType, Color>();
        foreach (DamageTypeColor typeColor in typeColorArray)
        {
            ColorDictionary.Add(typeColor.type, typeColor.color);
        }
        cameraRotation = Camera.main.transform.rotation;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if(mPopupText == null)
            {
                return;
            }
            PreInstantiate(TextPool, mPopupText, 10); 

 

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private PopupText GetFromPool(List<PopupText> pool, PopupText prefab)
    {
        foreach (var item in pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }

        var newInstance = Instantiate(prefab);
        pool.Add(newInstance);
        newInstance.transform.SetParent(transform);
        
        return newInstance;
    }
    private void PreInstantiate(List<PopupText> pool, PopupText prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var instance = Instantiate(prefab);
            instance.transform.rotation = cameraRotation;
            instance.gameObject.SetActive(false);
            pool.Add(instance);
            instance.transform.SetParent(transform);
        }
    }
    public PopupText GetFloatingText()
    {
        return GetFromPool(TextPool, mPopupText);
    }
    public void ShowPopup(Vector3 position, string message, bool isCritical, bool isMagic)
    {
        PopupText newPopup = GetFloatingText();
        newPopup.Setup(message, Color.white, fontSize,position, cameraRotation);

    }
    public void ShowPopUpGoldPickUp(Vector3 position, string message )
    {
        PopupText newPopup = GetFloatingText();
        newPopup.Setup("Gold +"+message, Color.yellow, fontSize/2, position, cameraRotation);
    }

    internal void ShowPopup(AttackInfo aAttackInfo, Vector3 aPosition)
    {
        if (aAttackInfo == null) return;
        PopupText newPopup = GetFloatingText();
        Color color = Color.white ;
        ColorDictionary.TryGetValue(aAttackInfo.damageType, out color);
        newPopup.Setup(aAttackInfo.damage.ToString(),
            color,
            aAttackInfo.isCritical ? criticalFontSize : fontSize,
            aPosition, cameraRotation) ;
    }
}
