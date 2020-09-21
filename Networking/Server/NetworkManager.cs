using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameServer
{
	public class NetworkManager : MonoBehaviour
	{
		public static NetworkManager instance;
		
		private static bool serverStarted = false;

		public GameObject playerPrefab;

		private void Awake()
		{
			if (instance == null)
			{
				DontDestroyOnLoad(gameObject);
				instance = this;
			}
			else if (instance != this)
			{
				Debug.Log("Instance already exists, destroying object!");
				Destroy(this);
			}
		}

		private void Start()
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;

			// call StartServer()
		}
		
		public bool ServerStarted()
		{
			return serverStarted;
		}
		
		public void StartServer(int maxPlayers)
		{
            //need to change max players here
			Server.Start(maxPlayers, 25565);
			serverStarted = true;
		}
		
		public void OnApplicationQuit()
		{
			serverStarted = false;
			Server.Stop();
		}

		public Player InstantiatePlayer()
		{
			return Instantiate(playerPrefab, new Vector3(0, 7, 0), Quaternion.identity).GetComponent<Player>();
			//return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
		}
	}
}
