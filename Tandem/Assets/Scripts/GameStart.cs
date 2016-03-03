using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton("Start"))
        {
            SceneManager.LoadScene("AlphaLevel1");
        }
	}
}
