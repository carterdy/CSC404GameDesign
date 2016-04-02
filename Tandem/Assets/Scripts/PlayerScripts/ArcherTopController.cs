using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcherTopController : PlayerTopScript
{

	public GameObject ArcherTurningActive;
	public Transform hand;

	private GameObject dummy;
	GameObject arrowPrefab;
	GameObject player;
	GameObject arrow;
	GameObject shotArrow;

	private float stickDir;
	private Vector2 stickInput;
	private float deadzone = 0.25f;

	// Boolean to see if Fire is being 'held'
	private bool firePressed;

	private float aimRange = 90f;
	private float leftRange;
	private float rightRange;

	public AudioClip shootSound;
	private AudioSource source;


	void Start ()
	{
		arrowPrefab = Resources.Load ("Arrow") as GameObject;
		player = GameObject.Find ("CompletePlayer");
		// null checker is broken
		// dummy used instead of null checker
		dummy = new GameObject ();
		arrow = dummy;
		firePressed = false;

		//audio setup
		source = GetComponent<AudioSource> ();
	}

	void FixedUpdate ()
	{
		/* Rotate the players based off the given input */
		float turn = Input.GetAxisRaw ("Horizontal2");
		if (turn != 0) {
			ArcherTurningActive.SetActive (true);
			Turn (turn);
		} else {
			ArcherTurningActive.SetActive (false);
		}

		// Aim Axis
		float aimX = Input.GetAxis ("AimX");
		float aimY = Input.GetAxis ("AimY");
		float fire = Input.GetAxis ("FireArrow");

		// Math to convert Axis to Angle Direction
		stickDir = Mathf.Atan2 (aimX, aimY) * Mathf.Rad2Deg;

		// Used for Deadzone Check
		stickInput = new Vector2 (aimX, aimY);

		// If joystick is active
		if (stickInput.magnitude > deadzone) {
			Aim (aimX, aimY);
			if (fire == 1) {
				// Prevent arrows from firing like a machine gun
				// Check if fire was pressed or held
				if (!firePressed) {
					firePressed = true;
					Shoot (arrow);
					arrow = dummy;
				} 
			}
			// When player releases button (Not being Held)
			else if (fire == 0 || fire == -1) {
				// Reset firePressed Variable
				firePressed = false;
			}
		} 
		// When Aim Axes are not active reset everything
		else { 
			if (arrow != dummy) {
				Destroy (arrow.gameObject);
				arrow = dummy;
			}
			// Reset firePressed Variable
			firePressed = false;
		}
	}

	/*
	 * Arrow Related Code Start
	 */

	void CreateArrow ()
	{
		arrow = Instantiate (arrowPrefab) as GameObject;
	}

	void Aim (float aimX, float aimY)
	{
		// if arrow does not exist
		// And fire is note held down
		if (arrow == dummy && !firePressed) {
			// Init Arrow
			CreateArrow ();
		}
		// Arrow exist, now aim the arrow
		else {
			// Rotate Arrow in the direction of the Joystick
			LimitAimCone ();
		}
	}

	void Shoot (GameObject arrow)
	{
		// Add Arrow Velocity, Driection of Arrow Head
		Rigidbody rb = arrow.GetComponent<Rigidbody> ();
		rb.velocity = arrow.transform.up * 10;

		//shoot sound
		source.PlayOneShot (shootSound, 1F);

		Destroy (arrow.gameObject, 3);
	}
		
	void LimitAimCone ()
	{
		float playerRotation = player.transform.eulerAngles.y;
		float arrowRot = playerRotation + stickDir;

		arrow.transform.rotation = Quaternion.Euler (new Vector3 (90, arrowRot, 0));

		if (arrowRot > 360) {
			arrowRot = arrowRot - 360f;
		}
		if (arrowRot < 0) {
			arrowRot = arrowRot + 360f;
		}

		leftRange = playerRotation - aimRange;
		rightRange = playerRotation + aimRange;


		// Find Min Ranges
		if (leftRange < 0f) {
			leftRange = leftRange + 360f;
		}

		// Find Max Ranges
		if (rightRange > 360f) {
			rightRange = rightRange - 360f;
		}

		// EDGE CASE
		// if leftRange is larger than rightRange
		// the cone will be from min to 0 to max
		// this case need to be cover :(
		if (leftRange > rightRange) {
			if (arrowRot < leftRange && arrowRot > leftRange - aimRange) {
				UpdatePosition (leftRange);
				LimitStickDir (leftRange);
			} else if (arrowRot > rightRange && arrowRot < rightRange + aimRange) {
				UpdatePosition (rightRange);
				LimitStickDir (rightRange);
			} else {
				UpdatePosition (arrowRot);
			}
		} 
		// NORMAL CASE HERE
		else {
			// Update Arrow with Range Restrictions
			// Maintain Min Value if Rotation is under Min
			if (arrowRot < leftRange) {
				UpdatePosition (leftRange);
				LimitStickDir (leftRange);
			} 
			// Maintain Max Value if Rotation is under Max
			else if (arrowRot > rightRange) {
				UpdatePosition (rightRange);
				LimitStickDir (rightRange);
			} 
			// All good, No limit
			else {
				UpdatePosition (arrowRot);
			}
		}
	}

	void LimitStickDir (float angle)
	{
		// Controller Direction angles are 180 to -180
		// Convert rotation angle to controller Angle
		if (angle > 180) {
			angle = angle - 360;
		}
		arrow.transform.rotation = Quaternion.Euler (new Vector3 (90, angle, 0));
	}

	void UpdatePosition (float angle)
	{
		// Arrow Should Rotate Around Player
		Vector3 playerPos = player.transform.position;
		float rads = angle * Mathf.Deg2Rad;

		// Math here...
		// Rotation around Origin with Vector [0, 0, 0.5]
		float newX = Mathf.Sin (rads) / 2;
		float newZ = Mathf.Cos (rads) / 2;
		// Make new vector for the arrow position
		Vector3 newPos = new Vector3 (newX, 0, newZ);

		// Ignores layers passed as ints
		// Player = 9
		// Weapon = 10
		Physics.IgnoreLayerCollision (9, 10, true);

		// Move the arrow into place
		arrow.transform.position = hand.position - newPos;
	}
	/*
	 * Arrow Related Code End
	 */
	void OnDisable ()
	{
		if (arrow)
			Destroy (arrow);
	}
}
