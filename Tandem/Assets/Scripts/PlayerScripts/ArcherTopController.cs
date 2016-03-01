using UnityEngine;
using System.Collections;

public class ArcherTopController : PlayerTopScript {

	GameObject arrowPrefab;
	GameObject player;

	void Start() {
		arrowPrefab = Resources.Load ("Arrow") as GameObject;
		player = GameObject.Find("CompletePlayer");
	}

	/* Rotate the players based off the given input */
	void Shoot()
	{
		GameObject arrow = Instantiate (arrowPrefab) as GameObject;
		// Set Arrow shooting position
		arrow.transform.position = transform.position + 
			// In front of the player
			transform.forward * transform.GetComponent<CapsuleCollider>().radius +
			// Height level of top player
			transform.up * transform.GetComponent<CapsuleCollider>().height / 2;

		// Rotate Arrow in the direction of the player
		arrow.transform.eulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);

		// Add Arrow Velocity
		Rigidbody rb = arrow.GetComponent<Rigidbody>();
		rb.velocity = player.transform.forward * 10;

		// Destroy the Arrow After 3 Seconds
		Destroy(arrow.gameObject, 3);
	}

	void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal2");
        Turn(turn);

		if (Input.GetButton("Fire2")) {
			Shoot ();
		}

    }
}
