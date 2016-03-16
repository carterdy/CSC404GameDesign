﻿using UnityEngine;
using System.Collections;

public class runAnimations : MonoBehaviour {


    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        //set the location of the bow
        //bow.position = rightHand.position;
        bool warriorActive = gameObject.GetComponent<WarriorBottomController>().isActiveAndEnabled;
        bool warriorGrounded = gameObject.GetComponent<WarriorBottomController>().isGrounded();
        float warriorMove = Mathf.Abs(Input.GetAxisRaw("Vertical"));

        bool archerActive = gameObject.GetComponent<ArcherBottomController>().isActiveAndEnabled;
        bool archerGrounded = gameObject.GetComponent<ArcherBottomController>().isGrounded();
        float archerMove = Mathf.Abs(Input.GetAxisRaw("Vertical2"));

        if (warriorActive && warriorGrounded && warriorMove > 0.0f)
        {
            anim.SetInteger("run", 1);
        }
        else if (archerActive && archerGrounded && archerMove > 0.0f)
        {
            anim.SetInteger("run", 1);
        }
        else
        {
            anim.SetInteger("run", 0);
        }

    }
	
	// Update is called once per frame
	void Update () {

	
	}
}
