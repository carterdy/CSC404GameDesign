using UnityEngine;
using System.Collections;

/* Using Unity tutorial "Training Day" for a reference to make movement code */

public class WarriorBottomController : PlayerBottomScript {


     void FixedUpdate ()
    {
        //Need to get movement axis values and hand them off to a movement function
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Jump"))
        {
            AttemptJump();
        }

       Move(vertical);
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
