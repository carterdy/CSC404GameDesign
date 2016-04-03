using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject controlsScreen;
    public GameObject pauseScreen;

    private CentralPlayerController players;
    private EventSystem eventSystem;

	// Use this for initialization
	void Awake () {
        players = GameObject.Find("CompletePlayer").GetComponent<CentralPlayerController>();
        eventSystem = EventSystem.current;
	}
	
	void FixedUpdate () {

        //Check to see if the players are still alive
	    if (players.getHP() == 0)
        {
            gameOver();
        }
	}

    /* Called to end the game in a bad way */
    public void gameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    /* Called to end the game */
    public void EndGame()
    {
        //Turn time on and load the title screen
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }

    /* Called to restart the current level */
    public void RestartLevel()
    {
        //Turn time back on in case we were in the pause menu
        Time.timeScale = 1;
        //Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /* Called to open the controls screen */
    public void OpenControlsScreen()
    {
        //Disable the pause screen.  Time will still be stopped though
        pauseScreen.SetActive(false);
        //Show the controls screen
        controlsScreen.SetActive(true);
        //Set the back button to the selected object in the event system
        eventSystem.SetSelectedGameObject(GameObject.Find("Back Button"));
    }

    /* Called to close the controls screen */
    public void CloseControlsScreen()
    {
        controlsScreen.SetActive(false);
        pauseScreen.SetActive(true);
        //Set the resume button in the pause screen to the selected button
        eventSystem.SetSelectedGameObject(GameObject.Find("Resume"));
    }
}
