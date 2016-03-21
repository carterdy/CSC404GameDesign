using UnityEngine;
using System.Collections;

public class rayCastShooting : MonoBehaviour {

    public float distance = 10;
    public bool debug = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        if(debug) Debug.DrawRay(transform.position, forward * distance, Color.red);
        RaycastHit enemy = new RaycastHit();
        bool hit = Physics.Raycast(transform.position, forward, out enemy, distance);

        if (hit)
        {
            if (debug) Debug.Log(enemy.transform.gameObject.name);
        }
	
	}
}
