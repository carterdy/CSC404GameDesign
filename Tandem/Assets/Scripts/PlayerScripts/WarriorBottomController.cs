using UnityEngine;
using System.Collections;

/* Using Unity tutorial "Training Day" for a reference to make movement code */

public class WarriorBottomController : PlayerBottomScript {

    public GameObject WarriorStraightActive;

    public AudioClip jumpSound;
    private AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate ()
    {
        //Need to get movement axis values and hand them off to a movement function
        if (isGrounded())
        {
            vertical = Input.GetAxisRaw("Vertical");
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

    void OnCollisionStay(Collision other)
    {
        //If the player is touching molten ground, take damage over time
        if (other.gameObject.tag == "Molten Ground")
        {
            gameObject.GetComponent<CentralPlayerController>().standingInFire();
        }
    }

}
