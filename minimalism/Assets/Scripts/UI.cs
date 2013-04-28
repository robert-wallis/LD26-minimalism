using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	public static string VERSION = "0.4";
	FLabel version;
	FLabel walkwaysLeft;

	Walkway walkway;

	void Start()
	{
		SetupFutile();
		version = new FLabel("comfortaa20", "v" + VERSION);
		version.anchorX = 1f;
		version.anchorY = 0f;
		version.x = Futile.screen.halfWidth - 20;
		version.y = -Futile.screen.halfHeight + 15;
		Futile.stage.AddChild(version);

		walkway = GetComponent<Walkway>();
		walkwaysLeft = new FLabel("comfortaa32", "");
		walkwaysLeft.anchorX = 1f;
		walkwaysLeft.x = Futile.screen.halfWidth - 20;
		Futile.stage.AddChild(walkwaysLeft);
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

	void Update()
	{
		walkwaysLeft.text = walkway.walkwaysLeft + " left";
	}
}
