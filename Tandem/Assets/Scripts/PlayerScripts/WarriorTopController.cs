using UnityEngine;
using System;
public class WarriorTopController : PlayerTopScript {

    public GameObject shield;
    public GameObject WarriorTurningActive;
    public float shieldRotateSpeed = 5f;

    private GameObject activeShield;
    //Min and max offset the shield is allowed to move around the player, in degrees
    private float shieldRightLimit = 90f;
    private float shieldLeftLimit = 90f;

    void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        if (turn != 0)
        {
            WarriorTurningActive.SetActive(true);
            Turn(turn);
        } else
        {
            WarriorTurningActive.SetActive(false);
        }
        if (Input.GetAxis("Block") > 0)
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
            activeShield.transform.localPosition = new Vector3(0f, 1.0f, 1.0f);
            activeShield.transform.rotation = gameObject.transform.rotation;
        }
        float horizontal = Input.GetAxis("ShieldHorizontal");
        //Setting the limits for moving the shield around the player
        float activeShieldRotation = activeShield.transform.rotation.eulerAngles.y;
        float playerRotation = gameObject.transform.rotation.eulerAngles.y;
        float trueLeftLimit;
        float trueRightLimit;

        if ((playerRotation - shieldLeftLimit) < 0f)
        {
            trueLeftLimit = playerRotation - shieldLeftLimit + 360f;
        } else
        {
            trueLeftLimit = playerRotation - shieldLeftLimit;
        }

        if ((playerRotation + shieldRightLimit) > 360f)
        {
            trueRightLimit = playerRotation + shieldRightLimit - 360f;
        } else
        {

            trueRightLimit = playerRotation + shieldRightLimit;
        }

        //These large if statements make sure the shield is within the left and right limits of movement and also keep the shield from getting locked after hitting a limit
        if ((playerRotation >= 270f || playerRotation <= 90) && ((activeShieldRotation > trueLeftLimit || (horizontal > 0 && activeShieldRotation - 180 > 0)) || (activeShieldRotation < trueRightLimit || (horizontal < 0 && activeShieldRotation - 180 < 0))))
        {
            activeShield.transform.RotateAround(gameObject.transform.position, Vector3.up, shieldRotateSpeed * horizontal);
        }
        else if ((playerRotation < 270 && playerRotation > 90) && ((activeShieldRotation > trueLeftLimit || (horizontal > 0)) && (activeShieldRotation < trueRightLimit || (horizontal < 0))))
        {
            activeShield.transform.RotateAround(gameObject.transform.position, Vector3.up, shieldRotateSpeed * horizontal);
        }
    }
    
    /* Cease blocking by destroying the shield */
    void StopBlock()
    {
        Destroy(activeShield);
    }
}
