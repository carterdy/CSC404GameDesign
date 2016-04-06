using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using XInputDotNetPure;

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

    public GameObject pauseScreen;

    public AudioClip flipSound;
    public AudioClip hitSound;
    public int reaction;

    public int gemCost = 10;


    //The boolean determining who is on bottom.  If it's not the warrior, it's the archer.
    private bool warriorBottom = true;
    //Player's HP. Player's health should not exceed this starting value.
    private int HP = 5;

    //States for players being invulnerable and the game being paused.  paused has to be checked for anything that shouldn't happen while paused
    private bool invuln = false;
    private bool paused = false;
    private bool pausing = false;

    private GameObject ArcherTurningBase;
    private GameObject ArcherStraightBase;
    private GameObject WarriorTurningBase;
    private GameObject WarriorStraightBase;

	// Respawn Stuff
	private GameObject startingSpawn;
	private GameObject respawn;
	private Vector3 newPos;
	private Vector3 oldPos;
	private Vector3 movement;

    private Animator players;

    private AudioSource source;
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

    /* A function that can be called by other objects to deal damage to the player */
    public void dealDamage ()
    {
        StartCoroutine(takeDamage());
    }

    /* Called to deal damage to the players. Players take 1 heart of damage per call.  Have to also start the invulnerability phase. */
    IEnumerator takeDamage()
    {
        if (!invuln)
        {

            GamePad.SetVibration(PlayerIndex.One, 0.3f, 0.3f);
            GamePad.SetVibration(PlayerIndex.Two, 0.3f, 0.3f);
            GamePad.SetVibration(PlayerIndex.Three, 0.3f, 0.3f);
            GamePad.SetVibration(PlayerIndex.Four, 0.3f, 0.3f);



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
            //Now have to make player invulnerable and start the visual effect.  Invulnerabiliy ends 1 second later.
            invuln = true;
            StartCoroutine(invulnFlicker(1));

            yield return new WaitForSeconds(1f);
            GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
            GamePad.SetVibration(PlayerIndex.Two, 0.0f, 0.0f);
            GamePad.SetVibration(PlayerIndex.Three, 0.0f, 0.0f);
            GamePad.SetVibration(PlayerIndex.Four, 0.0f, 0.0f);

            invuln = false;
        }
    }

    /* When started, cause the players to flicker for the given number of seconds */
    IEnumerator invulnFlicker(int seconds)
    {
        float flickerTime = 0.1f;
        //Calculate the number of times to flicker based off given seconds to flicker for
        int flickerTimes = (int) (seconds / flickerTime);
        //So, have to go through all the body parts of the players and change the material colour to make them flicker
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        //Flicker number of times needed
        for (int i = 0; i < flickerTimes / 2; i++)
        {
            //Go through each renderer found in children
            foreach (Renderer rend in renderers)
            {
                //Change the emmision of the material to make the part look pale
                Material mat = rend.material;
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.grey);
            }
            //Now wait a fraction of a second before changing back
            yield return new WaitForSeconds(flickerTime);
            foreach (Renderer rend in renderers)
            {
                //Change the emmision of the material to make the part look pale
                Material mat = rend.material;
                mat.SetColor("_EmissionColor", Color.black);
            }
            //Now wait again before flickering
            yield return new WaitForSeconds(flickerTime);
        }
        yield return null;
    }

    /* Called while the player is standing in fire */
    public void standingInFire ()
    {
        if (warriorBottom)
        {
           StartCoroutine(takeDamage());
        }
    }

    /* Check if players want to switch and change the UI/switch accordingly */
    void checkForSwitch()
    {
        players.SetInteger("flip", 0);
        float firstPlayerSwitch = Input.GetAxis("Switch1");
        float secondPlayerSwitch = Input.GetAxis("Switch2");
        if (firstPlayerSwitch > 0 && secondPlayerSwitch == 0)
        { //First player is holding down switch button but not second
            WarriorFlipActive.SetActive(true);
            WarriorFlipBase.SetActive(false);
            ArcherFlipWaiting.SetActive(true);
            ArcherFlipBase.SetActive(false);
            WarriorFlipBase.SetActive(false);
            ArcherFlipBase.SetActive(false);
        }
        else if (firstPlayerSwitch == 0 && secondPlayerSwitch > 0)
        { //Second player is holding down switch button but not first
            WarriorFlipWaiting.SetActive(true);
            WarriorFlipBase.SetActive(false);
            ArcherFlipActive.SetActive(true);
            ArcherFlipBase.SetActive(false);
            WarriorFlipBase.SetActive(false);
            ArcherFlipBase.SetActive(false);
        }
        else if (firstPlayerSwitch > 0 && secondPlayerSwitch > 0)
        { //Both players wanth to switch
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
            WarriorFlipActive.SetActive(false);
            WarriorFlipWaiting.SetActive(false);
            ArcherFlipActive.SetActive(false);
            ArcherFlipWaiting.SetActive(false);
            WarriorFlipBase.SetActive(true);
            ArcherFlipBase.SetActive(true);
        }
    }

	// SetPlayerState is reversed for Water Respawn for some reason...
	// Duplicated that code but reversed logic
	void enableControls ()
	{
		//Set the movement controller scripts
		if (warriorBottom) {
			gameObject.GetComponent<WarriorBottomController> ().enabled = true;
			gameObject.GetComponent<WarriorTopController> ().enabled = false;
			gameObject.GetComponent<ArcherBottomController> ().enabled = false;
			gameObject.GetComponent<ArcherTopController> ().enabled = true;
			//Flip the icons
			ArcherTurningBase.SetActive (true);
			ArcherStraightBase.SetActive (false);
			WarriorTurningBase.SetActive (false);
			WarriorStraightBase.SetActive (true);
			//Physically switch the players
			//players.SetInteger ("flip", 1);            
		} else if (!warriorBottom) {
			gameObject.GetComponent<ArcherBottomController> ().enabled = true;
			gameObject.GetComponent<ArcherTopController> ().enabled = false;
			gameObject.GetComponent<WarriorBottomController> ().enabled = false;
			gameObject.GetComponent<WarriorTopController> ().enabled = true;
			//Flip the icons
			ArcherTurningBase.SetActive (false);
			ArcherStraightBase.SetActive (true);
			WarriorTurningBase.SetActive (true);
			WarriorStraightBase.SetActive (false);
		}

		//Deactivate all the active movement indicators
		ArcherTurningActive.SetActive (false);
		ArcherStraightActive.SetActive (false);
		WarriorTurningActive.SetActive (false);
		WarriorStraightActive.SetActive (false);
	}

	void disableControls ()
	{
		gameObject.GetComponent<ArcherBottomController> ().enabled = false;
		gameObject.GetComponent<ArcherTopController> ().enabled = false;
		gameObject.GetComponent<WarriorBottomController> ().enabled = false;
		gameObject.GetComponent<WarriorTopController> ().enabled = false;
	}

	IEnumerator disableControlsFor (float seconds)
	{
		disableControls ();
		yield return new WaitForSeconds (seconds);
		enableControls ();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Water")
		{
			StartCoroutine(takeDamage ());
			StartCoroutine (disableControlsFor (1));
			gameObject.transform.position = respawn.transform.position;
            players.SetBool("bottom", warriorBottom);
        }

        //damage from mine exploding
        if (other.gameObject.tag == "Mine")
        {
            StartCoroutine(takeDamage());
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
        if(other.tag == "Gem")
        {
            Destroy(other.gameObject);
            Scores.totalScore += gemCost;
        }
    }

    void FixedUpdate ()
    {
        checkForSwitch();

        // Respawn should follow player, when grounded
        if (gameObject.GetComponent<WarriorBottomController> ().isGrounded ()) {
			newPos = transform.position;
			movement = (newPos - oldPos);

			// Move respawn to the player
//			respawn.transform.position = gameObject.transform.position;

			// If moving Forward
			if (Vector3.Dot (gameObject.transform.forward, movement) > 0) {
				// Offset respawn behind player
				respawn.transform.position = gameObject.transform.position;
				respawn.transform.position -= gameObject.transform.forward / 2;
			} else if (Vector3.Dot (gameObject.transform.forward, movement) < 0) {
				// Offset respawn in front player
				respawn.transform.position = gameObject.transform.position;
				respawn.transform.position += gameObject.transform.forward / 2;
			}
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

    /* Pause the game */
    IEnumerator pause()
    {
        //Check to make sure we're not already pausing/unpausing.  Prevents spam
        if (!pausing)
        {
            pausing = true;
            //First set paused bool to reflect the pause
            paused = true;
            //Turn on the pause screen
            pauseScreen.SetActive(true);
            //Set the resume button to the default selected button
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Resume"));
            //Wait a fraction of a second to keep the pause from being spammed
            yield return new WaitForSeconds(0.2f);
            pausing = false;
            //Set timescale to be 0 so physics stops happening
            Time.timeScale = 0;
        }
    }

    /* Unpause the game */
    IEnumerator unpause()
    {
        if (!pausing)
        {
            pausing = true;
            //first set paused bool to false
            paused = false;
            //Turn time back on
            Time.timeScale = 1;
            //Take the pause screen off
            pauseScreen.SetActive(false);
            //Wait a fraction of a second to keep the pause from being spammed
            yield return new WaitForSeconds(0.2f);
            pausing = false;
        }
    }

    /* Public access of Pause so UI buttons etc can call it */
    public void Pause()
    {
        StartCoroutine(pause());
    }

    /* Public access of Unpause so UI buttons can call it */
    public void Unpause()
    {
        StartCoroutine(unpause());
    }
	
	// Update is called once per frame.  Pause mechanics go in here because fixed update won't be called while the game is paused
	void Update () { 
        if (Input.GetButton("Start") && !paused)
        {
            StartCoroutine(pause());
        } else if (Input.GetButton("Start") && paused)
        {
            StartCoroutine(unpause());
        }
	}

	void LateUpdate() {
		oldPos = transform.position;
	}

    void OnEnable()
    {
        setPlayerState();
    }
    
    void OnDisable()
    {
        GamePad.SetVibration((PlayerIndex)0, 0.0f, 0.0f);
        GamePad.SetVibration((PlayerIndex)1, 0.0f, 0.0f);

    }
}
