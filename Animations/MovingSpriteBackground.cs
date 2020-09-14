using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSpriteBackground : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    public RectTransform rectTransform;
	public RawImage image;
	public float offset = 0f;

    void Start()
    {
        //
    }
	
    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;
        image.uvRect = new Rect(offset, offset, 1.2f, 1.8f);
    }
}
