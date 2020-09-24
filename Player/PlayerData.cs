using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class PlayerInfo
{
	public string name = "Player";
    public string playerMaterial = "PlayerPurple";
	
	public List<FoodObject> fridge = new List<FoodObject>();
	public List<FoodObject> bag = new List<FoodObject>();
	public List<FoodObject> counters = new List<FoodObject>();
	
	public float money = 30f;
	
	public int level = 10;
	public int xp = 5;
	//public int maxXP = 100;
	
	public int happiness = 70;
	//public int maxHappiness = 100;
	
	public FoodObject currentFood = null;
}

public class PlayerData : MonoBehaviour
{
	public static PlayerData player;
	private PlayerInfo playerInfo = new PlayerInfo();
	private Vector3 position = new Vector3(2f, 6.26f, -33f);
	private Vector3 rotation = new Vector3(0f, 0f, 0f);
	private Database database = new Database();
	
	void Awake()
	{
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		if(player == null)
		{
			DontDestroyOnLoad(gameObject);
			player = this;
			database.BuildDatabase();
			database.BuildShopDatabase();
			database.BuildRecipeDatabase();
			
			for(int i = 0; i < 56; ++i)
			{
				playerInfo.fridge.Add(null);
			}
			for(int i = 0; i < 16; ++i)
			{
				playerInfo.bag.Add(null);
			}
			for(int i = 0; i < 36; ++i)
			{
				playerInfo.counters.Add(null);
			}
		}
		else if(player != this)
		{
			Destroy(gameObject);
		}

        //TEMPLATE FOR HOW TO USE AUDIO MANAGER
        //FindObjectOfType<AudioManager>().Play("theme");
	}
	
	public List<Dictionary<string, FoodObject>> GetDB()
	{
		return database.database;
	}
	
	public List<Dictionary<string, FoodObject>> GetShopDB()
	{
		return database.shopDatabase;
	}
	
	public List<Dictionary<string, FoodObject>> GetRecipeDB()
	{
		return database.recipeDatabase;
	}
	
	public string GetName()
	{
		return playerInfo.name;
	}

	public void SetName(string newName)
	{
		playerInfo.name = newName;
        RefreshStats();
	}

    public string GetPlayerMaterial()
    {
        return playerInfo.playerMaterial;
    }

    public void SetPlayerMaterial(string newMaterial)
    {
        playerInfo.playerMaterial = newMaterial;
        RefreshStats();
    }
	
	public List<FoodObject> GetFridge()
	{
		return playerInfo.fridge;
	}
	public void SetFridge(List<FoodObject> newFridge)
	{
		playerInfo.fridge = newFridge;
	}
	
	public bool AddFoodItem(FoodObject food)
	{
		List<FoodObject> location = playerInfo.bag;

		// find if foodItem exists
		for (int i = 0; i < 15; ++i)
		{
			if (location[i] != null && food.getName() == location[i].getName())
			{
				location[i].setQuantity(location[i].getQuantity() + food.getQuantity());
				return true;
			}
		}
		// if foodItem does not exist create new copy of foodItem and add to first null slot
		for(int i = 0; i < 15; ++i)
		{
			if(location[i] == null)
			{
				location[i] = new FoodObject(food);
				return true;
			}
		}
		
		return false;
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
		
		ClientSend.UpdateFridge(indexFrom);
		ClientSend.UpdateFridge(indexTo);
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
		
		ClientSend.UpdateFridge(indexTo);
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
		
		ClientSend.UpdateFridge(indexFrom);
	}
	
	public FoodObject GetCounterFood(int index)
	{
		return playerInfo.counters[index];
	}
	public void SetCounterFood(int index, FoodObject food)
	{
		playerInfo.counters[index] = food;
		
		ClientSend.UpdateCounter(index);
	}
	
	public List<FoodObject> GetCounters()
	{
		return playerInfo.counters;
	}
	public void SetCounters(List<FoodObject> newCounters)
	{
		playerInfo.counters = newCounters;
	}
	
	public float GetMoney()
	{
		return playerInfo.money;
	}
	public void SetMoney(float amount)
	{
		playerInfo.money = amount;
		RefreshStats();
	}
	public void AddMoney(float amount)
	{
		playerInfo.money += amount;
		RefreshStats();
	}
	
	public int GetLevel()
	{
		return playerInfo.level;
	}
	public void SetLevel(int lvl)
	{
		playerInfo.level = lvl;
		RefreshStats();
	}
	
	public int GetXP()
	{
		return playerInfo.xp;
	}
	public void SetXP(int amount)
	{
		if(amount > GetMaxXP())
		{
			playerInfo.xp = amount - GetMaxXP();
			++playerInfo.level;
		}
		else
		{
			playerInfo.xp = amount;
		}
		
		RefreshStats();
	}
	
	public int GetMaxXP()
	{
		return playerInfo.level * 10;
	}
	// public void SetMaxXP(int max)
	// {
		// playerInfo.maxXP = max;
		// RefreshStats();
	// }
	
	public int GetHappiness()
	{
		return playerInfo.happiness;
	}
	public void SetHappiness(int amount)
	{
		if(amount < 0)
		{
			playerInfo.happiness = 0;
		}
		else if(amount > GetMaxHappiness())
		{
			playerInfo.happiness = GetMaxHappiness();
		}
		else
		{
			playerInfo.happiness = amount;
		}
		
		RefreshStats();
	}
	public void AddHappiness(float amount)
	{
		playerInfo.happiness += Convert.ToInt32(amount);
		RefreshStats();
	}
	
	public int GetMaxHappiness()
	{
		return (playerInfo.level*10) + 20;
	}
	// public void SetMaxHappiness(int max)
	// {
		// playerInfo.maxHappiness = max;
		// RefreshStats();
	// }
	
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
		GameObject player = GameObject.FindWithTag("Player");
        GameObject buttonEat = GameObject.Find("/Canvas/ButtonEat");
		
		if(food == null || food.getQuantity() <= 0)
		{
			playerInfo.currentFood = null;
			playerInfo.bag[15] = null;
			
			if(player != null)
			{
				for(int i = player.transform.childCount - 1; i >= 2; --i)
				{
					Destroy(player.transform.GetChild(i).gameObject); // third child is hat
				}
			}
			Debug.Log("you currently have NOTHING");
            buttonEat.GetComponent<Image>().enabled = false;
			ClientSend.PlayerFood("null");
			return;
		}
		
		playerInfo.currentFood = new FoodObject(food);
		playerInfo.bag[15] = food;
		
		if(player != null)
		{
			for(int i = player.transform.childCount - 1; i >= 2; --i)
			{
				Destroy(player.transform.GetChild(i).gameObject); // third child is hat
			}
			
			Instantiate(Resources.Load(playerInfo.currentFood.getModel()), player.transform);
		}
		Debug.Log("you currently have " + playerInfo.currentFood.getName());
		ClientSend.PlayerFood(food.getName());
		
		if(playerInfo.currentFood.getName() != "Plate")
		{
			buttonEat.GetComponent<Image>().enabled = true;
		}
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
	
	private void RefreshStats()
	{
		GameObject stats = GameObject.FindWithTag("Stats");
		
		if(stats != null)
		{
			stats.GetComponent<Stats>().Refresh();
		}
	}
}
