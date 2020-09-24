using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEat : MonoBehaviour
{


    public void EatFood()
	{
		Transform onHead = GameObject.FindWithTag("Player").transform.GetChild(2);
		onHead.SetParent(null, true);
		StartCoroutine(Slerp(onHead));
		
		FoodObject toEat = PlayerData.player.GetCurrentFood();
		
		PlayerData.player.SetHappiness(PlayerData.player.GetHappiness() + toEat.getHappiness());
		if(toEat.getHappiness() > 0)
		{
			PlayerData.player.SetXP(PlayerData.player.GetXP() + toEat.getHappiness());
		}
		
		toEat.subOneQ();
		
		if(toEat.getQuantity() <= 0)
		{
			toEat = null;
		}
        
        PlayerData.player.SetCurrentFood(toEat);
	}

	IEnumerator Slerp(Transform food)
	{
		float timeElapsed = 0f;
		
		Transform player = GameObject.FindWithTag("Player").transform;
		
		// Vector3 start = food.position;
		
		// Vector3 mid = player.position;
		// mid += player.forward * 0.5f;
		// mid += player.up * 0.5f;
		
		// Vector3 end = player.position;
		// end += player.forward * 0.5f;
		
		while(timeElapsed < 0.5f)
		{
			food.RotateAround(player.transform.position, player.transform.right, 2.5f);
			food.rotation = player.transform.rotation;
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		
		Destroy(food.gameObject);
		FindObjectOfType<AudioManager>().Play("eat");
	}
}
