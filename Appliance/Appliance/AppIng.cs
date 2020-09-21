using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppIng : MonoBehaviour
{
	public Transform contentPanel;
	public GameObject foodItemPrefab;
	public GameObject itemSlotPrefab;
	public GameObject canvasBlocker;
	
    public void SetContents(HashSet<FoodObject> foodSet)
	{
		bool isActive = gameObject.activeInHierarchy;
		
		foreach(Transform child in contentPanel)
		{
			Destroy(child.gameObject);
		}
		
		if(isActive)
		{
			foreach(FoodObject currentFood in foodSet)
			{
				GameObject slot = Instantiate(itemSlotPrefab, contentPanel);
				
				GameObject newFood = Instantiate(foodItemPrefab, slot.transform);
				
				DragDrop dragDrop = newFood.GetComponent<DragDrop>();
				dragDrop.Setup(currentFood, null, null, -1);
				
				CanvasGroup canvasGroup = newFood.GetComponent<CanvasGroup>();
				canvasGroup.blocksRaycasts = false;
			}
		}
		
		canvasBlocker.SetActive(isActive);
	}
	
	public void Close()
	{
		if(gameObject.activeInHierarchy)
		{
			canvasBlocker.SetActive(false);
			gameObject.SetActive(false);
		}
	}
}
