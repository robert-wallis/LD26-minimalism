using System;
using System.Collections.Generic;
using UnityEngine;

class GameState : MonoBehaviour
{

	GameObject player;
	GameObject backgroundMusic;
	Vector3 playerStartPosition;
	Quaternion playerStartRotation;
	public static GameState instance;

	void Awake()
	{
		instance = this; // not a CS singleton, but is a Unity singleton
		backgroundMusic = GameObject.Find("/environment/background music");
	}

	public void StartGame()
	{
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
		SendMessageUpwards("GameEnding", SendMessageOptions.DontRequireReceiver);
		SendMessageUpwards("GameLoose", SendMessageOptions.DontRequireReceiver);
		GA.API.Design.NewEvent("player:gameover:" + cause.name, player.transform.position);
		backgroundMusic.audio.Stop();
	}

	public void GameWin()
	{
		SendMessageUpwards("GameEnding", SendMessageOptions.DontRequireReceiver);
		SendMessageUpwards("GameWinning", SendMessageOptions.DontRequireReceiver);
		GA.API.Design.NewEvent("player:gamewin:walkwaysLeft", (float)GetComponent<Aaron>().walkwaysLeft);
		backgroundMusic.audio.Stop();
	}
}
