using System;
using System.Collections.Generic;
using UnityEngine;

class GameState : MonoBehaviour
{
	GameOver gameover;

	GameObject player;
	GameObject backgroundMusic;
	Vector3 playerStartPosition;
	Quaternion playerStartRotation;
	public static GameState instance;

	bool started = false;

	void Awake()
	{
		instance = this; // not a CS singleton, but is a Unity singleton
		gameover = GetComponent<GameOver>();
		backgroundMusic = GameObject.Find("/environment/background music");
	}

	public void StartGame()
	{
		gameover.enabled = false;
		backgroundMusic.audio.Play();
		SendMessageUpwards("GameStarting", SendMessageOptions.DontRequireReceiver);
		player = GameObject.Find("/Player");
	}

	public void RestartGame()
	{
		SendMessageUpwards("Cleanup", SendMessageOptions.DontRequireReceiver);
		StartGame();
	}

	public void GameOver(GameObject cause)
	{
		gameover.enabled = true;
		SendMessageUpwards("GameEnding", SendMessageOptions.DontRequireReceiver);
		GA.API.Design.NewEvent("player:gameover:" + cause.name, player.transform.position);
		backgroundMusic.audio.Stop();
	}

	public void GameWin()
	{
		gameover.enabled = true;
		SendMessageUpwards("GameEnding", SendMessageOptions.DontRequireReceiver);
		GA.API.Design.NewEvent("player:gamewin:walkwaysLeft", (float)GetComponent<Aaron>().walkwaysLeft);
		backgroundMusic.audio.Stop();
	}
}
