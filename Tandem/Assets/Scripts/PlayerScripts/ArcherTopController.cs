using UnityEngine;
using System.Collections;

public class ArcherTopController : PlayerTopScript {

	void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal2");
        Turn(turn);
    }
}
