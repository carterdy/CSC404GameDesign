using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreAnim : MonoBehaviour {
    private float total;
    private Text text;
    private int i;
	// Use this for initialization
	void Awake () {
        total = Scores.totalScore;
        text = GetComponent<Text>();
        i = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (i <= total)
        {
            text.text = i.ToString();
            i++;
        }
	
	}

}
