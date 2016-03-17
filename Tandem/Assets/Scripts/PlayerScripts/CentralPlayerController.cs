using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CentralPlayerController : MonoBehaviour {

    public static CentralPlayerController instance = null;

    public float playerHealth = 100f;

    //The boolean determining who is on bottom.  If it's not the warrior, it's the archer.
    private bool warriorBottom = true;

    private GameObject archerTopIcon;
    private GameObject archerBottomIcon;
    private GameObject warriorTopIcon;
    private GameObject warriorBottomIcon;
    private GameObject playerHealthBar;
	private GameObject startingSpawn;
	private GameObject respawn;

    private Animator players;

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

        players = GetComponent<Animator>();

        archerTopIcon = GameObject.Find("ArcherTopIcon");
        archerBottomIcon = GameObject.Find("ArcherBottomIcon");
        warriorTopIcon = GameObject.Find("WarriorTopIcon");
        warriorBottomIcon = GameObject.Find("WarriorBottomIcon");
        playerHealthBar = GameObject.Find("PlayerHealthBar");
		startingSpawn = GameObject.Find ("StartZone");
		respawn = Instantiate (startingSpawn, 
			gameObject.transform.position, 
			startingSpawn.transform.rotation)
			as GameObject;

        warriorTopIcon.SetActive(false);
        archerBottomIcon.SetActive(false);

		gameObject.transform.position = startingSpawn.transform.position;
        
    }

    /* Sets which script and UI elements are active depending on which player is top */
    void setPlayerState ()
    {
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
        }
    }

    /*Flip the players by disabling movement for the top player and changing the interactable objects */
    void FlipPlayers ()
    {
        GameObject[] iceObstacles = GameObject.FindGameObjectsWithTag("IceObstacle");
        GameObject[] fireObstacles = GameObject.FindGameObjectsWithTag("FireObstacle");
        //Set the movement controller scripts
        if (!players.IsInTransition(0) && players.GetCurrentAnimatorStateInfo(0).IsName("girlIdle"))
        {
            warriorBottom = true;
            setPlayerState();
            //Physically switch the players
            players.SetInteger("flip", 1);
            
            
        }
        else if (!players.IsInTransition(0) && players.GetCurrentAnimatorStateInfo(0).IsName("boyIdle"))
        {
            warriorBottom = false;
            setPlayerState();
            //Physically switch the players
            //archer.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            //warrior.transform.localPosition = new Vector3(0.0f, topOffset, 0.0f);
            players.SetInteger("flip", 1);
        }
        
    }

    /* Called to deal damage to the players */
    public void takeDamage(float damage)
    {
        playerHealth -= damage;
    }

    /* Called while the player is standing in fire */
    public void standingInFire ()
    {
        if (warriorBottom)
        {
            takeDamage(5 * Time.deltaTime);
        }
    }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Water")
		{
			takeDamage (20.0f);
			gameObject.SetActive(false);
			gameObject.transform.position = respawn.transform.position;
			gameObject.SetActive(true);
		}

        //damage from mine exploding
        if (other.gameObject.tag == "Mine")
        {
            takeDamage(35);
        }

        //damage from shooting obstacles
        if (other.gameObject.tag == "Pebble")
        {
            takeDamage(5);
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "End Zone")
        {
            SceneManager.LoadScene("Victory");
        }
    }

    void FixedUpdate ()
    {
        players.SetInteger("flip", 0);
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<WarriorBottomController>().isGrounded())
        {
            if (!players.GetCurrentAnimatorStateInfo(0).IsTag("flip"))
            {
                FlipPlayers();
            }
        }

        // Respawn should follow player, when grounded
        if (gameObject.GetComponent<WarriorBottomController> ().isGrounded ()) {
			respawn.transform.position = gameObject.transform.position;
		}
    }
	
	// Update is called once per frame
	void Update () {
        playerHealthBar.GetComponent<RectTransform>().localScale = new Vector3(playerHealth / 100f, 0.33f, 0f);
	}

    void OnEnable()
    {
        setPlayerState();
    }

}
