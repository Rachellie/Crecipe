using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customize : MonoBehaviour
{
    public TMP_InputField userInput;
    public Material newMaterial;

    public void SetName()
    {
        userInput.text = userInput.text.Trim();

        if(userInput.text != "")
        {
            PlayerData.player.SetName(userInput.text);
        }
    }

    public void SetMaterial()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Renderer>().material = newMaterial;
        PlayerData.player.SetPlayerMaterial(newMaterial.name);
		ClientSend.PlayerColor(newMaterial.name);
    }
}
