using UnityEngine;
using System.Collections;

public class ArcherBottomController : MonoBehaviour {

    public float speed = 4f;
    public float jumpSpeed = 5f;

    private Rigidbody rb;
    private Vector3 movement;
    private float rayLength = 100f;
    private int jumpable;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpable = LayerMask.GetMask("Jumpable");
    }

    void FixedUpdate()
    {
        //Need to get movement axis values and hand them off to a movement function
        float horizontal = Input.GetAxisRaw("Horizontal2");
        float vertical = Input.GetAxisRaw("Vertical2");
        if (Input.GetButton("Jump2"))
        {
            AttemptJump();
        }

        Move(horizontal, vertical);
    }

    /* Attempts to make the character jump.  Successful if the character is standing on a jumpable object */
    void AttemptJump()
    {
        //Going to cast a ray down from the character, and we know they're standing on something jumpable if the
        //difference between the ray hit and origin is 0

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, -Vector3.up, out hit, rayLength, jumpable))
        {
            Debug.Log("Raycast hit");
            Debug.Log(hit.distance);
            if (hit.distance - gameObject.transform.localScale.y <= 0.0001)
            {
                Debug.Log(hit.distance);
                rb.velocity = new Vector3(0f, jumpSpeed, 0f);
            }
        }
    }

    /* Move the player based off the given horizontal and vertical inputs */
    void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
