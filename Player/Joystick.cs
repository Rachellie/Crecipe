using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public CharacterController player;
	public RectTransform button;
    public float speed = 5.0f;
	
	private bool clicked = false;
    private bool touchStart = false;
    private Vector3 pointA;
    private Vector3 pointB;
	
	private AudioSource audio;
	private Terrain terrain;
	private int posX;
	private int posZ;
	public float[] textureValues;
	
	public AudioClip grassSound;
	public AudioClip pathSound;
	public AudioClip sandSound;

	void Start()
    {
		transform.position = PlayerData.player.GetPosition();
		transform.Rotate(PlayerData.player.GetRotation());
		
		PlayerData.player.SetCurrentFood(PlayerData.player.GetCurrentFood());
		
		terrain = Terrain.activeTerrain;
		audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update()
	{
		if(clicked)
		{
			if(Input.GetMouseButtonDown(0))
			{
				pointA = new Vector3(button.transform.position.x/Screen.width, 0, button.transform.position.y/Screen.height);
			}
			if(Input.GetMouseButton(0))
			{
				touchStart = true;
				pointB = new Vector3(Input.mousePosition.x/Screen.width, 0, Input.mousePosition.y/Screen.height);
			}
			else
			{
				touchStart = false;
			}
		}
		else
		{
			touchStart = false;
		}
	}
	
	public void clickOn()
	{
		clicked = true;
	}
	
	public void clickOff()
	{
		clicked = false;
	}
	
	private void FixedUpdate()
	{
        if(touchStart)
		{
            Vector3 offset = (pointB - pointA) * speed * 15;
            Vector3 direction = Vector3.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);
        }
		
		if(player.isGrounded && touchStart && !audio.isPlaying)
		{
			PlayFootstep();
		}
	}
	
	void moveCharacter(Vector3 direction)
	{
		player.SimpleMove(direction * speed);
		player.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
	
	private void GetTerrainTexture()
	{
		ConvertPosition(player.transform.position);
		CheckTexture();
	}
	
	private void ConvertPosition(Vector3 playerPos)
	{
		Vector3 terrainPos = playerPos - terrain.transform.position;
		
		Vector3 mapPosition = new Vector3
		(
			terrainPos.x / terrain.terrainData.size.x, 0, 
			terrainPos.z / terrain.terrainData.size.z
		);
		
		float xCoord = mapPosition.x * terrain.terrainData.alphamapWidth;
		float zCoord = mapPosition.z * terrain.terrainData.alphamapHeight;
		
		posX = (int)xCoord;
		posZ = (int)zCoord;
	}
	
	private void CheckTexture()
	{
		float[,,] alphaMap = terrain.terrainData.GetAlphamaps(posX, posZ, 1, 1);
		textureValues[0] = alphaMap[0,0,0];
		textureValues[1] = alphaMap[0,0,1];
		textureValues[2] = alphaMap[0,0,2];
		textureValues[3] = alphaMap[0,0,3];
		textureValues[4] = alphaMap[0,0,4];
	}
	
	private void PlayFootstep()
	{
		audio.volume = Random.Range(0.1f, 0.3f);
		audio.pitch = Random.Range(0.8f, 1.1f);
		
		if(terrain != null)
		{	
			GetTerrainTexture();
			
			if(textureValues[0] > 0)
			{
				// grass
				audio.PlayOneShot(grassSound, textureValues[0]);
			}
			if(textureValues[1] > 0)
			{
				// water
			}
			if(textureValues[2] > 0)
			{
				// dirt
			}
			if(textureValues[3] > 0)
			{
				// sand
				audio.PlayOneShot(sandSound, textureValues[3]);
			}
			if(textureValues[4] > 0)
			{
				// path
				audio.PlayOneShot(pathSound, textureValues[4]);
			}
		}
		else
		{
			// play a default sound
			audio.Play();
		}
	}
}
