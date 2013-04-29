using System;
using System.Collections.Generic;
using UnityEngine;

// some obstacles that the player must overcome
public class BlockingSystem : MonoBehaviour
{
	GameObject player;
	public GameObject prefab;
	LinkedList<GameObject> blockingObjects;

	public int leftToSend = 0;
	public int amountToSend = 3;
	public float sendBetweenDelay = 0.2f;
	float delayLeft = 0f;
	public float zInFrontOfPlayer = 150f;
	public bool started = false;

	// Use this for initialization
	void Start()
	{
		blockingObjects = new LinkedList<GameObject>();
	}

	void GameStarting()
	{
		player = GameObject.Find("/Player");
		started = true;
	}

	void GameEnding()
	{
		started = false;
	}

	public void Cleanup()
	{
		leftToSend = 0;
		delayLeft = 0f;
		while (blockingObjects.Count > 0) {
			Destroy(blockingObjects.First.Value);
			blockingObjects.RemoveFirst();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!started) {
			return;
		}

		if (leftToSend > 0) {
			if (delayLeft <= 0f) {
				delayLeft = sendBetweenDelay;
				leftToSend--;
				GameObject p = (GameObject)GameObject.Instantiate(prefab);
				BlockingObject pillar = p.GetComponent<BlockingObject>();
				pillar.Init(player.transform.position.z + zInFrontOfPlayer);
				blockingObjects.AddLast(p);
			}
			delayLeft -= Time.deltaTime;
		}
		while (blockingObjects.Count > 0 &&
			blockingObjects.First.Value.transform.position.z < player.transform.position.z) {
			Destroy(blockingObjects.First.Value);
			blockingObjects.RemoveFirst();
		}
	}

	public void SendBlocks()
	{
		leftToSend = amountToSend;
	}

}
