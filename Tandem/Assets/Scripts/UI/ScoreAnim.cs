using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreAnim : MonoBehaviour {
    private float total;
    private Text text;
	// Use this for initialization
	void Awake () {
        total = Scores.totalScore;
        text = GetComponent<Text>();
        text.text = total.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
