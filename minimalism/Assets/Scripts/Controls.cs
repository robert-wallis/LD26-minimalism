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

	bool playing = false;

	void Awake()
	{
		player = GameObject.Find("Player");
		playerStartPosition = player.transform.position;
		playerStartRotation = player.transform.rotation;
		player.rigidbody.Sleep();
		player.SetActive(false);
		Cleanup();
	}

	void GameStarting()
	{
		Cleanup();
		playing = true;
		player.SetActive(true);
		player.rigidbody.WakeUp();
	}

	void GameEnding()
	{
		playing = false;
		player.SetActive(false);
		player.rigidbody.Sleep();
	}

	// called from GameState send message
	void Cleanup()
	{
		player.transform.position = playerStartPosition;
		player.transform.rotation = playerStartRotation;
		player.rigidbody.velocity = Vector3.forward * speed;
	}

	void Update()
	{
		if (playing) {
			if (Input.GetButtonDown("Fire1")) {
				float vy = player.rigidbody.velocity.y;
				float avy = Mathf.Abs(vy); 
				if (avy < 0.01f || vy < -jumpForce) {
					player.audio.Play();
					jumpVector = Vector3.up * jumpForce;
				}
			}
			if (player.transform.position.y < -10) {
				GameState.instance.GameOver(this.gameObject);
			}
			if (Input.GetKeyDown(KeyCode.End)) {
				GameState.instance.GameOver(this.gameObject);
			}
			if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Home)) {
				GameState.instance.GameWin();
			}
		} else {
			if (Input.anyKeyDown) {
				GameState.instance.StartGame();
			}
		}
	}

	void FixedUpdate()
	{
		if (playing) {
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
}
