using UnityEngine;
using System.Collections;

public class mine_explosion : MonoBehaviour {

    private bool explode;

    // Use this for initialization
    void Start () {
        explode = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (explode == false)
        {
            transform.localScale += new Vector3(3, 3, 3);
            StartCoroutine(waiting());
            explode = true;
        }
       
        
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

}
