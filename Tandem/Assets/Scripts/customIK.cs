using UnityEngine;
using System.Collections;

public class customIK : MonoBehaviour {


    //bones for bow
    public Transform bow;
    
    //bones for archer
    public Transform rightArm, rightHand;

    //bones for archer that needs to be moved
    public Transform upperArm, lowerArm, hand, neck, chest, boyNeck;

    //public Transform target;
    public Transform pole;
    public Transform target; //hand target


    //public bool isEnabled;
    public float transition = 1.0f;
    public float elbowAngle = 0;
    public float weight = 1;

    //internal variables 
    private Quaternion lowerStart, handStart;
    private Vector3 targetStart, poleStart;

    private GameObject player;
    //private Animator anim;
    private float deadzone;

    float previousPos;
    public float chestRot = 70.0f;
    public float headRot = 30.0f;
    public float boyNeckRot = 135.0f;
    public float LeftAim = 160.0f;
    public float rightAim = 20.0f;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("CompletePlayer");
        previousPos = chest.transform.rotation.eulerAngles.y;
        anim = player.GetComponent<Animator>();
        deadzone = 0.25f;

        bow.parent.Rotate(0, 18.81f, 0);
        
  //      upperStart = upperArm.rotation;
        lowerStart = lowerArm.rotation;
        handStart = hand.rotation;
        targetStart = target.position - upperArm.position;
        poleStart = pole.position - upperArm.position;

	}

    void LateUpdate()
    {
        //check if archer is firing 
        // Aim Axis
        float aimX = Input.GetAxis("AimX");
        float aimY = Input.GetAxis("AimY");
    
        // Used for Deadzone Check
        Vector2 stickInput = new Vector2(aimX, aimY);

        // If joystick is active
        if (stickInput.magnitude > deadzone)
        {
            bow.parent.gameObject.SetActive(true);
            applyRotations(aimX, aimY);

            createIK();

        }
        else
        {
            bow.parent.gameObject.SetActive(false);
        }

    }
    float adjustAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        if (angle <= -180) angle += 360;
        return angle;
    }
    void applyRotations(float aimX, float aimY)
    {
//        Debug.Log("player: " + player.transform.rotation.eulerAngles.y);
        //find the center we want to rotate around
        float center = player.transform.rotation.eulerAngles.y;//0-360
//        Debug.Log("player: " + center);
        center = adjustAngle(center);
        //get the aim ranges
        float leftRange = center - LeftAim;
        float rightRange = center + rightAim;
        leftRange = adjustAngle(leftRange);
        rightRange = adjustAngle(rightRange);
        
        // Math to convert Axis to Angle Direction
        float stickDir = Mathf.Atan2(aimX, aimY) * Mathf.Rad2Deg;
 //       Debug.Log("dir: " + stickDir);
        boyNeck.localRotation = Quaternion.Euler(new Vector3(boyNeckRot, 0, 0));
        rightArm.localEulerAngles = new Vector3(17.9996f, 24.7699f, -32.3385f);

        float wantedChestRot = stickDir - chestRot;
        wantedChestRot = adjustAngle(wantedChestRot);
        float wantedHeadRot = stickDir - headRot;
        wantedHeadRot = adjustAngle(wantedHeadRot);

//        Debug.Log("leftR: "+ leftRange);
//        Debug.Log("rightR: " + rightRange);
//        Debug.Log("wantedR: " + wantedChestRot);

        if (leftRange > rightRange)
        {
            bool left = wantedChestRot <= 180 && wantedChestRot >= leftRange;
            bool right = wantedChestRot >= -180 && wantedChestRot <= rightRange;
            if (left || right)
            {
                chest.rotation = Quaternion.Euler(new Vector3(0, wantedChestRot, 0));
                neck.rotation = Quaternion.Euler(new Vector3(0, wantedHeadRot, 0));
                previousPos = chest.transform.rotation.eulerAngles.y;
            }
            else if (!left)
            {
                //stay on left
                chest.rotation = Quaternion.Euler(new Vector3(0, previousPos, 0));
                neck.rotation = Quaternion.Euler(new Vector3(0, previousPos + headRot, 0));
                chest.localEulerAngles = new Vector3(0, 360 - LeftAim, 0);
                neck.localEulerAngles = new Vector3(0, 30, 0);


            }
            else if (!right)
            {
                chest.rotation = Quaternion.Euler(new Vector3(0, previousPos, 0));
                neck.rotation = Quaternion.Euler(new Vector3(0, previousPos + headRot, 0));
                chest.localEulerAngles = new Vector3(0, rightAim, 0);
                neck.localEulerAngles = new Vector3(0, 30, 0);
            }
        }
        else
        {
            if (wantedChestRot >= leftRange && wantedChestRot <= rightRange)
            {
                chest.rotation = Quaternion.Euler(new Vector3(0, wantedChestRot, 0));
                neck.rotation = Quaternion.Euler(new Vector3(0, wantedHeadRot, 0));
                previousPos = chest.transform.rotation.eulerAngles.y;
            }
            else if (wantedChestRot < leftRange)
            {
                //stay on left
                chest.rotation = Quaternion.Euler(new Vector3(0, previousPos, 0));
                neck.rotation = Quaternion.Euler(new Vector3(0, previousPos + headRot, 0));
                chest.localEulerAngles = new Vector3(0, 360 - LeftAim, 0);
                neck.localEulerAngles = new Vector3(0, 30, 0);

            }
            else if (wantedChestRot > rightRange)
            {
                chest.rotation = Quaternion.Euler(new Vector3(0, previousPos, 0));
                neck.rotation = Quaternion.Euler(new Vector3(0, previousPos + headRot, 0));
                chest.localEulerAngles = new Vector3(0, rightAim, 0);
                neck.localEulerAngles = new Vector3(0, 30, 0);

            }
        }
//        Debug.Log("girl: " + chest.transform.rotation.eulerAngles.y);
//        Debug.Log(chest.transform.localEulerAngles.y);
//        Debug.Log(neck.transform.localEulerAngles.y);


    }

    void createIK()
    {
        float upperLength, lowerLength, armLength;
        upperLength = Vector3.Distance(upperArm.position, lowerArm.position);
        lowerLength = Vector3.Distance(lowerArm.position, hand.position);
        armLength = upperLength + lowerLength;
        float hypotenuse = upperLength;

        //set start rotations
//        upperStart = transform.rotation;
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
