using UnityEngine;
using System.Collections;

public class EnemyAIController : MonoBehaviour
{

	NavMeshAgent agent;
	public GameObject player1;
	public float playerDist;
	public float distThreshold;
	public float radius;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = 1;
		radius = 2;
		distThreshold = 3;
		player1 = GameObject.Find ("CompletePlayer");
	}
	
	// Update is called once per frame
	void Update ()
	{
		playerDist = Vector3.Distance (agent.transform.position, player1.transform.position);

		// If the player is within range, Follow player
		if (playerDist < distThreshold) {
			agent.Resume ();
			agent.SetDestination (player1.transform.position);
		} else {
			agent.Stop ();
		}

		// When the player is too close
		// Maintain Distance away from player
		if (playerDist < radius) {
			agent.Resume ();
			RaycastHit hit;

			// Find the direction vector from the player
			Vector3 direction = player1.transform.position - agent.transform.position;

			// Rotate direction vector to point to the floor
			direction = Quaternion.AngleAxis (80, Vector3.left) * direction;
			// Roate direction vector to 'reverse' the diretion vector
			direction = Quaternion.AngleAxis (180, Vector3.up) * direction;
			Debug.DrawRay (agent.transform.position, direction);

			// Cast Ray to determine direction the enemy should be moving
			if (Physics.Raycast (agent.transform.position, direction, out hit, radius)) {
				agent.SetDestination (hit.point);
				Debug.Log (hit.point);
			}
		} 
		// Stop moving when enemy is around the radius
		// Following if statement prevents the case when speed over shoots the radius
		// When over shooting, enemy walks outside and inside radius continuously
		else if (radius - 0.5f < playerDist && playerDist < radius + 0.5f) {
			agent.Stop();
		}
	}
}
