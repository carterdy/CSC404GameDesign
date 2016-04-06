using UnityEngine;
using System.Collections;

public class ShootToActivate : MonoBehaviour {

    public GameObject[] objectsToActivate;
    public GameObject ArrowHitParticle;
    private GameObject particle;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        particle = null;
    }

    void OnCollisionEnter(Collision other)
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
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
