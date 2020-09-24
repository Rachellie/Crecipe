using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plate : MonoBehaviour, IPointerDownHandler
{
	private FoodObject plate;
	public Transform player;
	public float range = 2f;
	
    public void OnPointerDown(PointerEventData data)
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range)
		{
			plate = PlayerData.player.GetDB()[0]["Plate"];
			
			if(PlayerData.player.GetCurrentFood() == null) // make sure there is nothing on head
			{
				PlayerData.player.SetCurrentFood(plate);
			}
			else if(PlayerData.player.GetCurrentFood().getName() == "Plate")
			{
				PlayerData.player.SetCurrentFood(null);
			}
		}
	}
}
