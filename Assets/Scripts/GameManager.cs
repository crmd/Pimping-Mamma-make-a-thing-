using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
	GS_RUNNING,
	GS_HIGHSCORES
}

[RequireComponent(typeof(HighscoreManager))]
public class GameManager : MonoBehaviour {

	public int score;
	public float timeLimit = 300.0f;
	public float timer;

	public Text timeText;
	public Text scoreText;

	private HighscoreManager hsManager;
	private GameState gameState;

	public SpriteRenderer preDawnBG;
	public SpriteRenderer sunriseBG;
	float transitionScale;

	public Text highscoreTable;
	public GameObject highscoreLabel;
	public GameObject highscoreNumbers;
	// Use this for initialization
	void Start () {
		hsManager = gameObject.GetComponent<HighscoreManager>();
		gameState = GameState.GS_RUNNING;
		transitionScale = 255/timeLimit;
		ChangeState(0);
	}
	
	// Update is called once per frame
	void Update () {
		preDawnBG.color = new Color(preDawnBG.color.r,
		                            preDawnBG.color.g,
		                            preDawnBG.color.b,
		                            ((timeLimit - timer) * 2) / timeLimit);
		sunriseBG.color = new Color(sunriseBG.color.r,
		                            sunriseBG.color.g,
		                            sunriseBG.color.b,
		                            (timeLimit - timer) / (timeLimit));

		switch(gameState)
		{
		case GameState.GS_RUNNING:
			timer -= Time.deltaTime;
			if((int)timer%60 < 10)
			{
				timeText.text = (int)timer/60 + ":0" + (int)timer%60;
			}
			else
			{
				timeText.text = (int)timer/60 + ":" + (int)timer%60;
			}
			scoreText.text = "Score: " + score;
			if(timer <= 0)
			{
				ChangeState(1);
			}
			break;
		case GameState.GS_HIGHSCORES:
			if(Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			break;
		}
	}

	void ChangeState(int state)
	{
		switch(state)
		{
		case 0:
			highscoreTable.text = "";
			highscoreLabel.SetActive(false);
			highscoreNumbers.SetActive(false);
			timer = timeLimit;
			
			gameState = GameState.GS_RUNNING;
			break;
		case 1:
			hsManager.NewHighscore(score);
			highscoreTable.text = hsManager.GetFormattedScores();
			highscoreLabel.SetActive(true);
			highscoreNumbers.SetActive(true);

			gameState = GameState.GS_HIGHSCORES;
			break;
		}
	}

	public GameState GetState()
	{
		return gameState;
	}


	public void AddScore(int val)
	{
		score += val;
	}
}
