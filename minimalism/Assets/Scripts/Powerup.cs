using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
	Walkway walkway;
	public uint value = 11;

	// Use this for initialization
	void Start()
	{
		walkway = GameObject.Find("game").GetComponent<Walkway>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.collider.name == "Player") {
			walkway.walkwaysLeft += value;
			audio.Play();
			Destroy(this);
		}
	}
}
