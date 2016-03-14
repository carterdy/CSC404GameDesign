using UnityEngine;
using System.Collections;

public class PlayerTopScript : MonoBehaviour {

    public float turnSpeed = 5f;

    private Rigidbody rb;

    // Use this for initialization
    protected void Awake () {
        rb = GetComponent<Rigidbody>();
    }

    /* Rotate the players based off the given input */
    protected void Turn(float turn)
    {
        if (gameObject.GetComponent<WarriorBottomController>().isGrounded())
        {
            rb.transform.Rotate(Vector3.up * turn * turnSpeed);
        }
    }
}
