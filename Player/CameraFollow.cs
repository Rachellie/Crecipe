using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform player;
	public Transform cam;
	
	public float x = 0f;
	public float y = 3f;
	public float z = -4f;
	
	public float multiplier = 1f;
	
    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(
			player.transform.position.x*multiplier + x, 
			player.transform.position.y*multiplier + y, 
			player.transform.position.z*multiplier + z);
    }
}
