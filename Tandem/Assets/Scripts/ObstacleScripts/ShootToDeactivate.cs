using UnityEngine;
using System.Collections;

/* The purpose of this script is to attach it to some object that is shot by the players (like a switch).
    When the object is shot, the paired object objectToDeactivate will be deactivated.  */

public class ShootToDeactivate : MonoBehaviour {

    public GameObject objectToDeactivate;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnCollisionEnter (Collision other)
    {
        Debug.Log("We got a collision");
        if (other.gameObject.tag == "Arrow")
        {
            Debug.Log("do we get here?");
            objectToDeactivate.SetActive(false);
        }
    }
}
