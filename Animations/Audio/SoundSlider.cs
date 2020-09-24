using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
	public Slider slider;
	public bool music = true;
	
	void Start()
	{
		if(music)
		{
			slider.value = PlayerPrefs.GetFloat("music", 0.5f);
		}
		else
		{
			slider.value = PlayerPrefs.GetFloat("sound", 0.5f);
		}
	}
	
	public void SetVolume()
	{
		if(music)
		{
			PlayerPrefs.SetFloat("music", slider.value);
		}
		else
		{
			PlayerPrefs.SetFloat("sound", slider.value);
		}
		
		AudioManager.instance.SetVolume();
	}
}
