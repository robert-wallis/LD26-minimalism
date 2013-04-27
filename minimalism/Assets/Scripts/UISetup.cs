using UnityEngine;
using System.Collections;

public class UISetup : MonoBehaviour
{
	public static string VERSION = "0.1";
	FLabel version;

	void Awake()
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

	void Start()
	{
		version = new FLabel("comfortaa20", VERSION);
		version.anchorX = 1f;
		version.anchorY = 0f;
		version.x = Futile.screen.halfWidth - 20;
		version.y = -Futile.screen.halfHeight + 15;
		Futile.stage.AddChild(version);

		FLabel test = new FLabel("comfortaa20", "v" + VERSION);
		test.color = Color.red;
		Futile.stage.AddChild(test);
	}
}
