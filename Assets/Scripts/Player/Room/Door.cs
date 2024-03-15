using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Close()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("Close");
    }
    public void Open()
    {
        if (animator == null)
        {
            return;
        }
        animator.SetTrigger("Open");
    }
}
