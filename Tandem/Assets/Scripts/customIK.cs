using UnityEngine;
using System.Collections;

public class customIK : MonoBehaviour {

    Animator anim;
    //bones
    //public Transform upperArm;
    //public Transform lowerArm;

    //targets
    public Transform leftIK;
    public Transform rightIK;
    //public Transform pole;

    //public bool IsEnabled;
    public float Weight = 1;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnAnimatorIK()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, Weight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, Weight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIK.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightIK.position);
    }
}
