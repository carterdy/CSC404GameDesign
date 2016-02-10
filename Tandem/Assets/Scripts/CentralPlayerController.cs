using UnityEngine;
using System.Collections;

public class CentralPlayerController : MonoBehaviour {

    public GameObject playerPair;
    public static CentralPlayerController instance = null;

    //The offset to place the top player above the bottom player
    private float topOffset;

    //The boolean determining who is on bottom.  If it's not the warrior, it's the archer.
    private bool warriorBottom = true;

    private GameObject warrior;
    private GameObject archer;


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

        warrior = GameObject.Find("CompletePlayer/Player1");
        archer = GameObject.Find("CompletePlayer/Player2");
        topOffset = archer.transform.localScale.y * 1.5f;
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
            playerPair.GetComponent<WarriorBottomController>().enabled = true;
            playerPair.GetComponent<ArcherBottomController>().enabled = false;
            //Physically switch the players
            warrior.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            archer.transform.localPosition = new Vector3(0.0f, topOffset, 0.0f);
        } else
        {
            playerPair.GetComponent<ArcherBottomController>().enabled = true;
            playerPair.GetComponent<WarriorBottomController>().enabled = false;
            //Physically switch the players
            archer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            warrior.transform.localPosition = new Vector3(0.0f, topOffset, 0.0f);
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
