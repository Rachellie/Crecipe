using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int sceneToLoad;
    public Vector3 playerPosition;
	public Vector3 playerRotation;

    void OnTriggerEnter(Collider other)
    {
        PlayerData.player.SetPosition(playerPosition);
		PlayerData.player.SetRotation(playerRotation);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void setPosition()
    {
        PlayerData.player.SetPosition(playerPosition);
		PlayerData.player.SetRotation(playerRotation);
        SceneManager.LoadScene(sceneToLoad);
    }
	
	public void ChangeScene()
	{
		SceneManager.LoadScene(sceneToLoad);
	}
}