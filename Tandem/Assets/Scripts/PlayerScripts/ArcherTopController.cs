using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherTopController : PlayerTopScript {

	GameObject arrowPrefab;
	GameObject player;
	GameObject arrow;

	private Vector2 stickInput;
	private float deadzone = 0.2f;

	void Start() {
		arrowPrefab = Resources.Load ("Arrow") as GameObject;
		player = GameObject.Find("CompletePlayer");
		arrow = null;
	}

	void FixedUpdate()
	{
		/* Rotate the players based off the given input */
		float turn = Input.GetAxisRaw("Horizontal2");
		Turn(turn);

		/* Shoot Arrow on Fire */
		float aimX = Input.GetAxis("AimX");
		float aimY = Input.GetAxis ("AimY");
		stickInput = new Vector2(aimX, aimY);

		// Debug.Log ("aimX: "+ aimX);
		// Debug.Log ("aimY: "+ aimY);

		// If joystick is active
		if (stickInput.magnitude > deadzone) {
			Aim (aimX, aimY);
		} 
		// Fire on X-Axis Release
		else {
			// Store the arrow in the arrow list
			if (arrow != null) {
				// Calls Destroy on the arrow
				Shoot (arrow);
				// Reset arrow, for next arrow to be shot
				arrow = null;
			}
		}
	}

	/*
	 * Arrow Related Code Start
	 */

	void CreateArrow() {
		arrow = Instantiate (arrowPrefab) as GameObject;
		// Set Arrow shooting position
		arrow.transform.position = player.transform.position + 
			// In front of the player
			player.transform.forward * transform.GetComponent<CapsuleCollider>().radius +
			// Height level of top player
			player.transform.up * transform.GetComponent<CapsuleCollider>().height / 2;
	}

	void Shoot(GameObject arrow)
	{
		// Add Arrow Velocity, Driection of Arrow Head
		Rigidbody rb = arrow.GetComponent<Rigidbody>();
		rb.velocity = arrow.transform.up * 10;

		// Destroy the Arrow After 3 Seconds
		Destroy(arrow.gameObject, 3);
	}

	void Aim (float aimX, float aimY) {
		// if arrow does not exist
		if (arrow == null) {
			// Init Arrow
			CreateArrow ();
		}
		// Arrow exist, now aim the arrow
		else {
			// Rotate Arrow in the direction of the Joystick
			float angle = Mathf.Atan2 (aimX, aimY) * Mathf.Rad2Deg;
			arrow.transform.rotation = Quaternion.Euler (new Vector3 (90, angle, 0));
		}
	}

	/*
	 * Arrow Related Code End
	 */
}
