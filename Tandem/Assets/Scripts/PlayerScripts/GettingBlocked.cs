using UnityEngine;
using System.Collections;

public class GettingBlocked : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
        } else if (col.gameObject.tag == "Player")
        {
            foreach (ContactPoint c in col.contacts)
            {
                Debug.Log(c.otherCollider.tag);
            }
            col.gameObject.GetComponent<CentralPlayerController>().takeDamage();
            Destroy(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
