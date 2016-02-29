using UnityEngine;
using System.Collections;

public class WarriorTopController : PlayerTopScript {

    public GameObject shield;

    private GameObject activeShield;

    void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        Turn(turn);
        if (Input.GetButton("Block") && !activeShield)
        {
            Block();
        } else if (!Input.GetButton("Block")) {
            StopBlock();
        }
    }

    /* Activate the player's block by creating a shield object to physically block projectiles */
    void Block()
    {
        Debug.Log("blocking");
        //Create a shield and set the parent to the player.  Then set the position to the front of the player.
        activeShield = Instantiate(shield);
        activeShield.transform.SetParent(gameObject.transform);
        activeShield.transform.localPosition = new Vector3(0f, 0.5f, 0.5f);
    }
    
    /* Cease blocking by destroying the shield */
    void StopBlock()
    {
        Destroy(activeShield);
    }
}
