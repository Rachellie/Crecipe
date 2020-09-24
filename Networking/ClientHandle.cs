using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
		string _color = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _color, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
		if(Client.instance.IsConnected())
		{
			int _id = _packet.ReadInt();
			Vector3 _position = _packet.ReadVector3();

			GameManager.players[_id].transform.position = _position;
		}
    }

    public static void PlayerRotation(Packet _packet)
    {
		if(Client.instance.IsConnected())
		{
			int _id = _packet.ReadInt();
			Quaternion _rotation = _packet.ReadQuaternion();

			GameManager.players[_id].transform.rotation = _rotation;
		}
    }
	
	public static void Disconnect(Packet _packet)
	{
		string _msg = _packet.ReadString();
		Client.instance.OnApplicationQuit();
		Debug.Log("server closed, client is disconnecting");
	}
	
	public static void RemoveClient(Packet _packet)
	{
		int _id = _packet.ReadInt();
		
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("OtherPlayer"))
		{
			if(player.GetComponent<PlayerManager>().id == _id)
			{
				GameManager.players.Remove(_id);
				Destroy(player);
				return;
			}
		}
	}
	
	public static void SetColor(Packet _packet)
	{
		int fromClient = _packet.ReadInt();
		string _color = _packet.ReadString();
		
		foreach(GameObject _player in GameObject.FindGameObjectsWithTag("OtherPlayer"))
		{
			if(_player.GetComponent<PlayerManager>().id == fromClient)
			{
				GameManager.instance.SetColor(fromClient, _color);
				return;
			}
		}
	}
	
	public static void SetFood(Packet _packet)
	{
		int fromClient = _packet.ReadInt();
		string foodName = _packet.ReadString();
		
		foreach(GameObject _player in GameObject.FindGameObjectsWithTag("OtherPlayer"))
		{
			if(_player.GetComponent<PlayerManager>().id == fromClient)
			{
				for(int i = _player.transform.childCount - 1; i >= 2; --i)
				{
					Destroy(_player.transform.GetChild(i).gameObject); // third child is hat
				}
				if(foodName == "null")
				{
					return;
				}
				
				FoodObject food = FindFood(foodName);
				Instantiate(Resources.Load(food.getModel()), _player.transform);
				return;
			}
		}
	}
	
	public static void SetFridge(Packet _packet)
	{
		List<FoodObject> newFridge = new List<FoodObject>();
		
		for(int i = 0; i < 56; ++i)
		{
			string name = _packet.ReadString();
			int quantity = _packet.ReadInt();
			
			if(name != "null")
			{
				FoodObject food = new FoodObject(FindFood(name));
				food.setQuantity(quantity);
				newFridge.Add(food);
			}
			else
			{
				newFridge.Add(null);
			}
		}
		
		PlayerData.player.SetFridge(newFridge);
	}
	
	public static void UpdateFridge(Packet _packet)
	{
		int slot = _packet.ReadInt();
		string name = _packet.ReadString();
		int quantity = _packet.ReadInt();
		
		if(name != "null")
		{
			FoodObject food = new FoodObject(FindFood(name));
			food.setQuantity(quantity);
			PlayerData.player.GetFridge()[slot] = food;
		}
		else
		{
			PlayerData.player.GetFridge()[slot] = null;
		}
		
		if(SceneManager.GetActiveScene().name == "InventoryScene")
		{
			GameObject fridge = GameObject.Find("Content");
			fridge.GetComponent<Bag>().Refresh();
		}
	}
	
	public static void SetCounters(Packet _packet)
	{
		List<FoodObject> newCounters = new List<FoodObject>();
		
		for(int i = 0; i < 36; ++i)
		{
			string name = _packet.ReadString();
			int quantity = _packet.ReadInt();
			
			if(name != "null")
			{
				FoodObject food = new FoodObject(FindFood(name));
				food.setQuantity(quantity);
				newCounters.Add(food);
			}
			else
			{
				newCounters.Add(null);
			}
		}
		
		PlayerData.player.SetCounters(newCounters);
	}
	
	public static void UpdateCounter(Packet _packet)
	{
		int num = _packet.ReadInt();
		string name = _packet.ReadString();
		int quantity = _packet.ReadInt();
		
		if(name != "null")
		{
			FoodObject food = new FoodObject(FindFood(name));
			food.setQuantity(quantity);
			PlayerData.player.GetCounters()[num] = food;
		}
		else
		{
			PlayerData.player.GetCounters()[num] = null;
		}
		
		if(SceneManager.GetActiveScene().name == "KitchenScene")
		{
			GameObject counters = GameObject.Find("Counters");
			
			foreach(Transform counter in counters.transform)
			{
				if(counter.GetComponent<Counter>().counterNum == num)
				{
					counter.GetComponent<Counter>().Refresh();
					return;
				}
			}
		}
	}
	
	public static void SetKettle(Packet _packet)
	{
		// SetAppliance(Kettle.ingredientSet, _packet);
	}
	
	public static void SetKnife(Packet _packet)
	{
		SetAppliance(Knife.ingredientSet, _packet);
	}
	
	public static void SetHand(Packet _packet)
	{
		// SetAppliance(Hand.ingredientSet, _packet);
	}
	
	public static void SetPan(Packet _packet)
	{
		SetAppliance(Pan.ingredientSet, _packet);
	}
	
	public static void SetPot(Packet _packet)
	{
		// SetAppliance(Pot.ingredientSet, _packet);
	}
	
	public static void SetOven(Packet _packet)
	{
		SetAppliance(Oven.ingredientSet, _packet);
	}
	
	public static void SetBlender(Packet _packet)
	{
		// SetAppliance(Blender.ingredientSet, _packet);
	}
	
	public static void SetRiceCooker(Packet _packet)
	{
		// SetAppliance(RiceCooker.ingredientSet, _packet);
	}
	
	public static void UpdateAppliance(Packet _packet)
	{
		string appName = _packet.ReadString();
		string foodName = _packet.ReadString();
		
		FoodObject food = new FoodObject(FindFood(foodName));
		
		if(appName == "Kettle")
		{
			// AddToApp(Kettle.ingredientSet, food);
		}
		else if(appName == "Knife")
		{
			AddToApp(Knife.ingredientSet, food);
		}
		else if(appName == "Hand")
		{
			// AddToApp(Hand.ingredientSet, food);
		}
		else if(appName == "Pan")
		{
			AddToApp(Pan.ingredientSet, food);
		}
		else if(appName == "Pot")
		{
			// AddToApp(Kettle.ingredientSet, food);
		}
		else if(appName == "Oven")
		{
			AddToApp(Oven.ingredientSet, food);
		}
		else if(appName == "Blender")
		{
			// AddToApp(Kettle.ingredientSet, food);
		}
		else if(appName == "RiceCooker")
		{
			// AddToApp(Kettle.ingredientSet, food);
		}
	}
	
	//
	//
	private static FoodObject FindFood(string name)
	{
		foreach(Dictionary<string, FoodObject> dict in PlayerData.player.GetDB())
		{
			if(dict.ContainsKey(name))
			{
				return dict[name];
			}
		}
		
		return null;
	}
	
	public static void SetAppliance(HashSet<FoodObject> ingSet, Packet _packet)
	{
		ingSet.Clear();
		
		int amount = _packet.ReadInt();
		for(int i = 0; i < amount; ++i)
		{
			string name = _packet.ReadString();
			int quantity = _packet.ReadInt();
			
			FoodObject food = new FoodObject(FindFood(name));
			food.setQuantity(quantity);
			
			ingSet.Add(food);
		}
	}
	
	public static void AddToApp(HashSet<FoodObject> foodSet, FoodObject food)
	{
		Debug.Log(food.getQuantity());
		bool notFound = true;
		food.setQuantity(1);
		
		if(food.getName() != "Plate")
		{
			foreach(FoodObject ing in foodSet)
			{
				if(ing.getName() == food.getName())
				{
					ing.addOneQ();
					notFound = false;
					break;
				}
			}
			if(notFound)
			{
				foodSet.Add(food);
			}
		}
		else
		{
			foodSet.Clear();
		}
		
		if(SceneManager.GetActiveScene().name == "KitchenScene")
		{
			GameObject appIng = GameObject.Find("AppIng");

			if(appIng != null)
			{
				appIng.GetComponent<AppIng>().Refresh();
			}
		}
	}
}
