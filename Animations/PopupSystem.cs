using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSystem : MonoBehaviour
{
    public GameObject popupBox;
    public Animator animator;
    public TMP_Text popupText;
    public Button check;
    public Button exit;
	public GameObject canvasBlocker;
	public GameObject objectBlocker;

    public void popup(string text)
    {
        popupBox.SetActive(true);
        popupText.text = text;
    }

    public void ShowHidePopup()
    {
        if (popupBox != null)
        {
            Animator animator = popupBox.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                Debug.Log(isOpen);
                animator.SetBool("show", !isOpen);
                
                if(isOpen)
                {
					canvasBlocker.SetActive(false);
					objectBlocker.SetActive(false);
                }
                else
                {
					canvasBlocker.SetActive(true);
					objectBlocker.SetActive(true);
                }
            }
        }
    }
}
