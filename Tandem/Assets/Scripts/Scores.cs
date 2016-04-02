using UnityEngine;
using System.Collections;

public class Scores : MonoBehaviour {
    public static int totalScore = 0;
    void awake()
    {
        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
