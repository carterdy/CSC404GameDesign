using UnityEngine;
using System.Collections;

public class BoulderController : MonoBehaviour {

    public float speed = 5f;
    public float destroyTime = 10f;
    public GameObject BoulderExplosion;

    private Vector3 direction;

	// Use this for initialization
	void Start () {
        direction = transform.forward;
        Invoke("destroyBoulder", destroyTime);
	}

    void destroyBoulder ()
    {
        Destroy(gameObject);
        Instantiate(BoulderExplosion, transform.position, BoulderExplosion.transform.rotation);

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * speed);
    }

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CentralPlayerController>().dealDamage();
            destroyBoulder();
        } else if (other.gameObject.tag == "Water")
        {
            destroyBoulder();
        }
    }
}
