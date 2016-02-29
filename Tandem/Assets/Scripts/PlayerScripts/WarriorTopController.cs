using UnityEngine;
using System.Collections;

public class WarriorTopController : PlayerTopScript {

    void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        Turn(turn);
    }
}
