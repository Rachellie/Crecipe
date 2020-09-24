using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float GetBarValue()
    {
        return slider.value;
    }

    public void SetBarValue(int num)
    {
        slider.value = num;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        Debug.Log("Change fill");
    }

    public void SetMaxBarValue(int num)
    {
        slider.maxValue = num;
        //fill.color = gradient.Evaluate(1f);
    }
}
