using UnityEngine;
using System.Collections;

/* Using Unity tutorial "Training Day" for a reference to make movement code */

public class WarriorBottomController : PlayerBottomScript {

    public GameObject WarriorStraightActive;
    public float onMoltenSpeed = 1f;

    public AudioClip jumpSound;
    private AudioSource source;
    private bool onMolten = false;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate ()
    {
        //Need to get movement axis values and hand them off to a movement function
        if (isGrounded())
        {
            vertical = Input.GetAxis("Vertical");
        }
        if (Input.GetButton("Jump"))
        {
            //jump sound
            source.PlayOneShot(jumpSound, 1F);
            AttemptJump();
        }

        if (vertical != 0)
        {
            WarriorStraightActive.SetActive(true);
            Move(vertical);
        }
        else
        {
            WarriorStraightActive.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Molten Ground")
        {
            onMolten = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Molten Ground")
        {
            onMolten = false;
        }
    }

    protected override void Move(float vertical)
    {
        //When the warrior is on the bottom, suffer reduced movement speed if standin on molten ground
        if (onMolten)
        {
            movement = transform.forward * vertical * onMoltenSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);
        }
        else {
            base.Move(vertical);
        }
    }

}
