using TMPro;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    TextMeshPro mText;
    Animator animator;
   // public static FloatingTextController Instance;

    internal void PopUp(int aDamageAmount)
    {
        if(animator == null || mText == null) { return; }
        transform.forward = Camera.main.transform.forward;
        mText.text = aDamageAmount.ToString();
        animator.SetTrigger("PopUp");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mText = GetComponent<TextMeshPro>();
        
    }
    public void EmptyText()
    {
        mText.text = "";
    }

}
