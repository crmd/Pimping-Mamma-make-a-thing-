using UnityEngine;
using System.Collections;

public class HighscoreManager : MonoBehaviour {

	private int highscoreLength = 10;
	private string formattedScores;
	// Use this for initialization
	void Start () {

		for(int i = 0; i < highscoreLength; ++i)
		{
			if(!PlayerPrefs.HasKey("PimpingMammaHS"+i))
			{
				ClearHighscores();
				break;
			}
		}
	}

	public void NewHighscore(int score)
	{
		int index = FindScoreIndex(score);
		if(index > -1)
		{
			OverwriteScores(score, index);
		}
		FormatScores();
	}

	void ClearHighscores()
	{
		for(int i = 0; i < highscoreLength; ++i)
		{
			if(PlayerPrefs.HasKey("PimpingMammaHS"+i))
			{
				PlayerPrefs.DeleteKey("PimpingMammaHS"+i);
			}
			PlayerPrefs.SetInt("PimpingMammaHS"+i, 0);
		}
	}

	int FindScoreIndex(int score)
	{
		for(int i = 0; i < highscoreLength; ++i)
		{
			if(PlayerPrefs.HasKey("PimpingMammaHS"+i))
			{
				if(PlayerPrefs.GetInt("PimpingMammaHS"+i) < score)
				{
					return i;
				}
			}
		}
		return -1;
	}

	void OverwriteScores(int score, int index)
	{
		int oldScore = 0;
		int newScore = 0;
		if(PlayerPrefs.HasKey("PimpingMammaHS"+index))
		{
			oldScore = PlayerPrefs.GetInt("PimpingMammaHS"+index);
			PlayerPrefs.SetInt("PimpingMammaHS"+index, score);
		}
		index++;
		for(int i = index; i < highscoreLength; ++i)
		{
			if(PlayerPrefs.HasKey("PimpingMammaHS"+i))
			{
				newScore = PlayerPrefs.GetInt("PimpingMammaHS"+i);
				PlayerPrefs.SetInt("PimpingMammaHS"+index, oldScore);
				oldScore = newScore;
			}
		}
	}

	void FormatScores()
	{
		formattedScores = "";
		for(int i = 0; i < highscoreLength; ++i)
		{
			if(PlayerPrefs.HasKey("PimpingMammaHS"+i))
			{
				formattedScores += PlayerPrefs.GetInt("PimpingMammaHS"+i).ToString();
			}
			formattedScores += "\n";
		}
	}

	public string GetFormattedScores()
	{
		return formattedScores;
	}
}
