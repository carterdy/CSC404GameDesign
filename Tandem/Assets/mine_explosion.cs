using UnityEngine;
using System.Collections;

public class mine_explosion : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }
	

    void OnCollisionEnter(Collision col)
    {
        transform.localScale += new Vector3(3, 3, 3);
        StartCoroutine(Example());
        
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

}
