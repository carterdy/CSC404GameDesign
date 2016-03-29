using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CentralPlayerController : MonoBehaviour {

    public static CentralPlayerController instance = null;

    //These are all UI elements.  They should all be public and plugged in, but some are private because reasons
    public GameObject ArcherStraightActive;
    public GameObject ArcherTurningActive;
    public GameObject WarriorStraightActive;
    public GameObject WarriorTurningActive;
    public GameObject ArcherFlipBase;
    public GameObject ArcherFlipActive;
    public GameObject ArcherFlipWaiting;
    public GameObject WarriorFlipBase;
    public GameObject WarriorFlipActive;
    public GameObject WarriorFlipWaiting;

    public GameObject[] hearts;
    public GameObject[] deadHearts;

    //The boolean determining who is on bottom.  If it's not the warrior, it's the archer.
    private bool warriorBottom = true;
    //Player's HP. Player's health should not exceed this starting value.
    private int HP = 5;

    private GameObject ArcherTurningBase;
    private GameObject ArcherStraightBase;
    private GameObject WarriorTurningBase;
    private GameObject WarriorStraightBase;
	private GameObject startingSpawn;
	private GameObject respawn;

    private Animator players;

    public AudioClip flipSound;
    public AudioClip hitSound;
    private AudioSource source;
    public int reaction;
    private int reactionTemp;
    private bool soundrelease;


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

        ArcherTurningBase = GameObject.Find("ArcherTurningBase");
        ArcherStraightBase = GameObject.Find("ArcherStraightBase");
        WarriorTurningBase = GameObject.Find("WarriorTurningBase");
        WarriorStraightBase = GameObject.Find("WarriorStraightBase");
		startingSpawn = GameObject.Find ("StartZone");
		respawn = Instantiate (startingSpawn, 
			gameObject.transform.position, 
			startingSpawn.transform.rotation)
			as GameObject;

        WarriorTurningBase.SetActive(false);
        ArcherStraightBase.SetActive(false);

		gameObject.transform.position = startingSpawn.transform.position;

        //audio setup
        source = GetComponent<AudioSource>();
        reactionTemp = reaction; 
    }

    /*  Return the amount of HP the players have left */
    public int getHP()
    {
        return HP;
    }

    /* Sets which script and UI elements are active depending on which player is top */
    void setPlayerState ()
    {
        //Set the movement controller scripts
        if (!players.IsInTransition(0) && players.GetCurrentAnimatorStateInfo(0).IsName("girlIdle"))
        {
            gameObject.GetComponent<WarriorBottomController>().enabled = true;
            gameObject.GetComponent<WarriorTopController>().enabled = false;
            gameObject.GetComponent<ArcherBottomController>().enabled = false;
            gameObject.GetComponent<ArcherTopController>().enabled = true;
            //Flip the icons
            ArcherTurningBase.SetActive(true);
            ArcherStraightBase.SetActive(false);
            WarriorTurningBase.SetActive(false);
            WarriorStraightBase.SetActive(true);
            //Physically switch the players
            players.SetInteger("flip", 1);            
        }

        else if (!players.IsInTransition(0) && players.GetCurrentAnimatorStateInfo(0).IsName("boyIdle"))
        {
            gameObject.GetComponent<ArcherBottomController>().enabled = true;
            gameObject.GetComponent<ArcherTopController>().enabled = false;
            gameObject.GetComponent<WarriorBottomController>().enabled = false;
            gameObject.GetComponent<WarriorTopController>().enabled = true;
            //Flip the icons
            ArcherTurningBase.SetActive(false);
            ArcherStraightBase.SetActive(true);
            WarriorTurningBase.SetActive(true);
            WarriorStraightBase.SetActive(false);
        }

        //Deactivate all the active movement indicators
        ArcherTurningActive.SetActive(false);
        ArcherStraightActive.SetActive(false);
        WarriorTurningActive.SetActive(false);
        WarriorStraightActive.SetActive(false);

    }

    /*Flip the players by disabling movement for the top player and changing the interactable objects */
    void FlipPlayers ()
    {
        //Set the movement controller scripts
        if (!players.IsInTransition(0) && players.GetCurrentAnimatorStateInfo(0).IsName("girlIdle"))
        {
            warriorBottom = true;
            setPlayerState();
            //Physically switch the players
            players.SetInteger("flip", 1);
            //flip audio
            source.PlayOneShot(flipSound, 1F);

        }
        else if (!players.IsInTransition(0) && players.GetCurrentAnimatorStateInfo(0).IsName("boyIdle"))
        {
            warriorBottom = false;
            setPlayerState();
            //Physically switch the players
            players.SetInteger("flip", 1);
            //flip audio
            source.PlayOneShot(flipSound, 1F);
        }
        
    }

    /* Called to deal damage to the players. Players take 1 heart of damage per call */
    public void takeDamage()
    {
        hearts[HP - 1].SetActive(false);
        deadHearts[HP - 1].SetActive(true);
        HP--;  //----------------------------------

        //Taking damage audio
        if (soundrelease)
        {
            source.PlayOneShot(hitSound, 1F);
            reactionTemp = 0;
            soundrelease = false;
        }
    }

    /* Called while the player is standing in fire */
    public void standingInFire ()
    {
        if (warriorBottom)
        {
            takeDamage();
        }
    }

    /* Check if players want to switch and change the UI/switch accordingly */
    void checkForSwitch()
    {
        players.SetInteger("flip", 0);
        float firstPlayerSwitch = Input.GetAxis("Switch1");
        float secondPlayerSwitch = Input.GetAxis("Switch2");
        Debug.Log(firstPlayerSwitch);
        Debug.Log(secondPlayerSwitch);

        if (firstPlayerSwitch > 0 && secondPlayerSwitch == 0)
        { //First player is holding down switch button but not second
            Debug.Log("p1 but no p2");
            WarriorFlipActive.SetActive(true);
            WarriorFlipBase.SetActive(false);
            ArcherFlipWaiting.SetActive(true);
            ArcherFlipBase.SetActive(false);
        }
        else if (firstPlayerSwitch == 0 && secondPlayerSwitch > 0)
        { //Second player is holding down switch button but not first
            Debug.Log("p2 but no p1");
            WarriorFlipWaiting.SetActive(true);
            WarriorFlipBase.SetActive(false);
            ArcherFlipActive.SetActive(true);
            ArcherFlipBase.SetActive(false);
        }
        else if (firstPlayerSwitch > 0 && secondPlayerSwitch > 0)
        { //Both players wanth to switch
            Debug.Log("both");
            WarriorFlipActive.SetActive(true);
            ArcherFlipActive.SetActive(true);
            WarriorFlipWaiting.SetActive(false);
            ArcherFlipWaiting.SetActive(false);
            WarriorFlipBase.SetActive(false);
            ArcherFlipBase.SetActive(false);
            //Have to make sure players are grounded and not already flipping
            if (gameObject.GetComponent<WarriorBottomController>().isGrounded())
            {
                if (!players.GetCurrentAnimatorStateInfo(0).IsTag("flip"))
                {
                    FlipPlayers();
                }
            }
        }
        else
        { //No one wants to switch
            Debug.Log("none");
            WarriorFlipActive.SetActive(false);
            WarriorFlipWaiting.SetActive(false);
            ArcherFlipActive.SetActive(false);
            ArcherFlipWaiting.SetActive(false);
            WarriorFlipBase.SetActive(true);
            ArcherFlipBase.SetActive(true);
        }
    }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Water")
		{
			takeDamage ();
			gameObject.SetActive(false);
			gameObject.transform.position = respawn.transform.position;
            gameObject.SetActive(true);
            players.SetBool("bottom", warriorBottom);
        }

        //damage from mine exploding. THIS SHOULD BE IN THE MINE SCRIPT AND CALL PLAYER'S TAKE DAMAGE
        if (other.gameObject.tag == "Mine")
        {
            takeDamage();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "End Zone2")
        {
            SceneManager.LoadScene("Victory");
        } else if (other.tag == "End Zone")
        {
            SceneManager.LoadScene("Level2");
        }
    }

    void FixedUpdate ()
    {

        checkForSwitch();

        // Respawn should follow player, when grounded
        if (gameObject.GetComponent<WarriorBottomController> ().isGrounded ()) {
			respawn.transform.position = gameObject.transform.position;
		}

        // reduce continously hit sound effect
        if (reactionTemp < reaction)
        {
            reactionTemp++;
        }
        else
        {
            soundrelease = true;
        }
    }
	
	// Update is called once per frame
	void Update () { 
	}

    void OnEnable()
    {
        setPlayerState();
    }

}
