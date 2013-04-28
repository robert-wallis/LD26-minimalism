using UnityEngine;
using System.Collections;

public class BlockingObject : MonoBehaviour
{
	public Vector3 startPosition;
	public Quaternion startRotation;
	public Vector3 destinationPosition;
	public Quaternion destinationRotation;
	public float speed = 2f;

	// setup the object to move into this Z location
	public void Init(float destZ)
	{
		startPosition.z = destZ;
		destinationPosition.z = destZ;
		transform.position = startPosition;
		transform.rotation = startRotation;
		Go.to(this.transform, speed, new GoTweenConfig()
			.position(destinationPosition)
			.rotation(destinationRotation)
			.setEaseType(GoEaseType.SineOut));
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.name == "Player") {
			GameState.instance.GameOver(this.gameObject);
		}
	}
}
