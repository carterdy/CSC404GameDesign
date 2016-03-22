using UnityEngine;
using System.Collections;

public class floatingUp : MonoBehaviour {

    private bool move;
    public float amplitude;          //Set in Inspector 
    public float speed;                  //Set in Inspector 
    private float tempValy;
    private float tempValx;
    private float tempValz;
    private Vector3 tempPos;

    public Transform target;

    // Use this for initialization
    void Start () {
        tempValx = transform.position.x;
        tempValz = transform.position.z;
        tempValy = transform.position.y;
        move = false;
      }
	
	// Update is called once per frame
	void Update () {
        if (move == true)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            
        }
    }

    void floatingup()
    {
        tempPos.y = tempValy + amplitude * Mathf.Sin(speed * Time.time);
        tempPos.x = tempValx;
        tempPos.z = tempValz;
        transform.position = tempPos;
        StartCoroutine(waiting());
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1);
        move = false;
    }


    public void MoveUp()
    {
        move = true;
    }

}
