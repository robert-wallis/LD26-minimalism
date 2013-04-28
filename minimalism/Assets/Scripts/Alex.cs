using UnityEngine;
using System.Collections;

public class Alex : MonoBehaviour
{
	public BlockingSystem pillars;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) {
			SendPillars();
		}
	}

	public void SendPillars()
	{
		pillars.SendBlock();
	}
}
