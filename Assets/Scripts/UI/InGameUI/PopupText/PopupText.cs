using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField]TextMeshPro mText;
    [SerializeField] Color mOriginalColor;
    private float duration = 2f;
	private float fadeDuration = 2f;
	private Vector3 moveDirection = new Vector3(0, 1, 0);
	private float moveSpeed = 2f;
	private float currentSpeed;
	private float timer;

    private void FixedUpdate()
	{
		transform.position += moveDirection * currentSpeed * Time.fixedDeltaTime;
		timer -= Time.fixedDeltaTime; 
		if (timer <= fadeDuration)
		{
			float alpha = timer / fadeDuration;
			Color newColor = mOriginalColor;
			newColor.a = alpha;
			mText.color = newColor;
			transform.localScale = Vector3.one * alpha;
		} 
		if (timer <= 0)
		{
			gameObject.SetActive(false);
		}
	}

	public void Setup(string text, Color color, int fontSize, Vector3 position, Quaternion rotation)
	{
		transform.localScale = Vector3.one;
		moveDirection = new Vector3(Random.Range(-0.2f,0.2f), 1, 0);
        currentSpeed = Random.Range(1, moveSpeed);
        mText.text = text;
        mOriginalColor = color;
		mText.fontSize = fontSize;
        mText.transform.position = position;
		mText.transform.rotation = rotation; 
        gameObject.SetActive(true);
        timer = duration;
    }

}
