using UnityEngine;
using System.Collections;

public class ArcherBottomController : PlayerBottomScript {

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

}
