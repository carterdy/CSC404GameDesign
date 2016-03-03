using UnityEngine;
using System.Collections;

public class Peddle_shooting : MonoBehaviour {

    // Use this for initialization
    //void Start () {
    //}

    // Update is called once per frame
    //void Update () {
    //}
    public GameObject obstacle;
    public GameObject peddle;
    public float speed = 0.1f;


    // Update is called once per frame
    void Update()
    {

            //bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject b = Instantiate(peddle, obstacle.transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity) as GameObject;
        //b.transform.position = player.transform.position + new Vector3 (0.5f, 0, 0);
        b.transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

}
