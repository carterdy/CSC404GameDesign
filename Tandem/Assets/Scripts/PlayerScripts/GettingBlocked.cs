using UnityEngine;
using System.Collections;

public class GettingBlocked : MonoBehaviour {

    public GameObject ShieldHitParticle;
    private GameObject particle;
	// Use this for initialization
	void Start () {
        particle = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.contacts[0].otherCollider.tag == "PlayerShield")
        {
            foreach (ContactPoint c in col.contacts)
            {
                Debug.Log(c.thisCollider.name);
            }
            Destroy(this.gameObject);

            //add particle animations

            //destroy previous particles
            if (particle != null) Destroy(particle);
            particle = Instantiate(ShieldHitParticle, col.contacts[0].point, Quaternion.LookRotation(transform.forward * -1)) as GameObject; 
        } else if (col.gameObject.tag == "Player")
        {
            foreach (ContactPoint c in col.contacts)
            {
                Debug.Log(c.otherCollider.tag);
            }
            col.gameObject.GetComponent<CentralPlayerController>().dealDamage();
            Destroy(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
