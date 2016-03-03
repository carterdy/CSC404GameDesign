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
	    if (players.playerHealth <= 0)
        {
            gameOver();
        }
	}

    /* Called to end the game in a bad way */
    public void gameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
