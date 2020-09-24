using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public GameObject directionalLight;
    public GameObject nightLight;
    public GameObject lampLight;
    public Material material;

    private int day; //0 = day, 1 = night

    private void Start()
    {
        day = PlayerPrefs.GetInt("day", 0);
        SetDayNight();
        SetEmission();
    }

    public void SetDayNight()
    {
        if(day == 0)
        {
            directionalLight.transform.rotation = Quaternion.Euler(50, -30, 0);
			directionalLight.GetComponent<Light>().intensity = 1f;
            nightLight.gameObject.SetActive(false);
            lampLight.gameObject.SetActive(false);
        }
        else
        {
            directionalLight.transform.rotation = Quaternion.Euler(-90, -30, 0);
			directionalLight.GetComponent<Light>().intensity = 0f;
            nightLight.gameObject.SetActive(true);
            lampLight.gameObject.SetActive(true);
        }
    }

    public void SwitchDayNight()
    {
        if(day == 0)
        {
            day = 1;
            SetDayNight();
            SetEmission();
        }
        else
        {
            day = 0;
            nightLight.gameObject.SetActive(true);
            SetDayNight();
            SetEmission();
        }
    }

    public void SetEmission()
    {
        if (day == 0)
        {
            material.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            material.SetColor("_EmissionColor", new Color(0.5f, 0.5f, 0.5f));
        }
    }
}
