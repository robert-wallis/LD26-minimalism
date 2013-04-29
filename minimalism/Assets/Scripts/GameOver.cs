using System;
using System.Collections.Generic;
using UnityEngine;

class GameOver : MonoBehaviour
{
	GameState gamestate;
	float waitLeft = 15f;
	FLabel countDown;
	bool enableStartup = true;

	void Start()
	{
		gamestate = GetComponent<GameState>();
		countDown = new FLabel("comfortaa32", "");
	}

	void OnEnable()
	{
		enableStartup = true;
	}

	void OnEnableStartup()
	{
		enableStartup = false;
		waitLeft = 15f;
		countDown.y = -Futile.screen.halfHeight * .5f;
		Futile.stage.AddChild(countDown);
	}

	void CleanupUI()
	{
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
			CleanupUI();
			gamestate.RestartGame();
		}
		waitLeft -= Time.deltaTime;
	}
}
