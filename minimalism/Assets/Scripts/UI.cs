using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	public static string VERSION = "1.01";
	FLabel version;
	FLabel walkwaysLeft;
	FSprite logo;
	FLabel youWin;
	FLabel youLoose;
	GameObject futilecamera;

	Aaron aaron;

	bool playing = false;

	void Start()
	{
		futilecamera = GameObject.Find("/Futile/Camera");
		SetupFutile();
		version = new FLabel("comfortaa20", "v" + VERSION);
		version.anchorX = 1f;
		version.anchorY = 0f;
		version.x = Futile.screen.halfWidth - 20;
		version.y = -Futile.screen.halfHeight + 15;
		Futile.stage.AddChild(version);

		aaron = GetComponent<Aaron>();
		walkwaysLeft = new FLabel("comfortaa32", "");
		walkwaysLeft.anchorY = 0f;
		walkwaysLeft.y = -Futile.screen.halfHeight + 20;
		Futile.stage.AddChild(walkwaysLeft);

		logo = new FSprite("logo");
		Futile.stage.AddChild(logo);

		youWin = new FLabel("comfortaa32", "Congratulations You Win");
		youLoose = new FLabel("comfortaa32", "Game Over");
	}

	void SetupFutile()
	{
		FutileParams futileParams = new FutileParams(true, true, false, false);
		futileParams.AddResolutionLevel(960f, 1.0f, 1.0f, "");
		futileParams.AddResolutionLevel(1920f, 2.0f, 2.0f, "");
		futileParams.origin = new Vector2(0.5f, 0.5f);
		futileParams.debugLog = false;
		Futile.instance.Init(futileParams);

		Futile.atlasManager.LoadAtlas("UI/UI");
		Futile.atlasManager.LoadFont("comfortaa20", "comfortaa20_0", "UI/comfortaa20", 0f, 0f);
		Futile.atlasManager.LoadFont("comfortaa32", "comfortaa32_0", "UI/comfortaa32", 0f, 0f);
	}

	void GameStarting()
	{
		futilecamera.SetActive(false);
		playing = true;
		Futile.stage.RemoveChild(logo);
		Futile.stage.RemoveChild(youWin);
		Futile.stage.RemoveChild(youLoose);
		Futile.stage.RemoveChild(version);
	}

	void GameEnding()
	{
		futilecamera.SetActive(true);
		playing = false;
		Futile.stage.AddChild(version);
	}

	void GameWinning()
	{
		Futile.stage.AddChild(youWin);
	}

	void GameLoose()
	{
		Futile.stage.AddChild(youLoose);
	}

	void Update()
	{
		if (playing) {
			walkwaysLeft.text = aaron.walkwaysLeft + " left";
		}
	}
}
