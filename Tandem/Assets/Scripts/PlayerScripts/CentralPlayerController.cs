using UnityEngine;
using System.Collections;

public class CentralPlayerController : MonoBehaviour {

    public static CentralPlayerController instance = null;

    public float playerHealth = 100f;

    //The offset to place the top player above the bottom player
    private float topOffset;

    //The boolean determining who is on bottom.  If it's not the warrior, it's the archer.
    private bool warriorBottom = true;

    private GameObject warrior;
    private GameObject archer;
    private GameObject archerTopIcon;
    private GameObject archerBottomIcon;
    private GameObject warriorTopIcon;
    private GameObject warriorBottomIcon;
    private GameObject playerHealthBar;


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
        archerTopIcon = GameObject.Find("ArcherTopIcon");
        archerBottomIcon = GameObject.Find("ArcherBottomIcon");
        warriorTopIcon = GameObject.Find("WarriorTopIcon");
        warriorBottomIcon = GameObject.Find("WarriorBottomIcon");
        playerHealthBar = GameObject.Find("PlayerHealthBar");

        warriorTopIcon.SetActive(false);
        archerBottomIcon.SetActive(false);
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
            gameObject.GetComponent<WarriorBottomController>().enabled = true;
            gameObject.GetComponent<WarriorTopController>().enabled = false;
            gameObject.GetComponent<ArcherBottomController>().enabled = false;
            gameObject.GetComponent<ArcherTopController>().enabled = true;
            //Flip the icons
            archerTopIcon.SetActive(true);
            archerBottomIcon.SetActive(false);
            warriorTopIcon.SetActive(false);
            warriorBottomIcon.SetActive(true);
            //Physically switch the players
            warrior.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            archer.transform.localPosition = new Vector3(0.0f, topOffset, 0.0f);
        } else
        {
            gameObject.GetComponent<ArcherBottomController>().enabled = true;
            gameObject.GetComponent<ArcherTopController>().enabled = false;
            gameObject.GetComponent<WarriorBottomController>().enabled = false;
            gameObject.GetComponent<WarriorTopController>().enabled = true;
            //Flip the icons
            archerTopIcon.SetActive(false);
            archerBottomIcon.SetActive(true);
            warriorTopIcon.SetActive(true);
            warriorBottomIcon.SetActive(false);
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

    /* Called to deal damage to the players */
    public void takeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    /* Called while the player is standing in fire */
    public void standingInFire ()
    {
        if (warriorBottom)
        {
            takeDamage(2 * Time.deltaTime);
        }
    }

    void FixedUpdate ()
    {
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<WarriorBottomController>().isGrounded())
        {
            FlipPlayers();
        }
    }
	
	// Update is called once per frame
	void Update () {
        playerHealthBar.GetComponent<RectTransform>().localScale = new Vector3(playerHealth / 100f, 0.33f, 0f);
	}
}
