using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResetScores : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        string scene = SceneManager.GetActiveScene().name;
        Debug.Log(scene);
        if (scene == "TitleScreen" || scene == "Victory" || scene == "GameOver") Scores.totalScore = 0;
	
	}
}
