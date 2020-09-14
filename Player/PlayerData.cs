using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerInfo
{
	public string name = "Player";
	public List<FoodObject> fridge = new List<FoodObject>();
	public List<FoodObject> spices = new List<FoodObject>();
	public List<FoodObject> bag = new List<FoodObject>();
	public float money = 30f;
	public int level = 10;
	public float xp = 0.1f;
	public float happiness = 0.5f;
	public FoodObject currentFood = null;
}

public class PlayerData : MonoBehaviour
{
	public static PlayerData player;
	private PlayerInfo playerInfo = new PlayerInfo();
	private Vector3 position = new Vector3(0f, 6.29f, -1f);
	private Vector3 rotation = new Vector3(0f, 0f, 0f);
	private HashTable shopTable = new HashTable();
	private HashTable recipeTable = new HashTable();
	
	void Awake()
	{
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		if(player == null)
		{
			DontDestroyOnLoad(gameObject);
			player = this;
			shopTable.BuildDatabase();
			recipeTable.BuildRecipeDatabase();
			
			for(int i = 0; i < 16; ++i)
			{
				playerInfo.bag.Add(null);
			}
			for(int i = 0; i < 56; ++i)
			{
				playerInfo.fridge.Add(null);
				playerInfo.spices.Add(null);
			}
		}
		else if(player != this)
		{
			Destroy(gameObject);
		}

        //TEMPLATE FOR HOW TO USE AUDIO MANAGER
        //FindObjectOfType<AudioManager>().Play("theme");
	}
	
	public List<List<FoodObject>> GetTable()
	{
		return shopTable.GetTable();
	}
	
	public List<List<FoodObject>> GetRecipeTable()
	{
		return recipeTable.GetTable();
	}
	
	public string GetName()
	{
		return playerInfo.name;
	}
	public void SetName(string newName)
	{
		playerInfo.name = newName;
	}
	
	public List<FoodObject> GetFridge()
	{
		return playerInfo.fridge;
	}
	
	public void AddFoodItem(FoodObject food)
	{
		List<FoodObject> location = playerInfo.bag;

		// find if foodItem exists
		for (int i = 0; i < location.Count; ++i)
		{
			if (location[i] != null && food.getName() == location[i].getName())
			{
				location[i].setQuantity(location[i].getQuantity() + food.getQuantity());
				return;
			}
		}
		// if foodItem does not exist create new copy of foodItem and add to first null slot
		for(int i = 0; i < location.Count; ++i)
		{
			if(location[i] == null)
			{
				location[i] = new FoodObject(food);
				return;
			}
		}
	}
	
	public void RemoveFoodItem(FoodObject food)
	{	
		for (int i = 0; i < playerInfo.fridge.Count; ++i)
		{
			if (food.getName() == playerInfo.fridge[i].getName())
			{
                // subtract 1 from quantity
                playerInfo.fridge[i].setQuantity(playerInfo.fridge[i].getQuantity() - 1);
				if(playerInfo.fridge[i].getQuantity() <= 0)
				{
                    playerInfo.fridge.RemoveAt(i);
				}
				return;
			}
		}
	}
	
	public List<FoodObject> GetBag()
	{
		return playerInfo.bag;
	}
	
	public void AddToBag(FoodObject food, int index = 0)
	{
		playerInfo.bag.Insert(index, food);
	}
	
	public void RemoveFromBag(int index = 0)
	{
		playerInfo.bag.RemoveAt(index);
	}
	
	public void SwapBagItems(int indexFrom, int indexTo)
	{
		if(indexFrom == indexTo)
		{
			return;
		}
		
		if(playerInfo.bag[indexFrom] != null && playerInfo.bag[indexTo] != null)
		{
			if(playerInfo.bag[indexFrom].getName() == playerInfo.bag[indexTo].getName())
			{
				playerInfo.bag[indexTo].setQuantity(playerInfo.bag[indexTo].getQuantity() + playerInfo.bag[indexFrom].getQuantity());
				playerInfo.bag[indexFrom] = null;
				return;
			}
		}
		FoodObject temp = playerInfo.bag[indexFrom];
		playerInfo.bag[indexFrom] = playerInfo.bag[indexTo];
		playerInfo.bag[indexTo] = temp;
	}
	
