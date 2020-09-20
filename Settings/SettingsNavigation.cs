using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsNavigation : MonoBehaviour
{
    public Image image;
    public Sprite otherSprite;
    public Sprite _otherSprite;

    public List<GameObject> SetOne;
    public List<GameObject> SetTwo;


    public void NextPage()
    {
        image.sprite = _otherSprite;
        SetInactive(true);
    }

    public void BackPage()
    {
        image.sprite = otherSprite;
        SetInactive(false);
    }

    public void SetInactive(bool page)
    {
        if(page) //1
        {
            foreach(GameObject gameObject in SetOne)
            {
                gameObject.SetActive(false);
            }
            foreach(GameObject gameObject in SetTwo)
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject gameObject in SetOne)
            {
                gameObject.SetActive(true);
            }
            foreach (GameObject gameObject in SetTwo)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
