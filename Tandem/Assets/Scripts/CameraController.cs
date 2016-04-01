using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /* Borrowing the method for camera control from Unity's Training Day tutorial */
    public Transform player;
    public float positionSmoothing = 5f;
    public Vector3 offset = new Vector3(0f, 7f, -7f);
    public float angleUp = 20f;

    // Use this for initialization
    void Start () {

	}
	
	void FixedUpdate () {
        //Find the angle of the player
        float playerAngle = player.transform.eulerAngles.y;
        //Calculate the target camera position as the player's position and rotation plus an offset
        Quaternion targetCameraRot = Quaternion.Euler(0, playerAngle, 0);
        Vector3 targetCameraPos = player.position + (targetCameraRot * offset);
        //Set the camera position by interpolating from the current position to the starting position with a smoothing factor to prevent jumpiness
        transform.position = Vector3.Lerp(transform.position, targetCameraPos, positionSmoothing * Time.deltaTime);
        //Finally look at the player to set the angle.  Looking at the player only makes the camera point too low, so we'll then rotate up after.
        transform.LookAt(player.transform);
        transform.Rotate(Vector3.left * angleUp);
	}
}
