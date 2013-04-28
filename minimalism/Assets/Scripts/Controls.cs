using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
	GameObject player;
	float speed = 20f;
	float jumpForce = 400f;
	Vector3 jumpVector = Vector3.zero;
	Vector3 playerStartPosition;
	Quaternion playerStartRotation;

	void Awake()
	{
		player = GameObject.Find("Player");
		playerStartPosition = player.transform.position;
		playerStartRotation = player.transform.rotation;
		Cleanup();
	}

	// called from GameState send message
	public void Cleanup()
	{
		player.transform.position = playerStartPosition;
		player.transform.rotation = playerStartRotation;
		player.rigidbody.velocity = Vector3.forward * speed;
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1")) {
			player.audio.Play();
			jumpVector = Vector3.up * jumpForce;
		}
	}

	void FixedUpdate()
	{
		// constant speed, sometimes something hits us and slows us down
		Vector3 newV = player.rigidbody.velocity;
		newV.z = speed;
		player.rigidbody.velocity = newV;

		if (Vector3.zero != jumpVector) {
			player.rigidbody.AddForce(jumpVector);
			jumpVector = Vector3.zero;
		}
	}
}
