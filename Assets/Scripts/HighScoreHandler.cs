using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreHandler : MonoBehaviour
{
    public Text HighScoreText;

    private void Start()
    {
        DisplayHighScore();
    }

    public void DisplayHighScore()
    {
        HighScoreText.text = GetHighScore().ToString();
    }

    public static int GetHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore")) return PlayerPrefs.GetInt("HighScore");
        else return 0;
    }

    public static void SetHighScore(int score)
    {
        int savedScore = PlayerPrefs.GetInt("HighScore", score);
        if (score < savedScore) score = savedScore;
        PlayerPrefs.SetInt("HighScore", score);
    }
    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }

    public void ResetHighScoreButton()
    {
        ResetHighScore();
        DisplayHighScore();
    }

}
