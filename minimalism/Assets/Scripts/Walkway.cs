using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Walkway : MonoBehaviour
{
	public GameObject walkwayPrefab;
	public GameObject powerupPrefab;
	public GameObject pushObject;
	public GameObject player;
	float walkwayGenerationDistance = 100f;
	Vector3 incrementPosition;
	Vector3 currentPosition;
	Quaternion currentRotation;
	Vector3 powerupHeight = new Vector3(0f, 5f, 0f);
	LinkedList<GameObject> walkways;
	LinkedList<GameObject> powerups;

	float lastHigh = 1000f;
	float generatePowerup = 100f;

	void Start()
	{
		walkways = new LinkedList<GameObject>();
		powerups = new LinkedList<GameObject>();
		currentPosition = new Vector3(0.0f, -1.0f, 0.0f);
		currentRotation = walkwayPrefab.transform.rotation;
		incrementPosition = new Vector3(0.0f, 0.0f, 10.0f);
	}

	void Update()
	{
	}

	void FixedUpdate()
	{
		GenerateWalkways();
		if (player.transform.position.z > lastHigh) {
			GA.API.Design.NewEvent("player:walkway:high", player.transform.position.z);
			lastHigh = (((int)player.transform.position.z / 1000) * 1000) + 1000f;
		}
		if (player.transform.position.z > generatePowerup) {
			generatePowerup = (((int)player.transform.position.z / 100) * 100) + 100f;
			GeneratePowerup();
		}
	}

	void GenerateWalkways()
	{
		// make new walkways
		while ((player.transform.position - currentPosition).magnitude < walkwayGenerationDistance) {
			GameObject newWalkway = (GameObject)Instantiate(walkwayPrefab, currentPosition, currentRotation);
			newWalkway.name = "walkway " + currentPosition.z;
			newWalkway.transform.parent = pushObject.transform;
			walkways.AddLast(newWalkway);
			currentPosition += incrementPosition;
		}
		// the first walkway ahead of us will stop the loop from deleting valid walkways
		while (walkways.Count > 0 &&
				(player.transform.position - walkways.First.Value.transform.position)
					.z > 20f)
		{
			GameObject removable = walkways.First.Value;
			walkways.RemoveFirst();
			Destroy(removable);
		}
	}

	void GeneratePowerup()
	{
		GameObject newPowerup = (GameObject)Instantiate(powerupPrefab, currentPosition + powerupHeight, powerupPrefab.transform.rotation);
		newPowerup.name = "powerup " + currentPosition.z;
		newPowerup.transform.parent = pushObject.transform;
		powerups.AddLast(newPowerup);

		// the first power-up ahead of us will stop the loop from deleting valid power-ups
		while (powerups.Count > 0 &&
				(player.transform.position - powerups.First.Value.transform.position)
					.z > 0f)
		{
			GameObject removable = powerups.First.Value;
			powerups.RemoveFirst();
			Destroy(removable);
		}
	}
}
