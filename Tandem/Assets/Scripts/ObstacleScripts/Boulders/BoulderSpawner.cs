using UnityEngine;
using System.Collections;


public class BoulderSpawner : MonoBehaviour {

    public float secondsBetween = 3f;
    //A delay to start spawning boulders so all the spawners don't have to spawn at the same time
    public float delay = 0f;
    public GameObject boulder;

    // Use this for initialization
    void Start () {
        InvokeRepeating("launchBoulder", delay, secondsBetween);
	}

    /* Launch a rolling boulder forward from the spawner */
    void launchBoulder ()
    {
        GameObject launchedBoulder = Instantiate(boulder) as GameObject;
        launchedBoulder.transform.position = gameObject.transform.position;
        launchedBoulder.transform.rotation = gameObject.transform.rotation;
    }

    void OnDisable()
    {
        CancelInvoke();
    }
	
}
