using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /* Borrowing the method for camera control from Unity's Training Day tutorial */
    public Transform player;
    public float positionSmoothing = 5f;
    public float rotationSmoothing = 5f;

    public Vector3 offset = new Vector3(0f, 7f, -7f);

    // Use this for initialization
    void Start () {

	}
	
	void FixedUpdate () {
        Vector3 targetCameraPos = player.position + offset;
        Quaternion targetCamRot = Quaternion.Euler(transform.rotation.x, player.rotation.y, transform.rotation.z);
        transform.position = Vector3.Lerp(transform.position, targetCameraPos, positionSmoothing * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetCamRot, rotationSmoothing * Time.deltaTime);
	}
}
