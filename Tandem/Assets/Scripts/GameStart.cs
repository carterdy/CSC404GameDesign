using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStart : MonoBehaviour {

	static int currentLevel = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton("Start"))
        {
            //incrementLevel ();
            //// Minus 3 for the non level scene files
            //// Title, Victory and GameOver
            //if (currentLevel <= Application.levelCount - 3) {
            //	SceneManager.LoadScene ("Level" + currentLevel);
            //} 

            //// Restart Title Screen
            //else {
            //	SceneManager.LoadScene ("TitleScreen");
            //}

            //Load level 1 if on title screen, otherwise load the titlescreen (since we'll be on a game over)
            if (SceneManager.GetActiveScene().name == "TitleScreen")
            {
                SceneManager.LoadScene("Level1");
            }
            else {
                SceneManager.LoadScene("TitleScreen");
            }
        }
	}

	// Safe level increment
	void incrementLevel() {
		if (Application.loadedLevelName == "Victory") {
			currentLevel++;
		}
	}
}
