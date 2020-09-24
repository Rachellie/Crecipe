using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsNavigation : MonoBehaviour
{
    public Image image;
    public Sprite spriteOne;
    public Sprite spriteTwo;
    public Sprite spriteThree;

    public Image maxPlayer;
    public Sprite Two;
    public Sprite Three;

    public List<GameObject> SetOne;
    public List<GameObject> SetTwo;
    public List<GameObject> SetThree;

    private int page = 1;


    public void NextPage()
    {
        if (page == 1)
        {
            page += 1;
        }
        else if (page == 2)
        {
            page += 1;
        }
        SetPanel();
    }

    public void BackPage()
    {
        if (page == 2)
        {
            page -= 1;
        }
        else if (page == 3)
        {
            page -= 1;
        }
        SetPanel();
    }

    public void SetPanel()
    {
        if(page == 1) //1
        {
            image.sprite = spriteOne;
            foreach (GameObject _object in SetTwo)
            {
                _object.SetActive(false);
            }
            foreach (GameObject _object in SetThree)
            {
                _object.SetActive(false);
            }
            foreach (GameObject _object in SetOne)
            {
                _object.SetActive(true);
            }
        }
        else if(page == 2)
        {
            image.sprite = spriteTwo;
            foreach (GameObject _object in SetOne)
            {
                _object.SetActive(false);
            }
            foreach (GameObject _object in SetThree)
            {
                _object.SetActive(false);
            }
            foreach (GameObject _object in SetTwo)
            {
                _object.SetActive(true);
            }
        }
        else
        {
            image.sprite = spriteThree;
            if(HostAndJoin.maxPlayers == 2)
            {
                maxPlayer.sprite = Two;
            }
            else
            {
                maxPlayer.sprite = Three;
            }
            foreach (GameObject _object in SetOne)
            {
                _object.SetActive(false);
            }
            foreach (GameObject _object in SetTwo)
            {
                _object.SetActive(false);
            }
            foreach (GameObject _object in SetThree)
            {
                _object.SetActive(true);
            }
        }
    }
}
