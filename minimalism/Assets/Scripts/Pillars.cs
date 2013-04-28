using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pillars : MonoBehaviour
{
	GameObject player;
	public GameObject pillarPrefab;
	LinkedList<GameObject> pillars;

	public int leftToSend = 0;
	public float pillarDelay = 0.2f;
	float delayLeft = 0f;
	public float zFromPlayer = 100f;

	// Use this for initialization
	void Start()
	{
		player = GameObject.Find("/Player");
		pillars = new LinkedList<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		if (leftToSend > 0) {
			if (delayLeft <= 0f) {
				delayLeft = pillarDelay;
				leftToSend--;
				GameObject p = (GameObject)GameObject.Instantiate(pillarPrefab);
				Pillar pillar = p.GetComponent<Pillar>();
				pillar.Init(player.transform.position.z + zFromPlayer);
				pillars.AddLast(p);
			}
			delayLeft -= Time.deltaTime;
		}
		while (pillars.Count > 0 && 
			pillars.First.Value.transform.position.z < player.transform.position.z) {
				Destroy(pillars.First.Value);
				pillars.RemoveFirst();
		}
	}

	public void SendPillars()
	{
		leftToSend = 3;
	}
}
