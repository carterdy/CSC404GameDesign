using UnityEngine;
using System;
public class WarriorTopController : PlayerTopScript {

    public GameObject shield;
    public float shieldRotateSpeed = 5f;

    private GameObject activeShield;

    void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        Turn(turn);
        if (Input.GetButton("Block"))
        {
            Block();
        } else if (!Input.GetButton("Block")) {
            StopBlock();
        }
    }

    /* Activate the player's block by creating a shield object to physically block projectiles */
    void Block()
    {
        if (!activeShield)
        {
            //Create a shield and set the parent to the player.  Then set the position to the front of the player.
            activeShield = Instantiate(shield);
            activeShield.transform.SetParent(gameObject.transform);
            activeShield.transform.localPosition = new Vector3(0f, 0.5f, 0.5f);
        }
        double vertical = Input.GetAxis("ShieldVertical");
        double horizontal = Input.GetAxis("ShieldHorizontal");
        double shieldAngle = (Math.Sin(horizontal)) * (180.0 / Math.PI);
        activeShield.transform.Rotate(Vector3.up * shieldRotateSpeed * (float)horizontal);
        //activeShield.transform.RotateAround(gameObject.transform.position, Vector3.up, shieldRotateSpeed * (float)horizontal);
        Debug.Log(shieldAngle);
    }
    
    /* Cease blocking by destroying the shield */
    void StopBlock()
    {
        Destroy(activeShield);
    }
}
