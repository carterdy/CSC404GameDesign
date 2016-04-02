using UnityEngine;
using System.Collections;

public class CollectableController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(45, 90, 20) * Time.deltaTime);
	
	}
}
