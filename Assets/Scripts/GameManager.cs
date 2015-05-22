using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int score;
	public float timeLimit = 300.0f;
	public float timer;

	public Text timeText;
	public Text scoreText;
	// Use this for initialization
	void Start () {
		timer = timeLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		timer -= Time.deltaTime;
		if((int)timer%60 < 10)
		{
			timeText.text = (int)timer/60 + ":0" + (int)timer%60;
		}
		else
		{
			timeText.text = (int)timer/60 + ":" + (int)timer%60;
		}
		scoreText.text = "$" + score;
	}

	void AddScore(int val)
	{
		score += val;
	}
}
