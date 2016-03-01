using UnityEngine;
using System;
public class WarriorTopController : PlayerTopScript {

    public GameObject shield;
    public float shieldRotateSpeed = 5f;

    private GameObject activeShield;
    //Min and max degrees the shield is allowed to rotate around the player. In Euler angles so it's weird
    private float shieldRotateMin = 90f;
    private float shieldRotateMax = 270f;

    void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        Turn(turn);
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
            activeShield.transform.localPosition = new Vector3(0f, 0.5f, 0.5f);
            activeShield.transform.rotation = gameObject.transform.rotation;
        }
        float horizontal = Input.GetAxis("ShieldHorizontal");
        float activeShieldRotation = activeShield.transform.rotation.eulerAngles.y;
        float playerRotation = gameObject.transform.rotation.eulerAngles.y;
        if ((activeShieldRotation < shieldRotateMin || (horizontal > 0 && (activeShieldRotation - 180 > 0))) || (activeShieldRotation > shieldRotateMax || (horizontal < 0 && (activeShieldRotation - 180 < 0))))
        {
            activeShield.transform.RotateAround(gameObject.transform.position, Vector3.up, shieldRotateSpeed * horizontal);
            Debug.Log(horizontal);
            Debug.Log(activeShield.transform.rotation.eulerAngles.y);
        }
    }
    
    /* Cease blocking by destroying the shield */
    void StopBlock()
    {
        Destroy(activeShield);
    }
}
