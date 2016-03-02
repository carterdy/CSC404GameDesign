using UnityEngine;
using System.Collections;

public class ArcherBottomController : PlayerBottomScript {

    //Set to true when the archer is on the bottom and standing on ice
    private bool onIce = false;
    private Vector3 onIceMovement;

    void FixedUpdate()
    {
        //Need to get movement axis values and hand them off to a movement function
        float vertical = Input.GetAxisRaw("Vertical2");
        if (Input.GetButton("Jump2"))
        {
            AttemptJump();
        }

        Move(vertical);
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
            onIceMovement = transform.forward * vertical * speed;
            rb.AddForce(movement);
        }
    }

}
