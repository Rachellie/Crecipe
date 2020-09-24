using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameServer
{
	public class ServerSend
	{
		/// <summary>Sends a packet to a client via TCP.</summary>
		/// <param name="_toClient">The client to send the packet the packet to.</param>
		/// <param name="_packet">The packet to send to the client.</param>
		private static void SendTCPData(int _toClient, Packet _packet)
		{
			_packet.WriteLength();
			Server.clients[_toClient].tcp.SendData(_packet);
		}

		/// <summary>Sends a packet to a client via UDP.</summary>
		/// <param name="_toClient">The client to send the packet the packet to.</param>
		/// <param name="_packet">The packet to send to the client.</param>
		private static void SendUDPData(int _toClient, Packet _packet)
		{
			_packet.WriteLength();
			Server.clients[_toClient].udp.SendData(_packet);
		}

		/// <summary>Sends a packet to all clients via TCP.</summary>
		/// <param name="_packet">The packet to send.</param>
		private static void SendTCPDataToAll(Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				Server.clients[i].tcp.SendData(_packet);
			}
		}
		/// <summary>Sends a packet to all clients except one via TCP.</summary>
		/// <param name="_exceptClient">The client to NOT send the data to.</param>
		/// <param name="_packet">The packet to send.</param>
		private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				if (i != _exceptClient)
				{
					Server.clients[i].tcp.SendData(_packet);
				}
			}
		}

		/// <summary>Sends a packet to all clients via UDP.</summary>
		/// <param name="_packet">The packet to send.</param>
		private static void SendUDPDataToAll(Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				Server.clients[i].udp.SendData(_packet);
			}
		}
		/// <summary>Sends a packet to all clients except one via UDP.</summary>
		/// <param name="_exceptClient">The client to NOT send the data to.</param>
		/// <param name="_packet">The packet to send.</param>
		private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
		{
			_packet.WriteLength();
			for (int i = 1; i <= Server.MaxPlayers; i++)
			{
				if (i != _exceptClient)
				{
					Server.clients[i].udp.SendData(_packet);
				}
			}
		}

		#region Packets
		/// <summary>Sends a welcome message to the given client.</summary>
		/// <param name="_toClient">The client to send the packet to.</param>
		/// <param name="_msg">The message to send.</param>
		public static void Welcome(int _toClient, string _msg)
		{
			using (Packet _packet = new Packet((int)ServerPackets.welcome))
			{
				_packet.Write(_msg);
				_packet.Write(_toClient);

				SendTCPData(_toClient, _packet);
			}
			
			SetFridge(_toClient);
			SetCounters(_toClient);
			
			SetKettle(_toClient);
			SetKnife(_toClient);
			SetHand(_toClient);
			SetPan(_toClient);
			SetPot(_toClient);
			SetOven(_toClient);
			SetBlender(_toClient);
			SetRiceCooker(_toClient);
		}
		
		public static void DisconnectClients(string _msg)
		{
			using (Packet _packet = new Packet((int)ServerPackets.disconnect))
			{
				_packet.Write(_msg);

				SendTCPDataToAll(_packet);
			}
		}

		/// <summary>Tells a client to spawn a player.</summary>
		/// <param name="_toClient">The client that should spawn the player.</param>
		/// <param name="_player">The player to spawn.</param>
		public static void SpawnPlayer(int _toClient, Player _player)
		{
			using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
			{
				_packet.Write(_player.id);
				_packet.Write(_player.username);
				_packet.Write(_player.color);
				_packet.Write(_player.transform.position);
				_packet.Write(_player.transform.rotation);

				SendTCPData(_toClient, _packet);
			}
		}

		/// <summary>Sends a player's updated position to all clients.</summary>
		/// <param name="_player">The player whose position to update.</param>
		public static void PlayerPosition(Player _player)
		{
			using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
			{
				_packet.Write(_player.id);
				_packet.Write(_player.transform.position);

				// SendUDPDataToAll(_packet);
				SendUDPDataToAll(_player.id, _packet);
			}
		}

		/// <summary>Sends a player's updated rotation to all clients except to himself (to avoid overwriting the local player's rotation).</summary>
		/// <param name="_player">The player whose rotation to update.</param>
		public static void PlayerRotation(Player _player)
		{
			using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
			{
				_packet.Write(_player.id);
				_packet.Write(_player.transform.rotation);

				SendUDPDataToAll(_player.id, _packet);
			}
		}
		
		public static void RemoveClient(Player _player)
		{
			using (Packet _packet = new Packet((int)ServerPackets.removeClient))
			{
				_packet.Write(_player.id);

				SendUDPDataToAll(_player.id, _packet);
			}
		}
		
		public static void SetColor(int fromClient, string color)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setColor))
			{
				_packet.Write(fromClient);
				_packet.Write(color);
				
				SendTCPDataToAll(_packet);
			}
		}
		
		public static void SetFood(int fromClient, string food)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setFood))
			{
				_packet.Write(fromClient);
				_packet.Write(food);
				
				SendTCPDataToAll(_packet);
			}
		}
		
		public static void SetFridge(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setFridge))
			{
				foreach(FoodObject food in PlayerData.player.GetFridge())
				{
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
				}
				
				SendTCPData(_toClient, _packet);
			}
		}
		
		public static void UpdateFridge(int fromClient, int slot, string name, int quantity)
		{
			using (Packet _packet = new Packet((int)ServerPackets.updateFridge))
			{
				_packet.Write(slot);
				_packet.Write(name);
				_packet.Write(quantity);
				
				SendTCPDataToAll(fromClient, _packet);
			}
		}
		
		public static void SetCounters(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setCounters))
			{
				foreach(FoodObject food in PlayerData.player.GetCounters())
				{
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
				}
				
				SendTCPData(_toClient, _packet);
			}
		}
		
		public static void UpdateCounter(int fromClient, int num, string name, int quantity)
		{
			using (Packet _packet = new Packet((int)ServerPackets.updateCounter))
			{
				_packet.Write(num);
				_packet.Write(name);
				_packet.Write(quantity);
				
				SendTCPDataToAll(fromClient, _packet);
			}
		}
		
		public static void SetKettle(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setKettle))
			{
				//
			}
		}
		
		public static void SetKnife(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setKnife))
			{
				_packet.Write(Knife.ingredientSet.Count);
				
				foreach(FoodObject food in Knife.ingredientSet)
				{
					_packet.Write(food.getName());
					_packet.Write(food.getQuantity());
				}
				
				SendTCPData(_toClient, _packet);
			}
		}
		
		public static void SetHand(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setHand))
			{
				//
			}
		}
		
		public static void SetPan(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setPan))
			{
				_packet.Write(Pan.ingredientSet.Count);
				
				foreach(FoodObject food in Pan.ingredientSet)
				{
					_packet.Write(food.getName());
					_packet.Write(food.getQuantity());
				}
				
				SendTCPData(_toClient, _packet);
			}
		}
		
		public static void SetPot(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setPot))
			{
				//
			}
		}
		
		public static void SetOven(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setOven))
			{
				_packet.Write(Oven.ingredientSet.Count);
				
				foreach(FoodObject food in Oven.ingredientSet)
				{
					_packet.Write(food.getName());
					_packet.Write(food.getQuantity());
				}
				
				SendTCPData(_toClient, _packet);
			}
		}
		
		public static void SetBlender(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setBlender))
			{
				//
			}
		}
		
		public static void SetRiceCooker(int _toClient)
		{
			using (Packet _packet = new Packet((int)ServerPackets.setRiceCooker))
			{
				//
			}
		}
		
		public static void UpdateAppliance(int fromClient, string appName, string foodName)
		{
			using (Packet _packet = new Packet((int)ServerPackets.updateAppliance))
			{
				_packet.Write(appName);
				_packet.Write(foodName);
				
				SendTCPDataToAll(fromClient, _packet);
			}
		}
		#endregion
	}
}
