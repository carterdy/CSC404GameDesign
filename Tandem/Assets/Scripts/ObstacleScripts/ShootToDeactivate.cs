using UnityEngine;
using System.Collections;

/* The purpose of this script is to attach it to some object that is shot by the players (like a switch).
    When the object is shot, the paired object objectToDeactivate will be deactivated.  */

public class ShootToDeactivate : MonoBehaviour {

    public GameObject[] objectsToDeactivate;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            foreach (GameObject obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        }
    }
}
