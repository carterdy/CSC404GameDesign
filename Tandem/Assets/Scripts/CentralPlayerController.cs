using UnityEngine;
using System.Collections;

public class CentralPlayerController : MonoBehaviour {

    public GameObject warrior;
    public GameObject archer;
    public static CentralPlayerController instance = null;

    //The boolean determining who is on bottom.  If it's not the warrior, it's the archer.
    private bool warriorBottom = true;


	// Use this for initialization
	void Awake () {
        //Borrowing simple singleton code from Unity's Roguelike tutorial
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    /*Flip the players by disabling movement for the top player and changing the interactable objects */
    void FlipPlayers ()
    {
        warriorBottom = !warriorBottom;
        GameObject[] iceObstacles = GameObject.FindGameObjectsWithTag("IceObstacle");
        GameObject[] fireObstacles = GameObject.FindGameObjectsWithTag("FireObstacle");
        //Set the movement controller scripts
        if (warriorBottom)
        {
            warrior.GetComponent<WarriorBottomController>().enabled = true;
            archer.GetComponent<ArcherBottomController>().enabled = false;
        } else
        {
            archer.GetComponent<ArcherBottomController>().enabled = true;
            warrior.GetComponent<WarriorBottomController>().enabled = false;
        }
        //Enable/disable the colliders on the elemental objects
        foreach (GameObject obstacle in iceObstacles)
        {
            if (warriorBottom)
            {
                obstacle.GetComponents<BoxCollider>()[1].enabled = false;
            } else
            {
                obstacle.GetComponents<BoxCollider>()[1].enabled = true;
            }
        }
        foreach (GameObject obstacle in fireObstacles)
        {
            if (warriorBottom)
            {
                obstacle.GetComponents<BoxCollider>()[1].enabled = true;
            } else
            {
                obstacle.GetComponents<BoxCollider>()[1].enabled = false;
            }
        }
    }

    void FixedUpdate ()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            FlipPlayers();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
