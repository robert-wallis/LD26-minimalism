using System;
using System.Collections.Generic;
using UnityEngine;

class GameState : MonoBehaviour
{
	Walkway walkway;
	Controls controls;
	GameObject player;
	Vector3 playerStartPosition;
	Quaternion playerStartRotation;
	GameOver gameover;
	public static GameState instance;

	void Awake()
	{
		instance = this; // not a CS singleton, but is a Unity singleton
		walkway = GetComponent<Walkway>();
		controls = GetComponent<Controls>();
		player = GameObject.Find("/Player");
		gameover = GetComponent<GameOver>();
		gameover.enabled = false;
	}

	void Update()
	{
		// game over
		if (player.transform.position.y < -10) {
			GameOver(this.gameObject);
		}
	}

	public void StartGame()
	{
		gameover.enabled = false;
		walkway.enabled = true;
		controls.enabled = true;
		SendMessageUpwards("Cleanup", SendMessageOptions.DontRequireReceiver);
		GameObject.Find("/Player/Mesh").renderer.enabled = true;
	}

	public void GameOver(GameObject cause)
	{
		gameover.enabled = true;
		walkway.enabled = false;
		controls.enabled = false;
		GameObject.Find("/Player/Mesh").renderer.enabled = false;
		GA.API.Design.NewEvent("player:gameover:" + cause.name, player.transform.position);
	}
}
