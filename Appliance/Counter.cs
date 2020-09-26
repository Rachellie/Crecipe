using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Counter : MonoBehaviour, IPointerDownHandler
{
	public int counterNum;
	
	public Transform player;
	public float range = 2f;
	
	void Start()
    {
        Refresh();
    }
	
	public void Refresh()
	{
		FoodObject food = PlayerData.player.GetCounterFood(counterNum);
		
		for (int i = gameObject.transform.childCount - 1; i >= 0; --i)
		{
			Destroy(gameObject.transform.GetChild(i).gameObject);
		}
		
		if(food != null)
		{
			Instantiate(Resources.Load(food.getModel()), gameObject.transform);
			gameObject.transform.GetChild(0).localPosition += new Vector3(0f, -0.21f, 0f);
			// gameObject.transform.GetChild(0).rotation = Quaternion.identity;
		}
	}
	
	public void OnPointerDown(PointerEventData data)
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range)
		{
			if(PlayerData.player.GetCurrentFood() != null && PlayerData.player.GetCounterFood(counterNum) == null)
			{
				// place ONE food on counter
				FoodObject food = PlayerData.player.GetCurrentFood();
				food.subOneQ();
				PlayerData.player.SetCurrentFood(food);
				
				FoodObject counter = new FoodObject(food);
				counter.setQuantity(1);
				PlayerData.player.SetCounterFood(counterNum, counter);
				Refresh();
			}
			else if(PlayerData.player.GetCurrentFood() != null && PlayerData.player.GetCounterFood(counterNum) != null)
			{
				// stack food back if same name
				FoodObject food = PlayerData.player.GetCurrentFood();
				if(PlayerData.player.GetCounterFood(counterNum).getName() == food.getName())
				{
					food.addOneQ();
					PlayerData.player.SetCurrentFood(food);
					PlayerData.player.SetCounterFood(counterNum, null);
					Refresh();
				}
			}
			else if(PlayerData.player.GetCurrentFood() == null && PlayerData.player.GetCounterFood(counterNum) != null)
			{
				// remove food from counter
				PlayerData.player.SetCurrentFood(PlayerData.player.GetCounterFood(counterNum));
				PlayerData.player.SetCounterFood(counterNum, null);
				Refresh();
			}
		}
	}
}
