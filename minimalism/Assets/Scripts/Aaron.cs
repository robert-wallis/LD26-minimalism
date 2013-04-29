using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// the brother that helps you out
public class Aaron : MonoBehaviour
{
	public GameObject walkwayPrefab;
	public GameObject powerupPrefab;
	public GameObject pushObject;
	public GameObject player;
	float walkwayGenerationDistance = 70f;
	Vector3 spawnPosition;
	static Vector3 spawnPositionDefault = new Vector3(0.0f, -1.0f, 0.0f);
	Quaternion spawnRotation;
	Vector3 incrementPosition;
	Vector3 powerupHeight = new Vector3(0f, 5f, 0f);
	LinkedList<GameObject> walkways;
	LinkedList<GameObject> powerups;

	[HideInInspector]
	public uint highScoreDistance = 1000;
	[HideInInspector]
	public uint highScoreWalkways = 100;
	float generatePowerup;
	public uint walkwaysLeft;

	bool playing = false;

	void Start()
	{
		walkways = new LinkedList<GameObject>();
		powerups = new LinkedList<GameObject>();
		incrementPosition = new Vector3(0.0f, 0.0f, 10.0f);
		Cleanup();
	}

	void GameStarting()
	{
		playing = true;
	}

	void GameEnding()
	{
		playing = false;
	}

	void Cleanup()
	{
		spawnPosition = spawnPositionDefault;
		spawnRotation = walkwayPrefab.transform.rotation;
		walkwaysLeft = 300;
		generatePowerup = 100f;
		while (walkways.Count > 0) {
			GameObject removable = walkways.First.Value;
			Destroy(removable);
			walkways.RemoveFirst();
		}
		while (powerups.Count > 0) {
			GameObject removable = powerups.First.Value;
			Destroy(removable);
			powerups.RemoveFirst();
		}
	}

	void FixedUpdate()
	{
		if (playing) {
			GenerateWalkways();
			if (player.transform.position.z > highScoreDistance) {
				GA.API.Design.NewEvent("player:distance:high", player.transform.position.z);
				highScoreDistance = (((uint)player.transform.position.z / 1000) * 1000) + 1000;
			}
			if (walkwaysLeft > highScoreWalkways) {
				GA.API.Design.NewEvent("player:walkways:high", walkwaysLeft);
				highScoreWalkways = ((walkwaysLeft / 100) * 100) + 100;
			}
			if (player.transform.position.z > generatePowerup) {
				generatePowerup = (((int)player.transform.position.z / 100) * 100) + 100f;
				GeneratePowerup();
			}
		}
	}

	void GenerateWalkways()
	{
		// make new walkways
		while (walkwaysLeft > 0 
			&& (spawnPosition - player.transform.position).z < walkwayGenerationDistance) {
			GameObject newWalkway = (GameObject)Instantiate(walkwayPrefab, spawnPosition, spawnRotation);
			newWalkway.name = "aaron " + spawnPosition.z;
			newWalkway.transform.parent = pushObject.transform;
			newWalkway.transform.Translate(Vector3.left * 30f);
			newWalkway.transform.Rotate(Vector3.forward + Vector3.up, 180f);
			Go.to(
				newWalkway.transform,
				1.5f,
				new GoTweenConfig()
					.position(spawnPosition)
					.rotation(spawnRotation)
					.setEaseType(GoEaseType.SineIn)
			);
			walkways.AddLast(newWalkway);
			spawnPosition += incrementPosition;
			walkwaysLeft--;
		}
		// the first aaron ahead of us will stop the loop from deleting valid walkways
		while (walkways.Count > 0 
			&& (player.transform.position - walkways.First.Value.transform.position) .z > 20f) {
			GameObject removable = walkways.First.Value;
			walkways.RemoveFirst();
			Destroy(removable);
		}
	}

	void GeneratePowerup()
	{
		GameObject newPowerup = (GameObject)Instantiate(powerupPrefab, spawnPosition + powerupHeight, powerupPrefab.transform.rotation);
		newPowerup.name = "powerup " + spawnPosition.z;
		newPowerup.transform.parent = pushObject.transform;
		powerups.AddLast(newPowerup);

		// the first power-up ahead of us will stop the loop from deleting valid power-ups
		while (powerups.Count > 0 &&
				(player.transform.position - powerups.First.Value.transform.position)
					.z > 0f) {
			GameObject removable = powerups.First.Value;
			powerups.RemoveFirst();
			Destroy(removable);
		}
	}

}
