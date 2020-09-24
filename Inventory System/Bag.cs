using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bag : MonoBehaviour
{
	public GameObject foodItemPrefab;
	public Canvas canvas;
	public Transform Model;
    public int numSlots;
    public GameObject itemSlot;
    public Transform parent;
	public bool inventory;
    public TMP_Text text;

    // index 15 is ItemSlotSelected
    private List<Transform> slotsList = new List<Transform>();
	
    void Start()
    {
		createSlots();
		
		foreach(Transform child in gameObject.transform)
		{
			slotsList.Add(child);
		}
		
		if(!inventory && slotsList.Count == 16)
		{
			slotsList.Add(slotsList[0]);
			slotsList.RemoveAt(0);
		}

		Refresh();
    }
	
	public void Refresh()
	{
		for(int i = 0; i < slotsList.Count; ++i)
		{
			for(int j = slotsList[i].childCount - 1; j >= 0; --j)
			{
				Destroy(slotsList[i].GetChild(j).gameObject);
			}
		}
		
		List<FoodObject> bag;
		if(!inventory)
		{
			bag = PlayerData.player.GetBag();
		}
		else
		{
			bag = PlayerData.player.GetFridge();
		}
		
		for(int i = 0; i < bag.Count; ++i)
		{
			if(bag.Count == 16 && slotsList.Count == 15 && i == 15)
			{
				break;
			}
			
			if(bag[i] != null)
			{
				GameObject food = Instantiate(foodItemPrefab, slotsList[i]);
				
				DragDrop dragDrop = food.GetComponent<DragDrop>();
				dragDrop.Setup(bag[i], canvas, this, i);
				dragDrop.tag = inventory;
			}
		}
		
		// create model
		if(Model != null)
		{
			FoodObject currentFood = PlayerData.player.GetCurrentFood();
			
			for (int i = Model.childCount - 1; i >= 0; --i)
			{
				Destroy(Model.GetChild(i).gameObject);
			}
			
			if(currentFood != null)
			{
				Instantiate(Resources.Load(currentFood.getModel()), Model);//
			}
		}
        //put Food Name
        if(PlayerData.player.GetCurrentFood() != null && text != null)
        {
            text.text = PlayerData.player.GetCurrentFood().getName();
        }
        else if(text != null)
        {
            text.text = "";
        }
	}

    private void createSlots()
    {
        for(int i = 0; i < numSlots; i++)
        {
            GameObject newItemSlot = Instantiate(itemSlot, parent);
			
			ItemSlot slot = newItemSlot.GetComponent<ItemSlot>();
			slot.slotNum = i;
			slot.tag = inventory;
			slot.SetBagScript(this);
        }
    }
}
