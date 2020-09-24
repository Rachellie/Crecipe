using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ObjectSceneChanger : MonoBehaviour, IPointerDownHandler
{
	public string sceneToLoad;
	public Transform player;
	public float range = 2f;
	
	public void OnPointerDown(PointerEventData data)
	{
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		if(distance <= range)
		{
			PlayerData.player.SetPosition(player.transform.position);
			PlayerData.player.SetRotation(player.transform.eulerAngles);
			
			SceneManager.LoadScene(sceneToLoad);
		}
	}
}
