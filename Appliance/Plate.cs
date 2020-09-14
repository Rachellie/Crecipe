﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
	private FoodObject plate = new FoodObject("Plate", "Sprites/plate", "Models/plate");
	public Transform player;
	public float range = 2f;
	
    void OnMouseDown()
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range)
		{
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
