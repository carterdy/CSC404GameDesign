using UnityEngine;
using System.Collections;

public class TriggerPlane : MonoBehaviour {

    public floatingUp PlaneGameObject;
    private bool triggered;

    // Use this for initialization
    void Start () {
        triggered = false;
    }
	
	// Update is called once per frame
	void Update () {}

    void OnCollisionEnter(Collision col)
    {
        if (triggered == false)
        {
            PlaneGameObject.MoveUp();
            triggered = true;
        }
        Destroy(this.gameObject);

    }

}
