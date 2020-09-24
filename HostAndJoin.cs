using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HostAndJoin : MonoBehaviour
{
	public TMP_InputField ipAddress;
    public static int maxPlayers = 3;
	
	public bool isSettingsPanel = false;
	
	public Button buttonHost;
	public Button buttonHostDisconnect;
	
	public Button buttonJoin;
	public Button buttonClientDisconnect;
	
	void Start()
	{
		if(isSettingsPanel)
		{
			SetButtons();
		}
	}
	
	public void SetMaxPlayers(int num)
	{
		maxPlayers = num;
	}
	
	public void SetButtons()
	{
		StartCoroutine(SetButtonsWait());
	}
	
	IEnumerator SetButtonsWait()
	{
		yield return new WaitForSeconds(0.1f);
		
		if(Client.instance.IsConnected())
		{
			buttonClientDisconnect.gameObject.SetActive(true);
			buttonJoin.gameObject.SetActive(false);
			buttonHost.gameObject.SetActive(false);
			buttonHostDisconnect.gameObject.SetActive(false);
		}
		else
		{
			buttonJoin.gameObject.SetActive(true);
			buttonClientDisconnect.gameObject.SetActive(false);
			//buttonHost.gameObject.SetActive(true);
			//buttonHostDisconnect.gameObject.SetActive(false);
		}
		
		if(GameServer.NetworkManager.instance.ServerStarted())
		{
			buttonHostDisconnect.gameObject.SetActive(true);
			buttonHost.gameObject.SetActive(false);
			buttonJoin.gameObject.SetActive(false);
			buttonClientDisconnect.gameObject.SetActive(false);
		}
		else
		{
			buttonHost.gameObject.SetActive(true);
			buttonHostDisconnect.gameObject.SetActive(false);
			//buttonJoin.gameObject.SetActive(true);
			//buttonClientDisconnect.gameObject.SetActive(false);
		}
	}
		
	public void StartServer()
	{
		GameServer.NetworkManager.instance.StartServer(maxPlayers);
		GameObject.Find("SettingsPanel").GetComponent<HostAndJoin>().SetButtons();
		
		JoinServer("127.0.0.1");
	}
	
	public void JoinServer(string ip)
	{
		Client.instance.ip = ip;
		Client.instance.ConnectToServer();
	}
	
	public void StopServer()
	{
		GameServer.NetworkManager.instance.OnApplicationQuit();
		GameObject.Find("SettingsPanel").GetComponent<HostAndJoin>().SetButtons();
	}
	
	public void JoinServer()
	{
		Client.instance.ip = ipAddress.text;
		Client.instance.ConnectToServer();
		GameObject.Find("SettingsPanel").GetComponent<HostAndJoin>().SetButtons();
	}
	
	public void LeaveServer()
	{
		Client.instance.OnApplicationQuit();
		GameObject.Find("SettingsPanel").GetComponent<HostAndJoin>().SetButtons();
	}
}
