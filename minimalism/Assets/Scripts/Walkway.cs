using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Walkway : MonoBehaviour
{
	public GameObject walkwayPrefab;
	public GameObject pushObject;
	public GameObject player;
	public float speed = 30f;
	public float walkwayGenerationDistance = 100f;
	Vector3 incrementPosition;
	Vector3 currentPosition;
	Quaternion currentRotation;
	LinkedList<GameObject> walkways;

	float lastHigh = 1000f;

	void Start()
	{
		walkways = new LinkedList<GameObject>();
		currentPosition = new Vector3(0.0f, -1.0f, 0.0f);
		currentRotation = walkwayPrefab.transform.rotation;
		incrementPosition = new Vector3(0.0f, 0.0f, 10.0f);
	}

	void Update()
	{
	}

	void FixedUpdate()
	{
		player.transform.Translate(0f, 0f, speed * Time.fixedDeltaTime);
		GenerateWalkways();
		if (player.transform.position.z > lastHigh) {
			GA.API.Design.NewEvent("player:walkway:high", player.transform.position.z);
			lastHigh = (((int)player.transform.position.z / 1000) * 1000) + 1000f;
		}
	}

	void GenerateWalkways()
	{
		// make new walkways
		while ((player.transform.position - currentPosition).magnitude < walkwayGenerationDistance) {
			GameObject newWalkway = (GameObject)Instantiate(walkwayPrefab, currentPosition, currentRotation);
			newWalkway.transform.parent = pushObject.transform;
			walkways.AddLast(newWalkway);
			currentPosition += incrementPosition;
		}
		// TODO: remove old walkways
		if (walkways.Count > 1) {
			// this should stop near us
			while ((player.transform.position - walkways.First.Value.transform.position).magnitude > 10f) {
				GameObject removable = walkways.First.Value;
				walkways.RemoveFirst();
				Destroy(removable);
			}
		}
	}
}