	public void SwapInventoryItems(int indexFrom, int indexTo)
	{
		if(indexFrom == indexTo)
		{
			return;
		}
		
		if(playerInfo.fridge[indexFrom] != null && playerInfo.fridge[indexTo] != null)
		{
			if(playerInfo.fridge[indexFrom].getName() == playerInfo.fridge[indexTo].getName())
			{
                playerInfo.fridge[indexTo].setQuantity(playerInfo.fridge[indexTo].getQuantity() + playerInfo.fridge[indexFrom].getQuantity());
                playerInfo.fridge[indexFrom] = null;
				return;
			}
		}
		
		FoodObject temp = playerInfo.fridge[indexFrom];
        playerInfo.fridge[indexFrom] = playerInfo.fridge[indexTo];
        playerInfo.fridge[indexTo] = temp;
	}

	// from bag to inventory
	public void BagInventorySwap(int indexFrom, int indexTo)
	{		
		if(playerInfo.bag[indexFrom] != null && playerInfo.fridge[indexTo] != null)
		{
			if(playerInfo.bag[indexFrom].getName() == playerInfo.fridge[indexTo].getName())
			{
                playerInfo.fridge[indexTo].setQuantity(playerInfo.fridge[indexTo].getQuantity() + playerInfo.bag[indexFrom].getQuantity());
				playerInfo.bag[indexFrom] = null;
				return;
			}
		}
		
		FoodObject temp = playerInfo.bag[indexFrom];
		playerInfo.bag[indexFrom] = playerInfo.fridge[indexTo];
        playerInfo.fridge[indexTo] = temp;
	}
	
	// from inventory to bag
	public void InventoryBagSwap(int indexFrom, int indexTo)
	{
		if(playerInfo.fridge[indexFrom] != null && playerInfo.bag[indexTo] != null)
		{
			if(playerInfo.fridge[indexFrom].getName() == playerInfo.bag[indexTo].getName())
			{
				playerInfo.bag[indexTo].setQuantity(playerInfo.bag[indexTo].getQuantity() + playerInfo.fridge[indexFrom].getQuantity());
                playerInfo.fridge[indexFrom] = null;
				return;
			}
		}
		
		FoodObject temp = playerInfo.fridge[indexFrom];
        playerInfo.fridge[indexFrom] = playerInfo.bag[indexTo];
		playerInfo.bag[indexTo] = temp;
	}
	
	public float GetMoney()
	{
		return playerInfo.money;
	}
	public void SetMoney(float amount)
	{
		playerInfo.money = amount;
	}
	
	public int GetLevel()
	{
		return playerInfo.level;
	}
	public void SetLevel(int lvl)
	{
		playerInfo.level = lvl;
	}
	
	public float GetXP()
	{
		return playerInfo.xp;
	}
	public void SetXP(float amount)
	{
		playerInfo.xp = amount;
	}
	
	public float GetHappiness()
	{
		return playerInfo.happiness;
	}
	public void SetHappiness(float amount)
	{
		playerInfo.happiness = amount;
	}
	
	public Vector3 GetPosition()
	{
		return position;
	}
	public void SetPosition(Vector3 newPos)
	{
		position = newPos;
	}
	
	public Vector3 GetRotation()
	{
		return rotation;
	}
	public void SetRotation(Vector3 newRot)
	{
		rotation = newRot;
	}
	
	public FoodObject GetCurrentFood()
	{
		return playerInfo.currentFood;
	}
	public void SetCurrentFood(FoodObject food)
	{
		playerInfo.bag[15] = food;
		
		GameObject player = GameObject.Find("Player");
		
		if(food == null)
		{
			playerInfo.currentFood = null;
			
			if(player != null)
			{
				for(int i = player.transform.childCount - 1; i >= 2; --i)
				{
					Destroy(player.transform.GetChild(i).gameObject); // third child is hat
				}
			}
			Debug.Log("you currently have NOTHING");
			return;
		}
		
		playerInfo.currentFood = new FoodObject(food);
		
		if(player != null)
		{
			for(int i = player.transform.childCount - 1; i >= 2; --i)
			{
				Destroy(player.transform.GetChild(i).gameObject); // third child is hat
			}
			
			Instantiate(Resources.Load(playerInfo.currentFood.getModel()), player.transform);
			if(playerInfo.currentFood.getPlate())
			{
				Instantiate(Resources.Load("Models/plate"), player.transform);
				player.transform.GetChild(2).localPosition += new Vector3(0f, 0.03f, 0f);
			}
		}
		Debug.Log("you currently have " + playerInfo.currentFood.getName());
	}
	
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerData.food");

		bf.Serialize(file, playerInfo);
		file.Close();
	}
	
	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerData.food"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerData.food", FileMode.Open);
			playerInfo = (PlayerInfo)bf.Deserialize(file);
			file.Close();
		}
	}
}
