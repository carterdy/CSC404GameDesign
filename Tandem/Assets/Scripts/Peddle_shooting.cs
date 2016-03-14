using UnityEngine;
using System.Collections;

public class Peddle_shooting : MonoBehaviour {

    // Use this for initialization
    //void Start () {
    //}

    // Update is called once per frame
    //void Update () {
    //}
    public GameObject obstacle;
    public GameObject pebble;
    public float speed = 20;
    public float ShootingSpeed;
    private float tempSpeed;

    void Start () {
        tempSpeed = ShootingSpeed;
    }

    void Update()
    {
        if (tempSpeed < 0)
        {

            GameObject projectile = Instantiate(pebble) as GameObject;
            projectile.transform.position = transform.position;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Vector3.forward * speed;
            tempSpeed = ShootingSpeed;
        }
        else
        {
            tempSpeed -= 1;
        }


    }
}
