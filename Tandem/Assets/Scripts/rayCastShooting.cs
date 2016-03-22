using UnityEngine;
using System.Collections;

public class rayCastShooting : MonoBehaviour {

    public float distance = 10.0f;
    public bool debug = false;

    private GameObject cursor;
    private Vector3 startPos;

	// Use this for initialization
	void Start () {
        cursor = GameObject.Find("Cursor");
        startPos = new Vector3(distance/10, 0, 0);
        cursor.transform.localPosition = startPos;
	
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
            cursor.transform.localPosition = new Vector3((enemy.distance/10)-0.01f, 0, 0);
        }
        else
        {
            cursor.transform.localPosition = new Vector3(distance / 10, 0, 0);
        }
	
	}
}
