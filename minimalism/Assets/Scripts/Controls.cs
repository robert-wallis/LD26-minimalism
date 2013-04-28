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
	}

	void OnEnable()
	{
		ResetGame();
	}

	void ResetGame()
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
		if (Vector3.zero != jumpVector) {
			player.rigidbody.AddForce(jumpVector);
			jumpVector = Vector3.zero;
		}
	}
}
