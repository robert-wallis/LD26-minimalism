using UnityEngine;
using System.Collections;

public class Alex : MonoBehaviour
{
	Pillars pillars;

	// Use this for initialization
	void Start()
	{
		pillars = GetComponent<Pillars>();
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
		pillars.SendPillars();
	}
}
