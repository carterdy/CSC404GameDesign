using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    private CentralPlayerController players;

	// Use this for initialization
	void Awake () {
        players = GameObject.Find("CompletePlayer").GetComponent<CentralPlayerController>();
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

    /* Called to kill the game */
    public void CloseGame()
    {
        Application.Quit();
    }

    /* Called to restart the current level */
    public void RestartLevel()
    {
        //Turn time back on in case we were in the pause menu
        Time.timeScale = 1;
        //Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
