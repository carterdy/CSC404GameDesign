using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class instructions : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.enabled = false;	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.enabled = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        text.enabled = false;
    }
}
