using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameServer
{
	public class Player : MonoBehaviour
	{
		public int id;
		public string username;
		public string color;

		//private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
		private bool[] inputs;
		
		void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
		
		public void Initialize(int _id, string _username, string _color, Vector3 _position)
		{
			id = _id;
			username = _username;
			color = _color;
			transform.position = _position;

			inputs = new bool[4];
		}

		/// <summary>Processes player input and moves the player.</summary>
		public void FixedUpdate()
		{
			Vector2 _inputDirection = Vector2.zero;
			if (inputs[0])
			{
				_inputDirection.y += 1;
			}
			if (inputs[1])
			{
				_inputDirection.y -= 1;
			}
			if (inputs[2])
			{
				_inputDirection.x -= 1;
			}
			if (inputs[3])
			{
				_inputDirection.x += 1;
			}

			Move(_inputDirection);
		}

		/// <summary>Calculates the player's desired movement direction and moves him.</summary>
		/// <param name="_inputDirection"></param>
		private void Move(Vector2 _inputDirection)
		{
			Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
			//transform.position += _moveDirection * moveSpeed;

			ServerSend.PlayerPosition(this);
			ServerSend.PlayerRotation(this);
		}

		/// <summary>Updates the player input with newly received input.</summary>
		/// <param name="_inputs">The new key inputs.</param>
		/// <param name="_rotation">The new rotation.</param>
		public void SetInput(Vector3 _position, Quaternion _rotation)
		{
			transform.position = _position;
			transform.rotation = _rotation;
		}
	}
}
