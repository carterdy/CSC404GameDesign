using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class GameStart : MonoBehaviour {

    public GameObject controlsScreen;
    public EventSystem eventSystem;


	static int currentLevel = 1;

	// Use this for initialization
	void Start () {
	}

    public void StartGame()
    {
        if (Input.GetButton("Start"))
        {
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

    /* Called to open the controls screen */
    public void OpenControlsScreen()
    {
        //Show the controls screen
        controlsScreen.SetActive(true);
        //Set the back button to the selected object in the event system
        eventSystem.SetSelectedGameObject(GameObject.Find("Back Button"));
    }

    /* Called to quit the game */
    public void Quit()
    {
        Application.Quit();
    }

    // Safe level increment
    void incrementLevel() {
		if (Application.loadedLevelName == "Victory") {
			currentLevel++;
		}
	}
}
