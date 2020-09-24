using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimator : MonoBehaviour
{
	public float speedX = 0.1f;
	public float speedY = 0.1f;
	
	private float currentX;
	private float currentY;
	
    // Start is called before the first frame update
    void Start()
    {
        currentX = GetComponent<Renderer>().material.mainTextureOffset.x;
		currentY = GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentX += Time.deltaTime * speedX;
		currentY += Time.deltaTime * speedY;
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(currentX, currentY));
    }
}
