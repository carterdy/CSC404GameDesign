using UnityEngine;
using System.Collections;

public class TalismanScript : MonoBehaviour {

    public float rotSpeed = 40f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(new Vector3(0f, rotSpeed * Time.deltaTime, 0f));
	}
}
