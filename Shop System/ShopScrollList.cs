using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScrollList : MonoBehaviour
{
	public bool isPlayer = false;
	public List<FoodObject> foodList;
	public Transform contentPanel;
	public ShopScrollList otherShop;
	public Text myGoldDisplay;
	public SimpleObjectPool buttonObjectPool;
	public float gold = 1 / 0f; // positive infinity
	public Text myCostDisplay;
	public static float cost = 0f;


	// Start is called before the first frame update
	void Start()
	{
		cost = 0f;
		
		if (isPlayer)
		{
			gold = PlayerData.player.GetMoney();
		}
		else
		{
			foodList = new List<FoodObject>();
			for (int i = 0; i <= PlayerData.player.GetLevel() && i < PlayerData.player.GetTable().Count; ++i)
			{
				foodList.AddRange(PlayerData.player.GetTable()[i]);
			}
			
		}
		RefreshDisplay();
	}

	public void RefreshDisplay()
	{
		if (isPlayer)
		{
			myGoldDisplay.text = "Gold: " + gold.ToString();
			myCostDisplay.text = cost.ToString();
		}
		else
		{
			myGoldDisplay.text = "Ingredients";
		}
		RemoveButtons();
		AddButtons();
	}

	private void AddButtons()
	{
		for (int i = 0; i < foodList.Count; ++i)
		{
			FoodObject food = foodList[i];
			GameObject newButton = buttonObjectPool.GetObject();
			newButton.transform.SetParent(contentPanel, false); // add false

			ShopButton shopButton = newButton.GetComponent<ShopButton>();
			shopButton.Setup(food, this);
		}
	}

	public void BuyFoodItems()
	{
		RemoveButtons();
		AddToBag();
		PlayerData.player.SetMoney(gold);
		ClearFoodItemList();
		cost = 0f;
		RefreshDisplay();
	}

	private void RemoveButtons()
	{
		while (contentPanel.childCount > 0)
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
			buttonObjectPool.ReturnObject(toRemove);
		}
	}

	private void AddToBag()
	{
		for (int i = 0; i < foodList.Count; ++i)
		{
			PlayerData.player.AddFoodItem(foodList[i]);
		}
	}

	private void ClearFoodItemList()
	{
		foodList.Clear();
	}

	public void TryTransferFoodItemToOtherShop(FoodObject food)
	{
		if (otherShop.gold >= food.getPrice())
		{
			gold += food.getPrice();
			otherShop.gold -= food.getPrice();

			AddFoodItem(food, otherShop);
			RemoveFoodItem(food, this);

			RefreshDisplay();
			otherShop.RefreshDisplay();
		}
	}

	private void AddFoodItem(FoodObject foodToAdd, ShopScrollList shopList)
	{
		if (shopList.isPlayer) // if shop that we add to is the player's:
		{
			cost += foodToAdd.getPrice();
			// find if foodItem exists, then increment quantity
			for (int i = 0; i < shopList.foodList.Count; ++i)
			{
				if (shopList.foodList[i].getName() == foodToAdd.getName())
				{
					shopList.foodList[i].addOneQ();
					return;
				}
			}
			// if foodItem not found in list then add it to the list with quantity 1
			foodToAdd.setQuantity(1);
			shopList.foodList.Add(foodToAdd);
		}
	}

	private void RemoveFoodItem(FoodObject foodToRemove, ShopScrollList shopList)
	{
		if (shopList.isPlayer)
		{
			cost -= foodToRemove.getPrice();
			// find the foodItem in the list
			for (int i = shopList.foodList.Count - 1; i >= 0; --i)
			{
				if (shopList.foodList[i].getName() == foodToRemove.getName())
				{
					// decrease quantity
					shopList.foodList[i].subOneQ();
					if (shopList.foodList[i].getQuantity() <= 0)
					{
						// remove if quantity is 0
						shopList.foodList.RemoveAt(i);
					}
				}
			}
		}
	}
}