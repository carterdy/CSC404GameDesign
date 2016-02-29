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

}
