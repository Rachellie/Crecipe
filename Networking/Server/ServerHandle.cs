using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameServer
{
	public class ServerHandle
	{
		public static void WelcomeReceived(int _fromClient, Packet _packet)
		{
			int _clientIdCheck = _packet.ReadInt();
			string _username = _packet.ReadString();
			string _color = _packet.ReadString();
			Vector3 _position = _packet.ReadVector3();

			Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
			if (_fromClient != _clientIdCheck)
			{
				Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
			}
			Server.clients[_fromClient].SendIntoGame(_username, _color, _position);
		}

		public static void PlayerMovement(int _fromClient, Packet _packet)
		{
			bool[] _inputs = new bool[_packet.ReadInt()];
			for (int i = 0; i < _inputs.Length; i++)
			{
				_inputs[i] = _packet.ReadBool();
			}
			Quaternion _rotation = _packet.ReadQuaternion();
			
			Vector3 _position = _packet.ReadVector3();

			Server.clients[_fromClient].player.SetInput(_position, _rotation);
		}
		
		public static void PlayerColor(int _fromClient, Packet _packet)
		{
			string color = _packet.ReadString();
			ServerSend.SetColor(_fromClient, color);
		}
	}
}
