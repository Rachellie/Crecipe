using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTab : MonoBehaviour
{
    public Button button;
	public int tabNum;
    public Text textLabel;
	
	private SliderMenuAnim sliderMenu;
	
	void Start()
    {
        button.onClick.AddListener(HandleClick);
    }
	
	public void Setup(SliderMenuAnim slider, int num)
	{
		tabNum = num;
		sliderMenu = slider;
		textLabel.text = num.ToString();
	}
	
	public void HandleClick()
	{
		Debug.Log("button " + textLabel.text + " clicked");
		sliderMenu.ClickTabButton(button, tabNum);
	}
}
