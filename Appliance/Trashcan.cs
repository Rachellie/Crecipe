using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
	public PopupSystem popup;
	public Transform player;
	public float range = 2f;
	
    void OnMouseDown()
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range && PlayerData.player.GetCurrentFood() != null)
		{
			popup.popup("Are you sure you want to delete " + PlayerData.player.GetCurrentFood().getName() + "?");
			popup.ShowHidePopup();
		}
	}
	
	public void DeleteItem()
	{
		PlayerData.player.SetCurrentFood(null);
	}
}
