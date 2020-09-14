using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    public void Save()
	{
		PlayerData.player.Save();
	}
	
    public void Load()
	{
		PlayerData.player.Load();
	}
}
