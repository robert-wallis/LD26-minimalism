using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
	Aaron aaron;
	public uint value = 11;

	// Use this for initialization
	void Start()
	{
		aaron = GameObject.Find("game").GetComponent<Aaron>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.collider.name == "Player") {
			aaron.walkwaysLeft += value;
			audio.Play();
			Destroy(this);
		}
	}
}
