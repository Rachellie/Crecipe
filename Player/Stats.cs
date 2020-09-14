using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
	public Image barHappiness;
	public Image barXP;
	public Text nameLabel;
	public Text levelLabel;
	
    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void Refresh()
	{
		barHappiness.fillAmount = PlayerData.player.GetHappiness();
		barXP.fillAmount = PlayerData.player.GetXP();
		nameLabel.text = PlayerData.player.GetName();
		levelLabel.text = "Lv. " + PlayerData.player.GetLevel().ToString();
	}
}
