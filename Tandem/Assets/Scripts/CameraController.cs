using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /* Borrowing the method for camera control from Unity's Training Day tutorial */
    public Transform player;
    public float positionSmoothing = 5f;
    public Vector3 offset = new Vector3(0f, 7f, -7f);

    // Use this for initialization
    void Start () {

	}
	
	void FixedUpdate () {
        float playerAngle = player.transform.eulerAngles.y;
        Quaternion targetCameraRot = Quaternion.Euler(0, playerAngle, 0);
        Vector3 targetCameraPos = player.position + (targetCameraRot * offset);
        transform.position = Vector3.Lerp(transform.position, targetCameraPos, positionSmoothing * Time.deltaTime);
        transform.LookAt(player.transform);
	}
}
