using UnityEngine;
using System.Collections;

public class shot_control : MonoBehaviour
{

    public Rigidbody rb;
    public int dmg = 1;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * 10f);
    }


}
