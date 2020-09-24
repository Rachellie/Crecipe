using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
	public Text nameLabel;
	public Text levelLabel;
	
	public Bar xp;
	public Bar happiness;
	
    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void Refresh()
	{
		xp.SetMaxBarValue(PlayerData.player.GetMaxXP());
		xp.SetBarValue(PlayerData.player.GetXP());
		
		happiness.SetMaxBarValue(PlayerData.player.GetMaxHappiness());
		happiness.SetBarValue(PlayerData.player.GetHappiness());
		
		nameLabel.text = PlayerData.player.GetName();
		levelLabel.text = "Lv. " + PlayerData.player.GetLevel().ToString();
	}
}
