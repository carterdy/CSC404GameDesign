using UnityEngine;
using System.Collections;

public class customIK : MonoBehaviour {


    //bones for bow
    public Transform bow;
    
    //bones for archer
    public Transform rightArm, rightHand;

    //bones for archer that needs to be moved
    public Transform upperArm, lowerArm, hand, neck, chest;

    //public Transform target;
    public Transform pole;
    public Transform target; //hand target


    //public bool isEnabled;
    public float transition = 1.0f;
    public float elbowAngle = 0;
    public float weight = 1;

    //internal variables 
    private Quaternion upperStart, lowerStart, handStart;
    private Vector3 targetStart, poleStart;

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        bow.parent.Rotate(0, 18.81f, 0);
        
        upperStart = upperArm.rotation;
        lowerStart = lowerArm.rotation;
        handStart = hand.rotation;
        targetStart = target.position - upperArm.position;
        poleStart = pole.position - upperArm.position;

	}

    void LateUpdate()
    {
        bow.position = rightHand.position;
        rightArm.localEulerAngles = new Vector3(17.9996f, 24.7699f, -32.3385f);
        

        float upperLength, lowerLength, armLength;
        upperLength = Vector3.Distance(upperArm.position, lowerArm.position);
        lowerLength = Vector3.Distance(lowerArm.position, hand.position);
        Debug.Log("lowerLength: " + Vector3.Distance(lowerArm.position, hand.position));
        armLength = upperLength + lowerLength;
        float hypotenuse = upperLength;
        
        //set start rotations
        upperStart = transform.rotation;
        lowerStart = lowerArm.rotation;

        //upperArm angle
        float targetDistance = Vector3.Distance(upperArm.position, target.position);
        targetDistance = Mathf.Min(targetDistance, armLength - 0.0001f);
        float adjacent = ((hypotenuse * hypotenuse) - (lowerLength * lowerLength) + (targetDistance * targetDistance)) / (2 * targetDistance);
        float angle = Mathf.Acos(adjacent / upperLength) * Mathf.Rad2Deg;

        //store prev info
        Vector3 targetPos = target.position;
        Vector3 polePos = pole.position;
        Transform upperArmParent = upperArm.parent;
        Transform lowerArmParent = lowerArm.parent;
        Transform handParent = hand.parent;
        Vector3 upperArmScale = upperArm.localScale;
        Vector3 lowerArmScale = lowerArm.localScale;
        Vector3 handScale = hand.localScale;
        Vector3 upperArmLocalPos = upperArm.localPosition;
        Vector3 lowerArmLocalPos = lowerArm.localPosition;
        Vector3 handLocalPos = hand.localPosition;
        Quaternion upperArmRot = upperArm.rotation;
        Quaternion lowerArmRot = lowerArm.rotation;
        Quaternion handRot = hand.rotation;

        //move arm
        target.position = targetStart + upperArm.position;
        pole.position = poleStart + upperArm.position;
        //upperArm.rotation = upperStart;
        lowerArm.rotation = lowerStart;
        hand.rotation = handStart;

        transform.position = upperArm.position;
        transform.LookAt(targetPos, polePos - transform.position);

        GameObject UpperAxisCorr, LowerAxisCorr, handAxisCorr;
        UpperAxisCorr = new GameObject("upperArmAxisCorrection");
        LowerAxisCorr = new GameObject("forearmAxisCorrection");
        handAxisCorr = new GameObject("handAxisCorrection");
        UpperAxisCorr.transform.position = upperArm.position;
        UpperAxisCorr.transform.LookAt(lowerArm.position, transform.root.up);
        UpperAxisCorr.transform.parent = transform;
        upperArm.parent = UpperAxisCorr.transform;
        LowerAxisCorr.transform.position = lowerArm.position;
        LowerAxisCorr.transform.LookAt(hand.position, transform.root.up);
        LowerAxisCorr.transform.parent = UpperAxisCorr.transform;
        lowerArm.parent = LowerAxisCorr.transform;
        handAxisCorr.transform.position = hand.position;
        handAxisCorr.transform.parent = LowerAxisCorr.transform;
        hand.parent = handAxisCorr.transform;

        //reset targets
        target.position = targetPos;
        pole.position = polePos;

        UpperAxisCorr.transform.LookAt(target, pole.position - UpperAxisCorr.transform.position);
        UpperAxisCorr.transform.localEulerAngles -= new Vector3(angle, 0f, 0f);

        LowerAxisCorr.transform.LookAt(target, pole.position - UpperAxisCorr.transform.position);
        handAxisCorr.transform.rotation = target.rotation;
        //Restore limbs.
        upperArm.parent = upperArmParent;
        lowerArm.parent = lowerArmParent;
        hand.parent = handParent;
        upperArm.localScale = upperArmScale;
        lowerArm.localScale = lowerArmScale;
        hand.localScale = handScale;
        upperArm.localPosition = upperArmLocalPos;
        lowerArm.localPosition = lowerArmLocalPos;
        hand.localPosition = handLocalPos;
        //Clean up temporary game objets.
        Destroy(UpperAxisCorr);
        Destroy(LowerAxisCorr);
        Destroy(handAxisCorr);

        //Transition.
        transition = Mathf.Clamp01(transition);
        upperArm.rotation = Quaternion.Slerp(upperArmRot, upperArm.rotation, transition);
        lowerArm.rotation = Quaternion.Slerp(lowerArmRot, lowerArm.rotation, transition);
        hand.rotation = Quaternion.Slerp(handRot, hand.rotation, transition);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
