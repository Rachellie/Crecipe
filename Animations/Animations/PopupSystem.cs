using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSystem : MonoBehaviour
{
    public GameObject popupBox;
    public bool block;
    public Animator animator;
    public TMP_Text popupText;
	public GameObject canvasBlocker;
	public GameObject objectBlocker;

    public void popup(string text)
    {
        popupText.text = text;
    }

    public void ShowHidePopup(bool block)
    {
        if (popupBox != null)
        {
            Animator animator = popupBox.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                Debug.Log(isOpen);
                animator.SetBool("show", !isOpen);

                if(block)
                {
                    if (isOpen)
                    {
                        canvasBlocker.gameObject.SetActive(true);
                        objectBlocker.gameObject.SetActive(true);
                    }
                    else
                    {
                        canvasBlocker.gameObject.SetActive(false);
                        objectBlocker.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void CloseIfOpen()
    {
        if(popupBox != null)
        {
            Animator animator = popupBox.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                Debug.Log(isOpen);

                if(!isOpen)
                {
                    animator.SetBool("show", true);
                }
            }
        }
    }
}
