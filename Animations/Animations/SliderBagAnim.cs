using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBagAnim : MonoBehaviour
{
    public GameObject PanelBag;
	public Transform FoodModel;
	private bool Rotating = false;
	public GameObject objectBlocker;

    public void ShowHideMenu()
    {
        if (PanelBag != null)
        {
            Animator animator = PanelBag.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                Debug.Log(isOpen);
                animator.SetBool("show", !isOpen);
				
				Rotating = isOpen;

                objectBlocker.gameObject.SetActive(isOpen);
            }
        }
    }
	
	void FixedUpdate()
	{
		if(Rotating)
		{
			FoodModel.Rotate(0, 1, 0);
		}
	}
}
