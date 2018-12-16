using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	public static int score;

	private Text myText;
	// Use this for initialization
	void Start () {
		score=0;
		myText = GetComponent<Text>();
	}
	// Update is called once per frame
	void Update () {
	}

	public void Score(int points){
		score += points;
		myText.text = score.ToString();
	}

	public static void Reset(){
		score = 0;
	}
}
