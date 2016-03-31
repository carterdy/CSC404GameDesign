using UnityEngine;
using System.Collections;

public class ArcherBottomController : PlayerBottomScript {

    public GameObject ArcherStraightActive;
    public float onIceSpeedModifier = 5;

    //Set to true when the archer is on the bottom and standing on ice
    private bool onIce = false;
    private Vector3 onIceMovement;

    public AudioClip jumpSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //Need to get movement axis values and hand them off to a movement function
        if (isGrounded())
        {
            vertical = Input.GetAxis("Vertical2");
        }
        if (Input.GetButton("Jump2"))
        {
            // jump sound
            source.PlayOneShot(jumpSound, 1F);
            AttemptJump();
        }

        if (vertical != 0)
        {
            ArcherStraightActive.SetActive(true);
            Move(vertical);
        }
        else
        {
            ArcherStraightActive.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Icy Ground")
        {
            onIce = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Icy Ground")
        {
            onIce = false;
        }
    }

    protected override void Move(float vertical)
    {
        //When the archer is on the bottom, movement is normal if not on ice.  If on ice, force is used to imitate sliding.
        if (!onIce)
        {
            movement = transform.forward * vertical * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);
        }
        else {
            onIceMovement = transform.forward * vertical * speed * onIceSpeedModifier;
            rb.AddForce(onIceMovement);
        }
    }

}
