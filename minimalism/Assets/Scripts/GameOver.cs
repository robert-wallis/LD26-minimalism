using System;
using System.Collections.Generic;
using UnityEngine;

class GameOver : MonoBehaviour
{
	GameState gamestate;
	float waitLeft = 5f;
	FLabel gameOver;
	FLabel countDown;
	bool enableStartup = true;

	void Start()
	{
		gamestate = GetComponent<GameState>();
		gameOver = new FLabel("comfortaa32", "Game Over");
		countDown = new FLabel("comfortaa32", "");
	}

	void OnEnable()
	{
		enableStartup = true;
	}

	void OnEnableStartup()
	{
		enableStartup = false;
		waitLeft = 5f;
		countDown.y = -Futile.screen.halfHeight * .5f;
		Futile.stage.AddChild(gameOver);
		Futile.stage.AddChild(countDown);
	}

	void OnDisable()
	{
		if (null != gameOver) {
			Futile.stage.RemoveChild(gameOver);
		}
		if (null != countDown) {
			Futile.stage.RemoveChild(countDown);
		}
	}

	void Update()
	{
		if (enableStartup) {
			OnEnableStartup();
		}
		countDown.text = "" + (int)(waitLeft + 1f);
		if (waitLeft < 0) {
			gamestate.StartGame();
		}
		waitLeft -= Time.deltaTime;
	}
}
