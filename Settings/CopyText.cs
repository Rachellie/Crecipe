using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;

public class CopyText : MonoBehaviour
{
    public void Copy()
    {
        WebClient webClient = new WebClient();
        string publicIp = webClient.DownloadString("https://api.ipify.org");

        GUIUtility.systemCopyBuffer = publicIp;
    }
}
