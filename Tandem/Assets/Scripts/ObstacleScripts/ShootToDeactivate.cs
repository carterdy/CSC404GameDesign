using UnityEngine;
using System.Collections;

/* The purpose of this script is to attach it to some object that is shot by the players (like a switch).
    When the object is shot, the paired object objectToDeactivate will be deactivated.  */

public class ShootToDeactivate : MonoBehaviour {

    public GameObject[] objectsToDeactivate;
    public GameObject ArrowHitParticle;
    private GameObject particle;

	// Use this for initialization
	void Start () {
        particle = null;
	
	}
	
	void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            //get the point of contact between arrow and target
            ContactPoint[] contact = other.contacts;
            //readjust the angel of the particle

            Quaternion angle = transform.rotation;
            angle.eulerAngles += new Vector3(0, 90, 0);

            //remove the previous particle
            if (particle != null) Destroy(particle);
            particle = Instantiate(ArrowHitParticle, contact[0].point, angle) as GameObject;
            foreach (GameObject obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        }
    }
}
