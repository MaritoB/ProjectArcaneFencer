using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private Color originalColor = Color.white;
    [SerializeField] private float duration = 0.5f;

    private bool isChangingColor = false;

    public void ShowDamageIndicator()
    {
        if (!isChangingColor)
        {
            isChangingColor = true;
            StartCoroutine(ChangeColor());
        }
    }

    private System.Collections.IEnumerator ChangeColor()
    {
        float elapsedTime = 0f;
        Material material = skinnedMeshRenderer.material;

        while (elapsedTime < duration)
        {
            float lerpFactor = elapsedTime / duration;
            material.color = Color.Lerp(damageColor, originalColor, lerpFactor);
            elapsedTime += Time.deltaTime;
            Debug.Log("ShowDamageIndicator");
            yield return null;
        }

        material.color = originalColor;
        isChangingColor = false;
    }
}
