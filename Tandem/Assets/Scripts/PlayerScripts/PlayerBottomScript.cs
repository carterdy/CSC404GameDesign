using UnityEngine;
using System.Collections;

public abstract class PlayerBottomScript : MonoBehaviour {

    public float speed = 4f;
    public float jumpSpeed = 5f;

    protected Rigidbody rb;
    protected Vector3 movement;
    private float rayLength = 100f;
    private int jumpable;

    /* Uses raycasting to determine if the player is grounded. Return true if the player is grounded, otherwise return false */
    public bool isGrounded()
    {
        //Going to cast a ray down from the character, and we know they're standing on something jumpable if the
        //difference between the ray hit and origin is 0

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, -Vector3.up, out hit, rayLength, jumpable))
        {
            if (hit.distance - (gameObject.transform.localScale.y / 2) <= 0.0001)
            {
                return true;
            }
        }
        return false;
    }

    // Use this for initialization
    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpable = LayerMask.GetMask("Jumpable");
    }

    /* Attempts to make the character jump.  Successful if the character is standing on a jumpable object */
    protected void AttemptJump()
    {
        //Going to cast a ray down from the character, and we know they're standing on something jumpable if the
        //difference between the ray hit and origin is 0

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, -Vector3.up, out hit, rayLength, jumpable))
        {
            if (isGrounded())
            {
                rb.velocity = new Vector3(0f, jumpSpeed, 0f);
            }
        }
    }

    /* Move the player based off the given horizontal and vertical inputs */
    protected virtual void Move(float vertical)
    {
        movement = transform.forward * vertical * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

    }

}
