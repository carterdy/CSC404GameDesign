using UnityEngine;
using System.Collections;

public class runAnimations : MonoBehaviour {

    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        bool warriorActive = gameObject.GetComponent<WarriorBottomController>().isActiveAndEnabled;
        //bool warriorJumping = Input.GetButton("Jump");
        bool warriorGrounded = gameObject.GetComponent<WarriorBottomController>().isGrounded();
        float warriorMove = Mathf.Abs(Input.GetAxisRaw("Vertical"));

        bool archerActive = gameObject.GetComponent<ArcherBottomController>().isActiveAndEnabled;
        //bool archerJumping = Input.GetButton("Jump2");
        bool archerGrounded = gameObject.GetComponent<ArcherBottomController>().isGrounded();
        float archerMove = Mathf.Abs(Input.GetAxisRaw("Vertical2"));

        Debug.Log(archerMove);
        string debug = "Active:" + archerActive + "\n" + "Grounded:" + archerGrounded + "\n" + "Moving:" + archerMove + "\n";
        Debug.Log(debug);

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
