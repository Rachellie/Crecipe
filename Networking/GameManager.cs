using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
	
	public Material red;
    public Material blue;
    public Material green;
    public Material purple;
    public Material orange;

    private void Awake()
    {
        if (instance == null)
        {		
			DontDestroyOnLoad(gameObject);
            instance = this;
			
			SceneManager.sceneLoaded -= OnSceneLoaded;
			SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
        }
    }

    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int _id, string _username, string _color, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            //_player = Instantiate(localPlayerPrefab, _position, _rotation);
			_player = GameObject.FindWithTag("Player");
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
			
			DontDestroyOnLoad(_player);
			Debug.Log("other player created");
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
		if(_id != Client.instance.myId)
		{
			SetColor(_id, _color);
		}
		
		PlayerController local = _player.GetComponent<PlayerController>();
		if(local != null)
		{
			local.connected = true;
		}
    }
	
	public void SetColor(int id, string color)
	{
		foreach(GameObject _player in GameObject.FindGameObjectsWithTag("OtherPlayer"))
		{
			if(_player.GetComponent<PlayerManager>().id == id)
			{
				if (color == "PlayerRed")
				{
					_player.GetComponent<Renderer>().material = red;
				}
				else if (color == "PlayerBlue")
				{
					_player.GetComponent<Renderer>().material = blue;
				}
				else if (color == "PlayerGreen")
				{
					_player.GetComponent<Renderer>().material = green;
				}
				else if (color == "PlayerOrange")
				{
					_player.GetComponent<Renderer>().material = orange;
				}
				else
				{
					_player.GetComponent<Renderer>().material = purple;
				}
				return;
			}
		}
	}
	
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// return if not the start calling scene
		// if(!string.Equals(scene.path, this.scene.path)) return;

		Debug.Log("Re-Initializing", this);
		// do your "Start" stuff here
		GameObject _player;
		
		_player = GameObject.FindWithTag("Player");
		if(_player != null)
		{
			_player.GetComponent<PlayerManager>().id = Client.instance.myId;
			_player.GetComponent<PlayerManager>().username = PlayerData.player.GetName();
			
			players[Client.instance.myId] = _player.GetComponent<PlayerManager>();
		}
		
		if(Client.instance.IsConnected())
		{
			StartCoroutine(SetPos());
		}
	}
	
	IEnumerator SetPos()
	{
		yield return new WaitForSeconds(0.1f);
		ClientSend.PlayerMovement(new bool[]{false, false, false, false});
	}
}
