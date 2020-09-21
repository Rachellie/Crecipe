using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trashcan : MonoBehaviour, IPointerDownHandler
{
	public PopupSystem popup;
	public Transform player;
	public float range = 2f;
	
    public void OnPointerDown(PointerEventData data)
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range && PlayerData.player.GetCurrentFood() != null)
		{
			popup.popup("Are you sure you want to delete " + PlayerData.player.GetCurrentFood().getName() + "?");
			popup.ShowHidePopup(true);
		}
	}
	
	public void DeleteItem()
	{
		Transform onHead = player.GetChild(2);
		onHead.SetParent(null, true);
		StartCoroutine(Slerp(onHead));
		
		PlayerData.player.SetCurrentFood(null);
	}
	
	IEnumerator Slerp(Transform food)
	{
		float timeElapsed = 0f;
		
		Vector3 start = food.position;
		Vector3 end = transform.position; // self
		
		while(timeElapsed < 0.25f)
		{
			food.position = Vector3.Slerp(start, end, timeElapsed/0.25f);
			food.Rotate(0, 2, 0);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		
		Destroy(food.gameObject);
	}
}
