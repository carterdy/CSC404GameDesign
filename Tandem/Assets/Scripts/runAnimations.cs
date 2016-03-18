using UnityEngine;
using System.Collections;

public class runAnimations : MonoBehaviour {


    private Animator anim;
    public GameObject weapon;
    public GameObject IK;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        weapon.SetActive(false);
        IK.GetComponent<customIK>().enabled = false;
	}

    void FixedUpdate()
    {
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

        //activate archer bow and arm movements
        if (warriorActive)
        {
            IK.GetComponent<customIK>().enabled = true;
        }
        else
        {
            IK.GetComponent<customIK>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

	
	}
}
