using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
	public GameObject player;
	float speed = 30f;
	float jumpForce = 400f;
	Vector3 jumpVector = Vector3.zero;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Fire1")) {
			jumpVector = Vector3.up * jumpForce;
		}
	}

	void FixedUpdate()
	{
		player.transform.Translate(0f, 0f, speed * Time.fixedDeltaTime);
		if (Vector3.zero != jumpVector) {
			player.rigidbody.AddForce(jumpVector);
			jumpVector = Vector3.zero;
		}
	}
}
