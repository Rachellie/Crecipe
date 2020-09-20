using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    public Button toggleButton;

    private int day; //0 = day, 1 = night

    private void Start()
    {
        day = PlayerPrefs.GetInt("day", 0);
        SetTogglePos();
    }

    public void SetTogglePos()
    {
        if (day == 0)
        {
            toggleButton.transform.localPosition = new Vector3(51, -200, 0);
        }
        else
        {
            toggleButton.transform.localPosition = new Vector3(-51, -200, 0);
        }
    }

    public void SwitchDayNight()
    {
        if(day == 0)
        {
            Debug.Log("Switched to night");
            PlayerPrefs.SetInt("day", 1);
            day = 1;
        }
        else
        {
            Debug.Log("Switched to day");
            PlayerPrefs.SetInt("day", 0);
            day = 0;
        }
    }
}
