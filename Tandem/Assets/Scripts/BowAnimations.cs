using UnityEngine;
using System.Collections;

public class BowAnimations : MonoBehaviour {

    private Animator anim;
    private float deadzone;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        deadzone = 0.25f;
	
	}
	
	// Update is called once per frame
	void Update () {
        float aimX = Input.GetAxis("AimX");
        float aimY = Input.GetAxis("AimY");
        bool archerOnTop = transform.root.gameObject.GetComponent<ArcherTopController>().isActiveAndEnabled;
        if (archerOnTop)
        {
            Vector2 stickInput = new Vector2(aimX, aimY);

            // If joystick is active
            if (stickInput.magnitude > deadzone)
            {
                anim.SetInteger("pull", 1);
            }
            else
            {
                anim.SetInteger("pull", 0);
            }

            if (Input.GetAxis("FireArrow") > 0) anim.SetInteger("pull", 0);

        }

    }
}
