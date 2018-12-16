using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesKeeper : MonoBehaviour {
	PlayerController pc = new PlayerController();
	UnityEngine.UI.Text myText;
	public int playerHealth;
	private int playerLives = 2;

	// Use this for initialization
	void Start () {
		playerHealth = pc.ReturnHealth();
		myText = GetComponent<Text>();
		myText.text = playerLives.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateHealth(int damage){
		playerHealth -= damage;
		playerLives = playerHealth/100;
		myText.text = playerLives.ToString();
	}
}
