using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientSend : MonoBehaviour
{
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(PlayerData.player.GetName());
			_packet.Write(PlayerData.player.GetPlayerMaterial());
			
			GameObject player = GameObject.FindWithTag("Player");
			if(player != null)
			{
				_packet.Write(player.transform.position);
			}
			else
			{
				_packet.Write(new Vector3(0, 0, 0));
			}

            SendTCPData(_packet);
        }
    }

    /// <summary>Sends player input to the server.</summary>
    /// <param name="_inputs"></param>
    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
			
			if(GameManager.players[Client.instance.myId] != null)
			{
				_packet.Write(GameManager.players[Client.instance.myId].transform.rotation);
				_packet.Write(GameManager.players[Client.instance.myId].transform.position);
			}
			else
			{
				_packet.Write(new Quaternion(0, 0, 0, 0));
				
				if(SceneManager.GetActiveScene().name == "InventoryScene")
				{
					_packet.Write(PlayerData.player.GetPosition());
				}
				else
				{
					_packet.Write(new Vector3(0, -10, 0));
				}
			}

            SendUDPData(_packet);
        }
    }
	
	public static void PlayerColor(string color)
	{
		using (Packet _packet = new Packet((int)ClientPackets.playerColor))
        {
			_packet.Write(color);
			
			SendTCPData(_packet);
		}
	}
	
	public static void PlayerFood(string food)
	{
		using (Packet _packet = new Packet((int)ClientPackets.playerFood))
        {
			_packet.Write(food);
			
			SendTCPData(_packet);
		}
	}
	
	public static void UpdateFridge(int slot)
	{
		using (Packet _packet = new Packet((int)ClientPackets.updateFridge))
        {
			_packet.Write(slot);
			
			FoodObject food = PlayerData.player.GetFridge()[slot];
			if(food != null)
			{
				_packet.Write(food.getName());
				_packet.Write(food.getQuantity());
			}
			else
			{
				_packet.Write("null");
				_packet.Write(0);
			}
			
			SendTCPData(_packet);
		}
	}
	
	public static void UpdateCounter(int num)
	{
		using (Packet _packet = new Packet((int)ClientPackets.updateCounter))
        {
			_packet.Write(num);
			
			FoodObject food = PlayerData.player.GetCounterFood(num);
			if(food != null)
			{
				_packet.Write(food.getName());
				_packet.Write(food.getQuantity());
			}
			else
			{
				_packet.Write("null");
				_packet.Write(0);
			}
			
			SendTCPData(_packet);
		}
	}
	
	public static void UpdateAppliance(string appName, string foodName)
	{
		using (Packet _packet = new Packet((int)ClientPackets.updateAppliance))
        {
			_packet.Write(appName);
			_packet.Write(foodName);
			
			SendTCPData(_packet);
		}
	}
    #endregion
}
